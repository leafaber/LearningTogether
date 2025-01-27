namespace LearningTogether.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public LTUser User { get; set; }
        public Course Course { get; set; }
    }
}
