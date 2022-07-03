using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<CourseEntity> GetCourses();
        Task<CourseEntity> GetCourse(Guid guid);
        IEnumerable<LessonEntity> GetLessonEntitiesByCourseGuid(Guid guid);
        IEnumerable<TaskEntity> GetTasksByLessonGuid(Guid guid);
        Task AddAsync(CourseEntity course);
        Task UpdateAsync(CourseEntity course);
        Task DeleteAsync(Guid guid);
    }
}
