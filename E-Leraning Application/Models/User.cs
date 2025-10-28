namespace E_Leraning_Application.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }  // "Admin", "Instructor", "Learner"

        public bool IsVerified { get; set; } = false;

        // Navigation Properties
        public ICollection<Course> CoursesCreated { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
