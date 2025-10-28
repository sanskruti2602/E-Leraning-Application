using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace E_Leraning_Application.Pages.Instructor
{
    public class AddLessonModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddLessonModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lesson Lesson { get; set; } = new();

        public List<SelectListItem> CourseOptions { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? instructorId = HttpContext.Session.GetInt32("UserId");

            if (instructorId == null)
                return RedirectToPage("/Account/Login");

            var courses = await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync();

            CourseOptions = courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? instructorId = HttpContext.Session.GetInt32("UserId");

            if (instructorId == null)
                return RedirectToPage("/Account/Login");

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == Lesson.CourseId && c.InstructorId == instructorId);

            if (course == null)
            {
                ErrorMessage = "You're not instructor of the selected course.";
                await OnGetAsync(); // reload course dropdown
                return Page();
            }

            _context.Lessons.Add(Lesson);
            await _context.SaveChangesAsync();

            SuccessMessage = "Lesson added successfully!";
            Lesson = new Lesson(); // Clear form fields
            await OnGetAsync();
            return Page();
        }
    }
}
