using E_Leraning_Application.Data;
using E_Leraning_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Leraning_Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Get user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Add new user
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Update user
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete user
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Get user by email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Validate user login (email + password)
        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            // Compare hashed password (this is simplified)
            return user.PasswordHash == password;
        }

        // Get all instructors (Role = "Instructor")
        public async Task<List<User>> GetInstructorsAsync()
        {
            return await _context.Users
                .Where(u => u.Role.ToLower() == "instructor")
                .ToListAsync();
        }

        // Get all students (Role = "Student")
        public async Task<List<User>> GetStudentsAsync()
        {
            return await _context.Users
                .Where(u => u.Role.ToLower() == "student")
                .ToListAsync();
        }
    }
}
