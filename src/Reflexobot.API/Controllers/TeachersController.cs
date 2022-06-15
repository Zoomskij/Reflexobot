using Microsoft.AspNetCore.Mvc;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Inerfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : Controller
    {
        private readonly IReceiverService _receiverSerive;
        private readonly IUpdateRepository _updateRepository;
        private readonly ILogger<TeachersController> _logger;
        public TeachersController(IReceiverService receiverSerive, IUpdateRepository updateRepository, ILogger<TeachersController> logger)
        {
            _receiverSerive = receiverSerive;
            _updateRepository = updateRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTeachers()
        {
            var teachers = _receiverSerive.GetTeachers();
            return Ok(teachers);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetTeacher(Guid studentGuid)
        {
            var currentTeacher = await _updateRepository.GetPersonByStudentGuid(studentGuid);
            return Ok(currentTeacher);
        }
    }
}
