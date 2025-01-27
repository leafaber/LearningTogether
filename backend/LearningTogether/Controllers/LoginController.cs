using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace LearningTogether.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private IConfiguration _config;
        
        // value if user already exists that will be sent as a POST response 
        public class AuthenticateUser
        {
            public bool Exists { get; set; }
            public string? Token { get; set; }
            public string? Email { get; set; }
            public string? Role { get; set; }
            public AuthenticateUser(bool exist)
            {
                Exists = (bool)exist;
            }
        }

        public LoginController(ApplicationDbContext context, IConfiguration config)
        {
            // context is a database context - all tables and data is accessed through it
            _context = context;
            // config is the appsetting.json configuration
            _config = config;
        }

        // GET: api/login
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LTUser>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            
            return await _context.Users.ToListAsync();
        }
        
        // POST: api/login
        /*
         * When a foreign form is expected to post use [FromForm] next to the parameters in the constructor
         *          Lea
         */
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticateUser>> PostUser([FromForm] string email, [FromForm] string password)
        {
            // Exists returns true is the user's email is already registered in DB
            var authenticateUser = new AuthenticateUser(false);
            if (_context.Users == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Users' is null.");
          }
          
          // first user found with this email is taken, if no results returns null
          var user = _context.Users.FirstOrDefault(u => u.Email == email);
          // exists:true - email exists and good password was entered, exists:false - email doesn't exist or the password is wrong
          // creating the pwd hash
          var hashedPassword = RegisterController.ComputeHash(Encoding.UTF8.GetBytes(password));
          
          // checking if the hash is the same as the one in the db
          if (user != null && user.PasswordHash == hashedPassword)
          {
              var token = GenerateToken(user);
              authenticateUser.Exists = true;
              authenticateUser.Token = token;
              authenticateUser.Email=user.Email;
              authenticateUser.Role = user.Role;

          }
            // if the user doesnt exist, no content is returned (null)        
          return authenticateUser;
        }

        // Generate the authentication token 

        private string GenerateToken(LTUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            //string IBAN = user.IBAN ?? "";  won't be used
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /*
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
