using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;
using Microsoft.IdentityModel.Tokens;

namespace LearningTogether.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;

        public RegisterController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/register
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LTUser>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/register/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LTUser>> GetUser(int id)
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
            // Should be changed and should just send wanted info - use old User model for that
            return user;
        }

        // POST: api/register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginController.AuthenticateUser>> PostUser([FromForm] string email, [FromForm] string password)
        {
            // Exists returns true is the user's email is already registered in DB
            var authenticateUser = new LoginController.AuthenticateUser(false);
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            
            //   Adding a new user to DB
            if (user == null)
            {
                var hashedPassword = ComputeHash(Encoding.UTF8.GetBytes(password));
                var newUser = new LTUser
                {
                    Email = email, PasswordHash = hashedPassword, FirstName = "", LastName = "", Role = "User"
                };
 
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                var token = GenerateToken(newUser);
                authenticateUser.Token = token;
            } else {
                authenticateUser.Exists = true;
            }
            return authenticateUser;
         }

        // DELETE: api/register/5
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

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       // creating a hash for the password
        public static string ComputeHash(byte[] bytesToHash)
        {
            var bytes = new byte[128 / 8];
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, bytes, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }
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
    }
}
