using Microsoft.AspNetCore.Mvc;
using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;
using Reflexobot.Services.Inerfaces;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IReceiverService _reseiverService;
        public CoursesController(ICourseService courseService, IReceiverService reseiverService)
        {
            _courseService = courseService;
            _reseiverService = reseiverService;
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

        [HttpGet]
        [Route("chats")]
        public IActionResult GetChats()
        {
            List<ChatEntityDto> groupedChats = new List<ChatEntityDto>();
            var chats = _reseiverService.GetChats();
            foreach (var chat in chats.GroupBy(x=> x.Id))
            {
                groupedChats.Add(new ChatEntityDto
                {
                    Id = chat.Key,
                    FirstName = chat.FirstOrDefault(x => x.Id == chat.Key).FirstName,
                    LastName = chat.FirstOrDefault(x => x.Id == chat.Key).LastName
                });
            }
            return Ok(groupedChats);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddCourse([FromBody] CourseEntity course)
        {
            await _courseService.AddAsync(course);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAchievment([FromBody] CourseEntity course)
        {
            await _courseService.UpdateAsync(course);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteAchievment(Guid guid)
        {
            await _courseService.DeleteAsync(guid);
            return Ok();
        }
    }

    public class ChatEntityDto 
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
