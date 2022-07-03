using Microsoft.AspNetCore.Mvc;
using Reflexobot.Entities;
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

        [HttpPost]
        [Route("")]
        public IActionResult AddAchievment([FromBody] Achievment achievment)
        {
            var achievments = _achievmentService.AddAsync(achievment);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateAchievment([FromBody] Achievment achievment)
        {
            var achievments = _achievmentService.UpdateAsync(achievment);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IActionResult DeleteAchievment(Guid guid)
        {
            var achievments = _achievmentService.DeleteAsync(guid);
            return Ok();
        }
    }
}
