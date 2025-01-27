using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearningTogether.Models
{
    public class LTUser
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Pass Hash")]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public string? IBAN { get; set; }
        public string FullName {
            get { return FirstName + " " + LastName; }
        }

        public ICollection<Course>? CreatedCourses { get; set; }
        public ICollection<Comment>? LeftComments { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<Rating>? Ratings{ get; set; }
    }
}
