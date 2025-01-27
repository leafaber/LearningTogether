using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearningTogether.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public LTUser? User { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
