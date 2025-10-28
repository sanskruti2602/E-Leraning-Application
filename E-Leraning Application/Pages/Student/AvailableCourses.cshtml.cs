using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Leraning_Application.Pages
{
    public class AvailableCourseModel : PageModel
    {
        private readonly AppDbContext _context;

        public AvailableCourseModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get; set; }

        [BindProperty]
        public int CourseId { get; set; }

        // To show messages per course after enrollment
        public Dictionary<int, string> EnrollmentMessages { get; set; } = new();

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Instructor)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null || sessionUserId == 0)
            {
                // User not logged in, redirect to login page
                return Redirect("/Account/Login");
            }
            int userId = sessionUserId.Value;

            // Check if already enrolled
            bool alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.CourseId == CourseId && e.UserId == userId);

            if (!alreadyEnrolled)
            {
                Enrollment enrollment = new Enrollment
                {
                    UserId = userId,
                    CourseId = CourseId,
                    EnrolledOn = DateTime.UtcNow,
                    Progress = 0f
                };
                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                EnrollmentMessages[CourseId] = "You have enrolled successfully!";
            }
            else
            {
                EnrollmentMessages[CourseId] = "You are already enrolled in this course.";
            }

            Courses = await _context.Courses
                .Include(c => c.Instructor)
                .ToListAsync();

            return Page();
        }
    }
}
