using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Leraning_Application.Pages.Instructor
{
    public class CourseListModel : PageModel
    {
        private readonly AppDbContext _context;

        public CourseListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Account/Login");

            Courses = await _context.Courses
                .Where(c => c.InstructorId == userId)
                .Include(c => c.Lessons)
                .ToListAsync();

            return Page();
        }
    }
}
