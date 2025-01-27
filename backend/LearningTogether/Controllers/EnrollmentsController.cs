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
using Microsoft.IdentityModel.Tokens;

namespace LearningTogether.Controllers
{
    [Route("api/enrolled")] // checks if the user is enrolled in the course or not
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/enrolled
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<LoginController.AuthenticateUser>> GetEnrollment([FromForm]string courseName)
        {
          if (_context.Enrollments == null)
          {
              return NotFound();
          }
          var authenticateUser = new LoginController.AuthenticateUser(false);
          var identity = HttpContext.User.Identity as ClaimsIdentity;
          if (identity != null)
          {
              var currentUser = CoursesController.GetCurrentUser(identity);
              var enrollment = await _context.Enrollments
                  .Include(s => s.Course)
                  .Where(s => s.UserId == currentUser.Id && s.Course.CourseName == courseName)
                  .ToListAsync();

              if (!enrollment.IsNullOrEmpty())
              {
                  authenticateUser.Exists = true;
              }
          }
          return authenticateUser;
        }

        // GET : api/enrolled/all
        // returns all the courses into which the user is enrolled
        [HttpGet(template:"all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrolledCourses()
        {
            if (_context.Enrollments == null)
            {
                return NotFound();
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var enrolledCourses = await _context.Enrollments
                                    .Include(s => s.Course)
                                    .Where(s => s.UserId == currentUser.Id)
                                    .ToListAsync();

                return enrolledCourses;
            }
            return Array.Empty<Enrollment>();
        }
    }
}
