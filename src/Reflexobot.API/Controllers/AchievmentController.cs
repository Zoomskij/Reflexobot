using Microsoft.AspNetCore.Mvc;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AchievmentController : Controller
    {
        private readonly IAchievmentService _achievmentService;
        public AchievmentController(IAchievmentService achievmentService)
        {
            _achievmentService = achievmentService; 
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAchievments()
        {
            var achievments = _achievmentService.GetAchievments();
            return Ok(achievments);
        }
    }
}
