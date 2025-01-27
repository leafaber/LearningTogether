using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;

namespace LearningTogether.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
         Commented out for now, maybe we'll use it later
         
        // GET: api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            return await _context.Comments.ToListAsync();
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }
            */
        // POST: api/comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Authorize]
        public async Task<ActionResult<Comment>> PostComment([FromForm]string courseName, [FromForm]string content)
        {
          if (_context.Comments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Comments' is null.");
          }

          var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
          if (course == null)
          {
              return Problem("Invalid course Name. Comment can't be stored.");
          }
          // Checking if the user already commented on the course, if they did - delete the previous record and write a new one
          var identity = HttpContext.User.Identity as ClaimsIdentity;
          if (identity != null)
          {
              var currentUser = CoursesController.GetCurrentUser(identity);
                // Autor name needs to be updateable when a user changes it in their profile
                Comment comment = new Comment{Course = course, AuthorId = currentUser.Id, ComContent = content, AuthorName = currentUser.FullName};
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return Ok();
          } 
          return Problem("Invalid identity");
        }
        
        // DELETE: api/comments
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromForm]int commentId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var currentUser = CoursesController.GetCurrentUser(identity);
            if (comment.AuthorId == currentUser.Id || currentUser.Role == "Admin")
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return NoContent();
            } 
            return Problem("This user can't do this action");
        }
        
        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
