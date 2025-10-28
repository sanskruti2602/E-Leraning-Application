namespace E_Leraning_Application.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;
        public float Progress { get; set; } = 0.0f;

    }
}
