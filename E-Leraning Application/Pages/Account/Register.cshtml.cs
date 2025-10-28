using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Leraning_Application.Models;         // ✅ Add your model namespace
using E_Leraning_Application.Data;           // ✅ Add your DbContext namespace

namespace E_Leraning_Application.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;   // ✅ Inject DbContext

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public bool IsSuccess { get; set; } = false;

        public void OnGet()
        {
            // Role is bound from query string
        }

        public IActionResult OnPost()
        {
            // ✅ Save user to the database
            var user = new User
            {
                FullName = FullName,
                Email = Email,
                PasswordHash = Password, // Optional: Hash before storing
                Role = Role,
                IsVerified = false
            };

            _context.Users.Add(user);
            _context.SaveChanges(); // ✅ VERY IMPORTANT!

            IsSuccess = true;
            return Page(); // stays on same page to show success message
        }
    }
}
