using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Leraning_Application.Models;
using E_Leraning_Application.Data;

namespace E_Leraning_Application.Pages.Account
{
    public class InstructorRegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public InstructorRegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (_context.Users.Any(u => u.Email == Email))
            {
                ErrorMessage = "Email is already registered.";
                return Page();
            }

            var instructor = new User
            {
                FullName = FullName,
                Email = Email,
                PasswordHash = Password, // You should hash this in real apps
                Role = "Instructor",
                IsVerified = true
            };

            _context.Users.Add(instructor);
            _context.SaveChanges();

            // Set session
            HttpContext.Session.SetInt32("UserId", instructor.Id);
            HttpContext.Session.SetString("UserRole", instructor.Role);

            return RedirectToPage("/Instructor/Dashboard");
        }
    }
}
