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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;

        public UsersController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /* GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LTUser>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }
        */

        // GET: api/users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<LTUser>> GetLTUser()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          var identity = HttpContext.User.Identity as ClaimsIdentity;
          if (identity != null)
          {
              return CoursesController.GetCurrentUser(identity);
          }
        return Problem("Invalid identity");
        }
        

        // POST: api/users/edit      EDITING USER DATA
        [HttpPost("edit")]
        [Authorize]
        // EMAIL SHOULDN'T BE EVER CHANGED
        public async Task<ActionResult<LoginController.AuthenticateUser>> PostLTUser([FromForm] string? password, [FromForm]string? firstName, [FromForm]string? lastName, [FromForm]string? phoneNumber)
        {
            // change data in DB and give a new token with new user claims
          if (_context.Users == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
          }
          
          var identity = HttpContext.User.Identity as ClaimsIdentity;
          
          if (identity != null)
          {
              var currentUser = CoursesController.GetCurrentUser(identity);
              LTUser ltUser = _context.Users.FirstOrDefault(u => u.Email == currentUser.Email);
              if (ltUser == null) Problem("User doesn't exist in the DB");

              if (password != null)
              {
                  ltUser.PasswordHash = RegisterController.ComputeHash(Encoding.UTF8.GetBytes(password));
              }
              ltUser.FirstName = firstName ?? currentUser.FirstName;
              ltUser.LastName = lastName ?? currentUser.LastName;
              ltUser.PhoneNumber = phoneNumber ?? currentUser.PhoneNumber;
              
              try
              {
                  _context.Update(ltUser);
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!LTUserExists(ltUser.Id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }
              var authenticateUser = new LoginController.AuthenticateUser(true);
              var token = GenerateToken(ltUser);
              authenticateUser.Token = token;
              return authenticateUser;
          }
            // PROBLEM OCCURS WHEN TRYING TO MAKE A NEW TOKEN - nullreference exception
          return Problem("Invalid identity");
        }

        // DELETE: api/users
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteLTUser([FromForm] string? email)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                LTUser ltUser;
                // Admin deletes other user's account
                if (email != null)
                {
                    if (currentUser.Role == "Admin")
                    {
                        ltUser = _context.Users.FirstOrDefault( u => u.Email == email);
                        if (ltUser == null) Problem("Invalid email");
                    }
                    else
                    {
                        return Problem("Just an Admin can do this action");
                    }
                }
                else
                {
                    // User deletes their own account
                    ltUser = await _context.Users.FindAsync(currentUser.Id);
                    if (ltUser == null)
                    {
                        return NotFound();
                    }
                }
                _context.Users.Remove(ltUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Problem("Invalid identity");
        }

        private bool LTUserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
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
