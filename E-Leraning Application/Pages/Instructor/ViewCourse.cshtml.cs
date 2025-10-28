using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Leraning_Application.Pages.Instructor
{
    public class ViewCourseModel : PageModel
    {
        private readonly AppDbContext _context;

        public ViewCourseModel(AppDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }
        public List<Lesson> Lessons { get; set; }
        public int LearnerCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Course == null)
            {
                return NotFound();
            }

            Lessons = Course.Lessons.ToList();
            LearnerCount = Course.Enrollments
                .Select(e => e.UserId)
                .Distinct()
                .Count();

            return Page();
        }
    }
}
