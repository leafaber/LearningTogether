using LearningTogether.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LearningTogether.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<LTUser> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        modelBuilder.Entity<LTUser>().ToTable("User");
        modelBuilder.Entity<Comment>().ToTable("Comment");
        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Subscription>().ToTable("Subscription");
        modelBuilder.Entity<Chapter>().ToTable("Chapter");
        modelBuilder.Entity<Material>().ToTable("Material");
        modelBuilder.Entity<Rating>().ToTable("Rating");
        
        // combied key from 2 primary keys
        modelBuilder.Entity<Rating>()
            .HasKey(c => new { c.CourseId, c.UserId});
    }
}
