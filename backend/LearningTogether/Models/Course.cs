using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace LearningTogether.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [JsonIgnore]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public int CreatorId { get; set; }
        [Display(Name = "Course Name")]
        public string? CourseName { get; set; } // should be not NULL
        public string? ECTS { get; set; }
        public float Price { get; set; }
        public bool Available { get; set; }
        [Display(Name = "Live Chat enabled")]
        public bool LCEnabled { get; set; }
        //[JsonIgnore] // won't be send in the GET request, otherwise it would create an 'include reference' loop
        public ICollection<Comment>? Comments { get; set; }
        [JsonIgnore] // for now Participants data won't be sent, I will probably make a list with user id's that are sent 
        public ICollection<Enrollment>? Participants { get; set; }
        public ICollection<Chapter>? Chapters{ get; set; }
        [JsonIgnore]
        public ICollection<Rating>? Ratings{ get; set; }
        [JsonIgnore]
        public LTUser? Creator { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
