using System.Text.Json.Serialization;

namespace LearningTogether.Models
{
    public class Material
    {
        [JsonIgnore]
        public int MaterialId { get; set; }
        [JsonIgnore]
        public int ChapterId { get; set; }
        public string MaterialName { get; set; }
        public byte[] ? Content { get; set; } // material content - BLOB
        [JsonIgnore]
        public Chapter Chapter { get; set; }
    }
}
