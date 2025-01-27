using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningTogether.Data;
using LearningTogether.Models;
using Microsoft.AspNetCore.Authorization;

namespace LearningTogether.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
          return  await _context.Categories.Include(c => c.Courses).ToListAsync();
        }
        
        // GET: api/categories/id
        [HttpGet("{categoryName}")]
        public async Task<ActionResult<Category>> GetCategory([FromForm]string categoryName)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
          var category = await _context.Categories.Include(c => c.Courses)
              .FirstOrDefaultAsync(c => c.CatName == categoryName);

          if (category == null)
           {
               return NotFound();
           }
            
           return category;
        }
        
        // POST: api/categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostCategory([FromForm] string categoryName, [FromForm] IFormFile? file)
        {
            string msg = "Category already exists";
            // For now it just sends all data, later it will be used to insert new category depending
          if (_context.Categories == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
          }
          
          var category = _context.Categories.FirstOrDefault(c => c.CatName == categoryName);
          if (category == null)
          {
              msg = "New category created";
              category = new Category{CatName = categoryName, CatImg = null};
              _context.Categories.Add(category);
              await _context.SaveChangesAsync();
          }
	
	    /* if the category (categoryName) that already exists is sent, 
	    * image of that category is replaced with the new one.
        */
          if (file != null)
          {
              if (file.Length > 0)
              {
                  //Getting file Extension
                  var fileExtension = Path.GetExtension(file.FileName);
                  // checking if it's an extension for an image
                  List<string> extensions = new List<string>() {".jpg", ".jpeg", ".png", ".gif"};
                  if(extensions.Contains(fileExtension))
                  {
                      // creating a byte array
                      using (var target = new MemoryStream())
                      {
                          file.CopyTo(target);
                          category.CatImg = target.ToArray();
                      }
                      // saving / updating the image
                      _context.Update(category);
                      await _context.SaveChangesAsync();
                  }
              }
         }
          return Ok(msg);
        }

        // DELETE: api/categories
 
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromForm] string categoryName)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var category = _context.Categories.FirstOrDefault(c => c.CatName == categoryName && currentUser.Role == "Admin");
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Problem("Invalid identity");
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
        
        // SUBSCRIPTIONS ENDPOINT api/categories/subscribed
        // remove subscriptions controller
        [HttpGet("subscribed")]
        [Authorize]
        public ActionResult<IEnumerable<Category?>> SubscriptionsEndpoint()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var categories = _context.Subscriptions.Include(s => s.Category)
                    .Where(s => s.UserId == currentUser.Id)
                    .Select(s => s.Category).ToList();
                return categories;
            }
            return Problem("Invalid identity");
        }
        
        [HttpPost("subscribed")]
        [Authorize]
        public async Task<ActionResult<Subscription>> PostSubscription([FromForm]string categoryName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var category = _context.Categories.FirstOrDefault(c => c.CatName == categoryName);
                if (category == null) return NotFound("Category name not found");
                
                try
                {   
                    if(_context.Subscriptions.FirstOrDefault(s => s.CategoryId == category.CategoryId && s.UserId == currentUser.Id) == null)
                        _context.Subscriptions.Add(new Subscription{UserId = currentUser.Id, CategoryId = category.CategoryId});
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
        
        [HttpDelete("subscribed")]
        [Authorize]
        public async Task<ActionResult<Subscription>> DeleteSubscription([FromForm]string categoryName)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var category = _context.Categories.FirstOrDefault(c => c.CatName == categoryName);
                if (category == null) return NotFound("Category name not found");
                var subscription = _context.Subscriptions.FirstOrDefault(s =>
                    s.UserId == currentUser.Id && s.CategoryId == category.CategoryId);
                if (subscription == null) return NotFound("Subscription not found");
                
                try
                {
                    _context.Subscriptions.Remove(subscription);
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

        [HttpGet("notsubscribed")]
        [Authorize]
        public ActionResult<IEnumerable<Category?>> GetNotYetSubscribedCategories()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUser = CoursesController.GetCurrentUser(identity);
                var categories = _context.Categories
                                    .Except(_context.Subscriptions.
                                                Include(s => s.Category)
                                                .Where(s => s.User == currentUser)
                                                .Select(s => s.Category))
                                .ToList();
                return categories;
            }
            return Problem("Invalid identity");
        }
    }
}
