using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;

namespace E_Leraning_Application.Pages.Instructor
{
    public class LearnersModel : PageModel
    {
        private readonly AppDbContext _context;

        public LearnersModel(AppDbContext context)
        {
            _context = context;
        }

        public class LearnerDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public List<string> CourseTitles { get; set; } = new();
        }

        public List<LearnerDto> Learners { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            int? instructorId = HttpContext.Session.GetInt32("UserId");
            string? role = HttpContext.Session.GetString("UserRole");

            if (instructorId == null || role != "Instructor")
                return RedirectToPage("/Account/Login");

            Learners = await _context.Enrollments
                .Where(e => e.Course.InstructorId == instructorId)
                .Include(e => e.Course)
                .Include(e => e.User)
                .GroupBy(e => e.UserId)
                .Select(g => new LearnerDto
                {
                    FullName = g.First().User.FullName,
                    Email = g.First().User.Email,
                    CourseTitles = g.Select(e => e.Course.Title).Distinct().ToList()
                })
                .ToListAsync();

            return Page();
        }
    }
}
