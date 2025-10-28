using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace E_Leraning_Application.Pages.Student
{
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _context;

        public DashboardModel(AppDbContext context)
        {
            _context = context;
        }

        public string StudentName { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        public async Task OnGetAsync()
        {
            // Get logged-in user id from session
            var sessionUserId = HttpContext.Session.GetInt32("UserId");

            if (sessionUserId == null)
            {
                // User not logged in, redirect to login (optional)
                // Response.Redirect("/Account/Login");
                // Or just show empty dashboard
                StudentName = "Guest";
                Enrollments = new List<Enrollment>();
                return;
            }

            int studentId = sessionUserId.Value;

            var student = await _context.Users.FindAsync(studentId);
            StudentName = student?.FullName ?? "Student";

            Enrollments = await _context.Enrollments
                .Where(e => e.UserId == studentId)
                .Include(e => e.Course)
                .ToListAsync();
        }
    }
}
