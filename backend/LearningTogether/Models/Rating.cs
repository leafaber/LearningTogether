namespace LearningTogether.Models;

public class Rating
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int Mark { get; set; } // can be 1 or -1
    
    public Course Course { get; set; }
    public LTUser User { get; set; }
}