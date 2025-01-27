using System.Text.Json.Serialization;

namespace LearningTogether.Models
{
    public class Chapter   // previous Unit model
    {
        [JsonIgnore]
        public int ChapterId { get; set; }
        [JsonIgnore]
        public int CourseId { get; set; }
        public string ChapterName { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Course Course { get; set; }
        public ICollection<Material> Materials{ get; set; }
    }
}
