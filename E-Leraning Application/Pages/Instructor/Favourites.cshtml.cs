using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace E_Leraning_Application.Pages.Instructor
{
    public class FavouritesModel : PageModel
    {
        private readonly AppDbContext _context;

        public FavouritesModel(AppDbContext context)
        {
            _context = context;
        }

        public Course? MostLovedCourse { get; set; }
        public int EnrollmentCount { get; set; }
        public List<User> EnrolledStudents { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            int? instructorId = HttpContext.Session.GetInt32("UserId");
            string? role = HttpContext.Session.GetString("UserRole");

            if (instructorId == null || role != "Instructor")
                return RedirectToPage("/Account/Login");

            // Get the instructor's courses with enrollments
            var instructorCourses = await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.User)
                .ToListAsync();

            MostLovedCourse = instructorCourses
                .OrderByDescending(c => c.Enrollments.Count)
                .FirstOrDefault();

            if (MostLovedCourse != null)
            {
                EnrollmentCount = MostLovedCourse.Enrollments.Count;
                EnrolledStudents = MostLovedCourse.Enrollments
                    .Select(e => e.User)
                    .ToList();
            }

            return Page();
        }
    }
}
