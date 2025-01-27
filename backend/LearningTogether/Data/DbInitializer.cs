using LearningTogether.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;

namespace LearningTogether.Data;
// Filling th DB with some data
public class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Look for any user.
        if (!context.Users.Any())  
        {
            var users = new LTUser[]	// PASSWORD FOR EVERY USER IS 123
            {
                new LTUser{FirstName="Admin",LastName="Adminko",Email="admin@gmail.com", Role = "Admin", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Carson",LastName="Alexander", Email="ja.netko@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Meredith",LastName="Alonso",Email="ja.netko2@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Arturo",LastName="Anand", Email="ja.netko3@gmail.com", IBAN="HR789456123111", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Gytis",LastName="Barzdukas", Email="ja.netko4@gmail.com", PhoneNumber= "0987654321", IBAN="HR789434823101", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Yan",LastName="Li",Email="ja.netko5@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Peggy",LastName="Justice", Email="ja.netko6@gmail.com", PhoneNumber="0981234567", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Laura",LastName="Norman", Email="ja.netko7@gmail.com", PhoneNumber = "09781234569", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Nino",LastName="Olivetto",Email="ja.netko8@gmail.com", IBAN="HR789456123101", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Miro",LastName="Merlin", Email="ja.netko9@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Filip",LastName="Lovrov", Email="ja.netko10@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Jenny",LastName="Black", Email="ja.netko11@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Sandra",LastName="Horvat", Email="ja.netko12@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Sunny",LastName="Varga", Email="ja.netko13@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Maxim",LastName="Novak", Email="ja.netko12@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"},
                new LTUser{FirstName="Franka",LastName="Repov", Email="ja.netko12@gmail.com", Role = "PubUser", PasswordHash = "oudaCubzWVIMjTxaQh1KT85fn+p2KjQR"}
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
        
        if (!context.Categories.Any())
        {
            var categories = new Category[]
            {
                new Category{CatName="Science"},
                new Category{CatName="Finances"},
                new Category{CatName="Mathematics"},
                new Category{CatName="Music"},
                new Category{CatName="Literature"},
                new Category{CatName="Drama"},
                new Category{CatName="Art"},
                new Category{CatName="Sport"}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        if (!context.Subscriptions.Any())
        {
            var subscriptions = new Subscription[]
            {
                new Subscription{UserId = 4, CategoryId = 2},
                new Subscription{UserId = 4, CategoryId = 3},
                new Subscription{UserId = 5, CategoryId = 3},
                new Subscription{UserId = 6, CategoryId = 4},
                new Subscription{UserId = 7, CategoryId = 1},
                new Subscription{UserId = 7, CategoryId = 3},
                new Subscription{UserId = 8, CategoryId = 5},
                new Subscription{UserId = 10, CategoryId = 6}
            };

            context.Subscriptions.AddRange(subscriptions);
            context.SaveChanges();
        }
        if (!context.Courses.Any())
        {
                var courses = new Course[]
            {
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=3, CategoryId = 2,CourseName="Microeconomics"},
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=3, CategoryId = 1,CourseName="Chemistry"},
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=4, CategoryId = 2,CourseName="Macroeconomics"},
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=5, CategoryId = 3,CourseName="Calculus"},
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=5, CategoryId = 3,CourseName="Trigonometry"},
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=8, CategoryId = 4,CourseName="Composition"},
                new Course{Available=true,LCEnabled = false, Price=0,ECTS = "One month", CreatorId=8, CategoryId = 5,CourseName="Literature"}
            };

                context.Courses.AddRange(courses);
                context.SaveChanges();
        }

        if (!context.Enrollments.Any())
        {
            var enrollments = new Enrollment[]
            {
                new Enrollment{UserId=3,CourseId=2},
                new Enrollment{UserId=4,CourseId=1},
                new Enrollment{UserId=4,CourseId=2},
                new Enrollment{UserId=4,CourseId=5},
                new Enrollment{UserId=4,CourseId=7},
                new Enrollment{UserId=5,CourseId=4},
                new Enrollment{UserId=6,CourseId=6},
                new Enrollment{UserId=6,CourseId=7},
                new Enrollment{UserId=7,CourseId=4},
                new Enrollment{UserId=7,CourseId=5},
                new Enrollment{UserId=8,CourseId=1},
                new Enrollment{UserId=8,CourseId=2},
                new Enrollment{UserId=9,CourseId=4},
                new Enrollment{UserId=10,CourseId=1},
                new Enrollment{UserId=10,CourseId=3},
                new Enrollment{UserId=10,CourseId=4},
                new Enrollment{UserId=10,CourseId=5},
            };

                context.Enrollments.AddRange(enrollments);
                context.SaveChanges();
        }
        
        if (!context.Comments.Any())
        {
            var comments = new Comment[]
            {
                new Comment{AuthorId=8,CourseId=1, ComContent = "Awesome!" , AuthorName = context.Users.FirstOrDefault(u => u.Id == 8).FullName},
                new Comment{AuthorId=3,CourseId=2, ComContent = "Could be better", AuthorName = context.Users.FirstOrDefault(u => u.Id == 3).FullName},
                new Comment{AuthorId=3,CourseId=2, ComContent = "But it's ok I guess", AuthorName = context.Users.FirstOrDefault(u => u.Id == 3).FullName},
                new Comment{AuthorId=4,CourseId=7, ComContent = "Excellent tutorial", AuthorName = context.Users.FirstOrDefault(u => u.Id == 4).FullName},
                new Comment{AuthorId=5,CourseId=4, ComContent = "Thank you very much on this course", AuthorName = context.Users.FirstOrDefault(u => u.Id == 5).FullName},
                new Comment{AuthorId=6,CourseId=6, ComContent = "It's hard, but it can be learned :)", AuthorName = context.Users.FirstOrDefault(u => u.Id == 6).FullName},
                new Comment{AuthorId=6,CourseId=7, ComContent = "Explained very well", AuthorName = context.Users.FirstOrDefault(u => u.Id == 6).FullName},
                new Comment{AuthorId=7,CourseId=5, ComContent = "Very bad course, don't even bother wasting your time on this one >:(", AuthorName = context.Users.FirstOrDefault(u => u.Id == 7).FullName},
                new Comment{AuthorId=8,CourseId=2, ComContent = "Very good course!", AuthorName = context.Users.FirstOrDefault(u => u.Id == 8).FullName},
                new Comment{AuthorId=9,CourseId=4, ComContent = "Best one yet!", AuthorName = context.Users.FirstOrDefault(u => u.Id == 9).FullName},
                new Comment{AuthorId=10,CourseId=1, ComContent = "So good!", AuthorName = context.Users.FirstOrDefault(u => u.Id == 10).FullName},
                new Comment{AuthorId=10,CourseId=1, ComContent = "I like it very much!", AuthorName = context.Users.FirstOrDefault(u => u.Id == 10).FullName},
                new Comment{AuthorId=10,CourseId=3, ComContent = "Very educational", AuthorName = context.Users.FirstOrDefault(u => u.Id == 10).FullName},
                new Comment{AuthorId=10,CourseId=5, ComContent = "Very unclear!", AuthorName = context.Users.FirstOrDefault(u => u.Id == 10).FullName}
            };

            context.Comments.AddRange(comments);
            context.SaveChanges();
        }
        
        if (!context.Ratings.Any())
        {

            var ratings = new Rating[]
            {
                new Rating{UserId=4,CourseId=1, Mark = 1},
                new Rating{UserId=4,CourseId=2, Mark = -1},
                new Rating{UserId=4,CourseId=5, Mark = -1},
                new Rating{UserId=4,CourseId=7, Mark = 1},
                new Rating{UserId=5,CourseId=4, Mark = 1},
                new Rating{UserId=6,CourseId=6, Mark = 1},
                new Rating{UserId=6,CourseId=7, Mark = 1},
                new Rating{UserId=7,CourseId=4, Mark = 1},
                new Rating{UserId=7,CourseId=5, Mark = -1},
                new Rating{UserId=8,CourseId=1, Mark = 1},
                new Rating{UserId=8,CourseId=2, Mark = 1},
                new Rating{UserId=9,CourseId=4, Mark = 1},
                new Rating{UserId=10,CourseId=1, Mark = 1},
                new Rating{UserId=10,CourseId=3, Mark = 1},
                new Rating{UserId=10,CourseId=4, Mark = 1},
                new Rating{UserId=10,CourseId=5, Mark = -1},
            };

            context.Ratings.AddRange(ratings);
            context.SaveChanges();
        }

        if (!context.Chapters.Any())
        {
            var chapters = new Chapter[]
            {
                new Chapter{ChapterName = "First", Description = "Basics of macroeconomics", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Macroeconomics")},
                new Chapter{ChapterName = "First", Description = "Basics of microeconomics", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Microeconomics")},
                new Chapter{ChapterName = "First", Description = "Basics of chemistry", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Chemistry")},
                new Chapter{ChapterName = "First", Description = "Basics of calculus", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Calculus")},
                new Chapter{ChapterName = "First", Description = "Basics of trigonometry", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Trigonometry")},
                new Chapter{ChapterName = "First", Description = "Basics of composition", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Composition")},
                new Chapter{ChapterName = "First", Description = "Basics of literature", Course = context.Courses.FirstOrDefault(c => c.CourseName  == "Literature")},
            };
            
            context.Chapters.AddRange(chapters);
            context.SaveChanges();
        }
    }
}

