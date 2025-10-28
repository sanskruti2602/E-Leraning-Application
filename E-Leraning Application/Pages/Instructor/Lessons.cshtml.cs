using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;

namespace E_Leraning_Application.Pages.Instructor
{
    public class LessonsModel : PageModel
    {
        private readonly AppDbContext _context;

        public LessonsModel(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, List<Lesson>> CourseLessons { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            int? instructorId = HttpContext.Session.GetInt32("UserId");
            string? role = HttpContext.Session.GetString("UserRole");

            if (instructorId == null || role != "Instructor")
                return RedirectToPage("/Account/Login");

            var courses = await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .Include(c => c.Lessons)
                .ToListAsync();

            CourseLessons = courses
                .Where(c => c.Lessons != null && c.Lessons.Any())
                .ToDictionary(
                    c => c.Title,
                    c => c.Lessons.OrderBy(l => l.Title).ToList()
                );

            return Page();
        }
    }
}
