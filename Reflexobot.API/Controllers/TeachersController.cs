using Microsoft.AspNetCore.Mvc;
using Reflexobot.Services.Inerfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : Controller
    {
        private readonly IReceiverService _receiverSerive;
        private readonly ILogger<TeachersController> _logger;
        public TeachersController(IReceiverService receiverSerive, ILogger<TeachersController> logger)
        {
            _receiverSerive = receiverSerive;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTeachers()
        {
            var teachers = _receiverSerive.GetTeachers();
            return Ok(teachers);
        }
    }
}
