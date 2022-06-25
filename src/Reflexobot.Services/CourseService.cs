using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public IEnumerable<CourseEntity> GetCourses()
        {
            return _courseRepository.GetCourses();
        }

        public async Task<CourseEntity> GetCourse(Guid guid)
        {
            return await _courseRepository.GetCourse(guid);
        }

        public IEnumerable<LessonEntity> GetLessonEntitiesByCourseGuid(Guid guid)
        {
            return _courseRepository.GetLessonEntitiesByCourseGuid(guid);
        }

        public IEnumerable<TaskEntity> GetTasksByLessonGuid(Guid guid)
        {
            return _courseRepository.GetTasksByLessonGuid(guid);
        }
    }
}
