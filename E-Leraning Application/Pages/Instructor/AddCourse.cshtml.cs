using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;

namespace E_Leraning_Application.Pages.Instructor
{
    public class AddCourseModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddCourseModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // ✅ Manual validation
            if (string.IsNullOrWhiteSpace(Course.Title) || string.IsNullOrWhiteSpace(Course.Description))
            {
                ErrorMessage = "Both Title and Description are required.";
                return Page();
            }

            try
            {
                // ✅ Get email of logged-in user
                var userEmail = User.Identity?.Name;

                if (string.IsNullOrEmpty(userEmail))
                {
                    ErrorMessage = "Unable to identify logged-in instructor.";
                    return Page();
                }

                // ✅ Find user in DB with this email and role
                var instructor = _context.Users.FirstOrDefault(u => u.Email == userEmail && u.Role == "Instructor");

                if (instructor == null)
                {
                    ErrorMessage = "Only instructors can add courses.";
                    return Page();
                }

                // ✅ Assign instructor and timestamp
                Course.InstructorId = instructor.Id;
                Course.CreatedAt = DateTime.UtcNow;

                // ✅ Save to database
                _context.Courses.Add(Course);
                _context.SaveChanges();

                SuccessMessage = "Course added successfully!";
                ModelState.Clear();
                Course = new Course();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Something went wrong while saving the course.";
                Console.WriteLine(ex.Message);
            }

            return Page();
        }
    }
}
