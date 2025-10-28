using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Leraning_Application.Data;
using E_Leraning_Application.Models;

namespace E_Leraning_Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all courses created by a specific instructor
        public async Task<List<Course>> GetCoursesByInstructorAsync(string instructorId)
        {
            int instructorIdInt = int.Parse(instructorId);
            return await _context.Courses
                .Where(c => c.InstructorId == instructorIdInt)
                .Include(c => c.Lessons)
                .Include(c => c.Enrollments)
                .ToListAsync();
        }

        // ✅ Get all courses (Admin)
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Instructor) // Instructor User
                .Include(c => c.Lessons)
                .Include(c => c.Enrollments)
                .ToListAsync();
        }

        // ✅ Get a course by ID
        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Enrollments)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        // ✅ Add a new course
        public async Task AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        // ✅ Update existing course
        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        // ✅ Delete course
        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ Get most liked course (by highest enrollment)
        public async Task<string> GetMostLikedCourseAsync(string instructorId)
        {
            int instructorIdInt = int.Parse(instructorId);
            var mostPopular = await _context.Courses
                .Where(c => c.InstructorId == instructorIdInt)
                .OrderByDescending(c => c.Enrollments.Count)
                .FirstOrDefaultAsync();

            return mostPopular?.Title ?? "No courses yet";
        }
    }
}
