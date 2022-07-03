using Microsoft.AspNetCore.Mvc;
using Reflexobot.Entities;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoalController : Controller
    {
        private readonly IGoalService _goalService;
        public GoalController(IGoalService goalService)
        {
            _goalService = goalService; 
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetGoals()
        {
            var achievments = _goalService.Get();
            return Ok(achievments);
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddGoal(string text)
        {
            _goalService.AddAsync(text);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateGoal([FromBody] Goal goal)
        {
            _goalService.UpdateAsync(goal);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IActionResult DeleteGoal(Guid guid)
        {
            var achievments = _goalService.DeleteAsync(guid);
            return Ok();
        }
    }
}
