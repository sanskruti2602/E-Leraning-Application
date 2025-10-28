using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Leraning_Application.Pages.Instructor
{
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _context;

        public DashboardModel(AppDbContext context)
        {
            _context = context;
        }

        // Public properties for binding to the view
        public List<Course> InstructorCourses { get; set; } = new();
        public int TotalCourses { get; set; }
        public int TotalLearners { get; set; }
        public int TotalChapters { get; set; }
        public string MostPopularCourse { get; set; } = "N/A";

        // Favourites (most enrolled course)
        public string MostLovedCourseTitle { get; set; } = "N/A";
        public int MostLovedCourseCount { get; set; } = 0;

        public async Task<IActionResult> OnGetAsync()
        {
            // ✅ Get instructor ID from session
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? role = HttpContext.Session.GetString("UserRole");

            if (userId == null || role != "Instructor")
                return RedirectToPage("/Account/Login");

            int instructorId = userId.Value;

            // ✅ Load courses with enrollments and lessons
            InstructorCourses = await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .Include(c => c.Enrollments)
                .Include(c => c.Lessons)
                .ToListAsync();

            // ✅ Total courses created by this instructor
            TotalCourses = InstructorCourses.Count;

            // ✅ Total distinct learners enrolled in any of instructor’s courses
            TotalLearners = InstructorCourses
                .SelectMany(c => c.Enrollments)
                .Select(e => e.UserId)
                .Distinct()
                .Count();

            // ✅ Total chapters across all their courses
            TotalChapters = InstructorCourses.Sum(c => c.Lessons.Count);

            // ✅ Most popular course by enrollment count
            MostPopularCourse = InstructorCourses
                .OrderByDescending(c => c.Enrollments.Count)
                .FirstOrDefault()?.Title ?? "N/A";

            // ✅ Favourites card logic: most loved course
            var mostLoved = InstructorCourses
                .OrderByDescending(c => c.Enrollments.Count)
                .FirstOrDefault();

            if (mostLoved != null)
            {
                MostLovedCourseTitle = mostLoved.Title;
                MostLovedCourseCount = mostLoved.Enrollments.Count;
            }

            return Page();
        }
    }
}
