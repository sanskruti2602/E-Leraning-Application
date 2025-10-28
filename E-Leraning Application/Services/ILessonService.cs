using System.Collections.Generic;
using System.Threading.Tasks;
using E_Leraning_Application.Models;

namespace E_Leraning_Application.Services
{
    public interface ILessonService
    {
        Task<List<Lesson>> GetLessonsByCourseAsync(int courseId);
        Task<Lesson> GetLessonByIdAsync(int lessonId);
        Task AddLessonAsync(Lesson lesson);
        Task UpdateLessonAsync(Lesson lesson);
        Task DeleteLessonAsync(int lessonId);
    }
}
