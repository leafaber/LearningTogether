namespace LearningTogether.Models.CombinedModels;

public class CoursesCommentsRating
{
    public string CreatorName { get; set; } 
    public string CategoryName { get; set; } 
    public Course Course { get; set; }
    // 'Rating' property is filled in CoursesController with the sum of all ratings/marks for that course by users
    public int Rating { get; set; }
    
}