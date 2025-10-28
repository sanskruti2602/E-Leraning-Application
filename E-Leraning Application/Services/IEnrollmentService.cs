using E_Leraning_Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Leraning_Application.Services
{
    public interface IEnrollmentService
    {
        Task<List<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment> GetEnrollmentByIdAsync(int id);
        Task<List<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
        Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<Enrollment> EnrollUserAsync(Enrollment enrollment);
        Task<bool> UpdateProgressAsync(int enrollmentId, float progress);
        Task<bool> DeleteEnrollmentAsync(int id);
    }
}
