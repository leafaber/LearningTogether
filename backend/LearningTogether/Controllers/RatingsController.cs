using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;
using Microsoft.AspNetCore.Authorization;

namespace LearningTogether.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Rating>> PostRating([FromForm]string courseName,[FromForm] string mark)
        {
          if (_context.Ratings == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Ratings'  is null.");
          }
          
          var point = int.Parse(mark);
          if (point != 1 && point != -1 && point != 0) return Problem("Invalid mark value, mark [-1, 1]");
          
          var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
          if (course == null)
          {
              return Problem("Invalid course name");
          }
          var identity = HttpContext.User.Identity as ClaimsIdentity;
          if (identity != null)
          {
              var currentUser = CoursesController.GetCurrentUser(identity);
              // Checking if the user already rated the course, if they did - delete the previous record and write a new one
              Rating rating = _context.Ratings.FirstOrDefault(r => r.CourseId == course.CourseId && r.UserId == currentUser.Id);
              if (rating != null)
              {
                  _context.Ratings.Remove(rating);
                  await _context.SaveChangesAsync(); 
              }

              if (point != 0)
              {
                  rating = new Rating { CourseId = course.CourseId, UserId = currentUser.Id, Mark = point };
                  _context.Ratings.Add(rating);
              }
              
              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateException)
              {
                  if (RatingExists(rating.CourseId, rating.UserId))
                  {
                      return Conflict();
                  }
                  else
                  {
                      throw;
                  }
              }
              return Ok();
          }
          return Problem("Invalid identity");
        }

        // DELETE: api/ratings
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteRating([FromForm]string courseName)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);

                var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
                if (course == null)
                {
                    return Problem("Invalid course name");
                }

                var rating = _context.Ratings.FirstOrDefault(c => c.CourseId == course.CourseId && c.UserId == currentUser.Id);
                if (rating == null)
                {
                    return NotFound();
                }

                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return Problem("Invalid identity");
        }
        
        private bool RatingExists(int courseId, int userId)
        {
            return (_context.Ratings?.Any(e => e.CourseId == courseId && e.UserId == userId)).GetValueOrDefault();
        }
    }
}
