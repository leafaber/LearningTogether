using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;
using LearningTogether.Models.CombinedModels;
using Microsoft.AspNetCore.Authorization;

namespace LearningTogether.Controllers
{
    [Route("api/courses/{courseName}")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/courses/courseName
        // returns all chapters of the course, without the materials, maybe add just material names later
        [HttpGet]
        public async Task<ActionResult<CoursesCommentsRating>> GetCourse(string courseName)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            /* All binaries are sent, probably will need changing:
             *  Only send file names, and then when clicking on the name send that material's binary
            */
            var course = _context.Courses.Include(c=> c.Comments)
                .Include(c => c.Chapters)
                .FirstOrDefault(c => c.CourseName == courseName);

            if (course == null)
            {
                return NotFound();
            }
            // later can be made more efficient by storing the full name in the DB
           
            CoursesCommentsRating ccr = new CoursesCommentsRating
            {
                CreatorName = _context.Users.FirstOrDefault(u => u.Id == course.CreatorId).FullName,
                CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == course.CategoryId).CatName,
                Course = course,
                Rating = CalculateRating(course.CourseId)
            };
            return ccr;
        }
        
        // Creating a chapter
        // POST: api/courses/courseName
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostChapter(string courseName, [FromForm]string chapterName, [FromForm]string description)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var course = _context.Courses.FirstOrDefault(c => c.CourseName == courseName);
                if (course == null || course.CreatorId != currentUser.Id) Problem("Invalid course name");
                
                var chapter = await _context.Chapters.Include(ch => ch.Course)
                    .FirstOrDefaultAsync(ch => ch.Course == course && ch.ChapterName == chapterName);
                if (chapter != null) Problem("Chapter already exists");

                try
                {
                    _context.Chapters.Add(new Chapter{ Course = course, ChapterName = chapterName, Description = description });
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
        
        // Deleting a chapter
        // POST api/courses/courseName
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteChapter(string courseName, [FromForm]string chapterName)
        {
            if (_context.Chapters == null) return NotFound();
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var chapter = await _context.Chapters.Include(ch => ch.Course)
                    .Include(ch => ch.Materials)
                    .FirstOrDefaultAsync(ch => ch.Course.CourseName == courseName && ch.ChapterName == chapterName);

                if (chapter != null && (chapter.Course.CreatorId == currentUser.Id || currentUser.Role == "Admin"))
                {
                    _context.Chapters.Remove(chapter);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return Problem("Invalid identity");
        }
        
        // GET api/courses/courseName/chapterName
        // returning course chapter's materials
        [HttpGet("{chapterName}")]  // getting individual chapters
        [Authorize] // only authorized user can access materials
        public async Task<ActionResult<Chapter>> GetCourseChapters(string courseName, string chapterName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);   // not used 
                var chapter = await _context.Chapters.Include(ch => ch.Course)
                    .Include(ch => ch.Materials)
                    .FirstOrDefaultAsync(ch => ch.Course.CourseName == courseName && ch.ChapterName == chapterName);

                if (chapter == null) return NotFound();

                return chapter;
            }
            return Problem("Invalid identity");
        }
        
        
        // adding materials to a chapter
        [HttpPost("{chapterName}")]  
        [Authorize] 
        public async Task<ActionResult> PostMaterial(string courseName, string chapterName, [FromForm]IFormFile? file)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var chapter = await _context.Chapters.Include(ch => ch.Course)
                    .FirstOrDefaultAsync(ch => ch.Course.CourseName == courseName && ch.ChapterName == chapterName);
                if (chapter == null || chapter.Course.CreatorId != currentUser.Id) return NotFound();
                
                if (file != null)
                {
                    string fileName = file.FileName;
                    var material = await _context.Materials
                        .FirstOrDefaultAsync(m => m.MaterialName == fileName && m.ChapterId == chapter.ChapterId);
                    if (material != null)   // check if material with this name already exists in this chapter
                    {
                        return Problem("Material with this name already exists");
                    }
                    if (file.Length > 0)
                    {
                        // creating a byte array
                        var newMaterial = new Material { Chapter = chapter, MaterialName = fileName };
                        using (var target = new MemoryStream())
                        {
                            file.CopyTo(target);
                            newMaterial.Content = target.ToArray();
                        }
                        // saving the file
                        _context.Materials.Add(newMaterial);
                        await _context.SaveChangesAsync();
                    }
                    return Ok();
                }
                return Problem("No file sent");
            }
            return Problem("Invalid identity");
        }
        
        // deleting materials from a chapter
        [HttpDelete("{chapterName}")]  
        public async Task<ActionResult> DeleteMaterial(string courseName, string chapterName, [FromForm]string fileName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var material = await _context.Materials
                    .Include(m => m.Chapter).ThenInclude(m => m.Course)
                    .FirstOrDefaultAsync(m => m.Chapter.Course.CourseName == courseName && m.Chapter.ChapterName == chapterName && m.MaterialName == fileName);
                if (material == null || material.Chapter.Course.CreatorId != currentUser.Id) return NotFound();
                try
                {
                    _context.Materials.Remove(material);
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
        
        // DOWNLOADING A FILE/ MATERIAL 
        [HttpGet("{chapterName}/{fileName}")] // getting individual chapters
        [Authorize] // only authorized user can access materials
        public async Task<ActionResult<Material>> DownloadFile(string courseName, string chapterName, string fileName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var chapter = await _context.Chapters.Include(ch => ch.Course)
                    .FirstOrDefaultAsync(ch => ch.Course.CourseName == courseName && ch.ChapterName == chapterName);
                if (chapter == null || chapter.Course.CreatorId != currentUser.Id) return NotFound();

                var material = await _context.Materials.FirstOrDefaultAsync(m =>
                    m.MaterialName == fileName && m.ChapterId == chapter.ChapterId);
                if (material == null) return Problem("Invalid file name");
                
                return material;
            }
            return Problem("Invalid identity");
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
    }
}
