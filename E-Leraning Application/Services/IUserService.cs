using E_Leraning_Application.Models;

namespace E_Leraning_Application.Services
{
    public interface IUserService
    {
        // CRUD
        Task<User?> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);

        // Auth
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserAsync(string email, string password);

        // Role-based filters
        Task<List<User>> GetInstructorsAsync();
        Task<List<User>> GetStudentsAsync();
    }
}
