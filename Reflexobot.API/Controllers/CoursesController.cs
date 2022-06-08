using Microsoft.AspNetCore.Mvc;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _courseService.GetCourses();
            return Ok(courses);
        }

        [HttpGet]
        [Route("/lessons/{courseGuid}")]
        public IActionResult GetLessonsByCourseGuid(Guid courseGuid)
        {
            var lessons = _courseService.GetLessonEntitiesByCourseGuid(courseGuid);
            return Ok(lessons);
        }

        [HttpGet]
        [Route("/tasks/{lessonGuid}")]
        public IActionResult GetTasksByLessonGuid(Guid lessonGuid)
        {
            var lessons = _courseService.GetTasksByLessonGuid(lessonGuid);
            return Ok(lessons);
        }
    }
}
