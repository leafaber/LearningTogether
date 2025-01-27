using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;
using LearningTogether.Models.CombinedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace LearningTogether.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoursesCommentsRating>>> GetCourses()
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            /* Getting course and it's comments and ratings info, packing it all up in the
             new "CoursesCommentsRatings" model and making it available to access.            
            */
            var courses = await _context.Courses.Include(c=> c.Comments)
                .Include(c => c.Chapters).ToListAsync();
            
            List<CoursesCommentsRating> ccrList = new List<CoursesCommentsRating>();

            foreach (var course in courses)
          {
              
              CoursesCommentsRating ccr = new CoursesCommentsRating
              {
                  CreatorName = _context.Users.FirstOrDefault(u => u.Id == course.CreatorId).FullName,
                  CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == course.CategoryId).CatName,
                  Course = course,
                  Rating = CalculateRating(course.CourseId)
              };
              ccrList.Add(ccr);
          }
          return ccrList;
        }

        // POST: api/courses
        // Course creation
        // TO DO: add max participants num and replace categoryId with categoryName
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostCourse([FromForm] string courseName, [FromForm] string categoryName, [FromForm] string? ECTS, [FromForm] float price) 
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Courses' is null.");
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);
                var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
                var category = _context.Categories.FirstOrDefault(c => c.CatName == categoryName);
                if (category == null) Problem("Invalid category name");
                if (course == null) {
                  _context.Courses.Add(new Course{Available=true,LCEnabled = false, Price=price, ECTS = ECTS, CreatorId=currentUser.Id, 
                      Category = category, CourseName=courseName});
                  await _context.SaveChangesAsync();
                  return Ok(); 
                }
                
                return Ok("Course name already taken");
            } 
            return Problem("Invalid identity");
        }

        // DELETE: api/courses
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCourse([FromForm]string courseName)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);
                var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName && (c.CreatorId == currentUser.Id || currentUser.Role == "Admin"));
                if (course == null)
                {
                    return NotFound();
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Problem("Invalid identity");
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }

        private int CalculateRating(int courseId)
        {
            int res = 0;
            var chkRating = _context.Ratings.Where(r => r.CourseId == courseId);
            if (chkRating != null)
            {
                res = chkRating.Sum(r => r.Mark);
            }
            return res;
        }
        
        // OWN COURSES MANAGEMENT - courses user created
        // GET api/courses/own
        [HttpGet("own")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CoursesCommentsRating>>> OwnCoursesEndpoint()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);

                var courses = await _context.Courses.Include(c=> c.Comments)
                    .Include(c => c.Chapters)
                    .Where(c => c.CreatorId == currentUser.Id).ToListAsync();
            
                List<CoursesCommentsRating> ccrList = new List<CoursesCommentsRating>();

                foreach (var course in courses)
                {
                    CoursesCommentsRating ccr = new CoursesCommentsRating
                    {
                        CreatorName = currentUser.FullName,
                        CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == course.CategoryId).CatName,
                        Course = course,
                        Rating = CalculateRating(course.CourseId)
                    };
                    ccrList.Add(ccr);
                }
                return ccrList;
            }
            return Problem("Invalid identity");
        }
        
        /* RECOMMENDED COURSES ENDPOINT - api/courses/recommended
            Sends a top course they are not yet enrolled in from each category user is subscribed to.
        */
        [HttpGet("recommended")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CoursesCommentsRating>>> RecommendedCoursesEndpoint()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);
                var userCategories = _context.Subscriptions.Include(s => s.Category)
                    .Where(c => c.UserId == currentUser.Id)
                    .Select(s => s.Category).ToList();
                // courses user already enrolled by the user
                var userEnrollments = _context.Enrollments.Include(s => s.Course)
                    .Where(c => c.UserId == currentUser.Id)
                    .Select(s => s.Course).ToList();
                
                var ccrList = new List<CoursesCommentsRating>();
                foreach (var category in userCategories)
                {
                    // picking the top course in the category
                    var courses = _context.Courses.Include(c => c.Creator)
                        .Where(c => c.CategoryId == category.CategoryId && !userEnrollments.Contains(c)  && c.CreatorId != currentUser.Id).ToList();
                    CoursesCommentsRating ccr = new CoursesCommentsRating
                    {
                        CreatorName = "",
                        CategoryName = category.CatName,
                        Rating = 0
                    };
                    foreach (var course in courses)
                    {
                        int rating = CalculateRating(course.CourseId);
                        if (rating > ccr.Rating)
                        {
                            ccr.CreatorName = course.Creator.FullName;
                            ccr.Course = course;
                            ccr.Rating = rating;
                        }
                    }
                    if(ccr.CreatorName != "") ccrList.Add(ccr);
                }
                return ccrList;
            }
            return Problem("Invalid identity");
        }
        // COURSES USER ENROLLED 
        // GET api/courses/enrolled
        [HttpGet("enrolled")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CoursesCommentsRating>>> EnrolledCoursesEndpoint()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);
                var courses = await _context.Enrollments.Include(s => s.Course).ThenInclude(c=> c.Comments)
                    .Include(s => s.Course).ThenInclude(c=> c.Creator)
                        .Where(c => c.UserId == currentUser.Id)
                        .Select(s => s.Course).ToListAsync();
                
                List<CoursesCommentsRating> ccrList = new List<CoursesCommentsRating>();

                foreach (var course in courses)
                {
                    CoursesCommentsRating ccr = new CoursesCommentsRating
                    {
                        CreatorName = course.Creator.FullName,
                        CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == course.CategoryId).CatName,
                        Course = course,
                        Rating = CalculateRating(course.CourseId)
                    };
                    ccrList.Add(ccr);
                }
                return ccrList;
            }
            return Problem("Invalid identity");
        }

        // ENROLLMENT POST - POST api/courses/enrolled
        [HttpPost("enrolled")]
        [Authorize]
        public async Task<ActionResult> PostEnrollment([FromForm]string courseName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);
                var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
                if (course == null) return NotFound("Course name not found");

                try
                {   
                    if(_context.Enrollments.FirstOrDefault(e => e.CourseId == course.CourseId && e.UserId == currentUser.Id) == null)
                        _context.Enrollments.Add(new Enrollment{UserId = currentUser.Id, CourseId = course.CourseId});
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return Ok();
            }
            return Problem("Invalid identity");
        }

        // ENROLLMENT DELETION
        [HttpDelete("enrolled")]
        [Authorize]
        public async Task<ActionResult> DeleteEnrollment([FromForm] string courseName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = GetCurrentUser(identity);
                var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
                if (course == null) return NotFound("Course name not found");
                var enrollment =
                    _context.Enrollments.FirstOrDefault(
                        e => e.CourseId == course.CourseId && e.UserId == currentUser.Id);
                if (enrollment == null) return NotFound("Enrollment not found");
                
                try
                {
                    _context.Enrollments.Remove(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return Ok();
            }
            return Problem("Invalid identity");
        }
        
        public static LTUser GetCurrentUser(ClaimsIdentity identity)
        {
            var userClaims = identity.Claims;
            var userIdString = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return new LTUser
            {
                Id = int.Parse(userIdString),
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                PhoneNumber = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };
        }
    }
}
