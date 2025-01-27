using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LearningTogether.Controllers;
using NUnit.Framework;
using LearningTogether.Data;
using LearningTogether.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningTogether.Tests;

public class Tests
{
    
    private DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;
    private CoursesController coursesController;
    private CommentsController commentsController;

    [SetUp]
    public void Setup()
    {
        // filling the in memory database with some data
        using var context = new ApplicationDbContext(dbContextOptions);
        DbInitializer.Initialize(context); 

        coursesController = new CoursesController(new ApplicationDbContext(dbContextOptions));
        commentsController = new CommentsController(new ApplicationDbContext(dbContextOptions));
    }
    
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    [Test]
    public async Task GetAllCourses_shouldtGiveNull()
    {
        var result = await coursesController.GetCourses();
        var ccrs = result.Value.GetEnumerator();
        Debug.Assert(ccrs != null, $"Can't receive courses' data");
    }
    
    [Test]
    public async Task PostComment_ShouldBeSuccessful()
    {
        bool correct = true;
        int userId = 5;
        string courseName = "Calculus";
        string content = "New comment";
        using var context = new ApplicationDbContext(dbContextOptions);
        var course = context.Courses.FirstOrDefault(c => c.CourseName == courseName);

        var user = context.Users.FirstOrDefault(u => u.Id == userId);

        if (course == null || user == null)
        {
            correct = false;
        }
        else
        {
            Comment comment = new Comment{Course = course, AuthorId = user.Id, ComContent = content, AuthorName = user.FullName};
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }
        
        Debug.Assert(correct, $"Comment not posted");
    }
    
    [Test]
    public async Task DeleteComment_AsAuthor()
    {
        await AddUserComment();
        bool correct = true;
        int commentId = 20;
        int userId = 5;
        using var context = new ApplicationDbContext(dbContextOptions);
 
        var comment = await context.Comments.FindAsync(commentId);
        var user = context.Users.FirstOrDefault(u => u.Id == userId);
        
        if(user == null || comment == null) Debug.Assert(false, $"Invalid user or comment id");
        if (comment.AuthorId == user.Id || user.Role == "Admin")
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();

        }
        else { correct = false; }
        Debug.Assert(correct, $"Comment not deleted");
    }
    
    [Test]
    public async Task DeleteComment_AsAdmin()
    {
        await AddUserComment();
        bool correct = true;
        int commentId = 20;
        int userId = 1; // admins id
        using var context = new ApplicationDbContext(dbContextOptions);
 
        var comment = await context.Comments.FindAsync(commentId);
        var user = context.Users.FirstOrDefault(u => u.Id == userId);
        
        if(user == null || comment == null) Debug.Assert(false, $"Invalid user or comment id");
        if (comment.AuthorId == user.Id || user.Role == "Admin")
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();

        }
        else { correct = false; }
        Debug.Assert(correct, $"Comment not deleted");
    }
    
    [Test]
    public async Task DeleteComment_Unsuccessful_notAnAdminOrAnAuthor()
    {
        await AddUserComment();
        bool cantDelete = false;
        int commentId = 20;
        int userId = 3; // some other user's id
        using var context = new ApplicationDbContext(dbContextOptions);
 
        var comment = await context.Comments.FindAsync(commentId);
        var user = context.Users.FirstOrDefault(u => u.Id == userId);
        
        if(user == null || comment == null) Debug.Assert(false, $"Invalid user or comment id");
        if (comment.AuthorId == user.Id || user.Role == "Admin")
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }
        else { cantDelete = true; }
        Debug.Assert(cantDelete, $"Comment deleted");
    }

    private async Task AddUserComment()
    {
        int commentId = 20;
        int userId = 5;
        using var context = new ApplicationDbContext(dbContextOptions);
        
        var com = await context.Comments.FindAsync(commentId);
        if (com != null)
        {
            context.Comments.Remove(com);
            await context.SaveChangesAsync();
        }
        
        var comment = new Comment
        {
            CommentId = commentId, AuthorId = userId, CourseId = 4, ComContent = "New comment",
            AuthorName = context.Users.FirstOrDefault(u => u.Id == userId).FullName
        };
        
        context.Comments.Add(comment);
        context.SaveChanges();
    }
    
}
