using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace LearningTogether.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Display(Name = "Name")]
        public string CatName { get; set; }
        [MaxLength]
        public byte[] ? CatImg { get; set; }
        public ICollection<Course>? Courses { get; set; }
        [JsonIgnore]
        public ICollection<Subscription>? Subscribers { get; set; }
    }
}
