using System.Text.Json.Serialization;

namespace LearningTogether.Models
{
    public class Comment
    {
        [JsonIgnore]
        public int CommentId { get; set; }
        [JsonIgnore]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public int CourseId { get; set; }
        public string ComContent { get; set; }
        // full name of the author, if user changes their name, this also need to be changed (maybe use email instead of FName + LName later)
        public string AuthorName{ get; set; }
        [JsonIgnore] // If not ignored it would include a lot of users personal information - what we don't want to do
        public LTUser Author { get; set; }
        [JsonIgnore]
        public Course Course { get; set; }
    }
}
