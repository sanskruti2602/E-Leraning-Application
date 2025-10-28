using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Leraning_Application.Models;
using E_Leraning_Application.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace E_Leraning_Application.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and password are required.";
                return Page();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == Email.Trim());

            if (user == null || user.PasswordHash != Password.Trim()) // In real apps: use hashing
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            // ✅ Set session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);

            // ✅ Role-based redirection
            switch (user.Role)
            {
                case "Student":
                    return RedirectToPage("/Student/Dashboard");

                case "Instructor":
                    return RedirectToPage("/Instructor/Dashboard");

                case "Admin":
                    return RedirectToPage("/Admin/Dashboard");

                default:
                    return RedirectToPage("/Index");
            }
        }
    }
}
