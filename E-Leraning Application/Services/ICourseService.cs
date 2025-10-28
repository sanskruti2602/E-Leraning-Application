using System.Collections.Generic;
using System.Threading.Tasks;
using E_Leraning_Application.Models;

namespace E_Leraning_Application.Services
{
    public interface ICourseService
    {
        // 📘 Instructor-only: Get their own courses
        Task<List<Course>> GetCoursesByInstructorAsync(string instructorId);

        // 📘 Admin-only: View all courses
        Task<List<Course>> GetAllCoursesAsync();

        // 📘 Shared: Get one course by ID
        Task<Course> GetCourseByIdAsync(int courseId);

        // ➕ Instructor/Admin: Add a new course
        Task AddCourseAsync(Course course);

        // 📝 Instructor/Admin: Update an existing course
        Task UpdateCourseAsync(Course course);

        // ❌ Instructor/Admin: Delete a course
        Task DeleteCourseAsync(int courseId);

        // ❤️ Optional: Get most liked course for dashboard
        Task<string> GetMostLikedCourseAsync(string instructorId);
    }
}
