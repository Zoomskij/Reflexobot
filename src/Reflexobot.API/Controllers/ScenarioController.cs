using Microsoft.AspNetCore.Mvc;
using Reflexobot.Models;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioController : Controller
    {
        private readonly IScenarioService _scenarioService;
        public ScenarioController(IScenarioService scenarioService)
        {
            _scenarioService = scenarioService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetScenariosTree()
        {
            var scenarios = _scenarioService.GetTree();
           
            return Ok(scenarios);
        }

        [HttpPost]
        [Route("")]
        public async Task AddScenario(string text)
        {
            await _scenarioService.AddAsync(text, Guid.Empty);
        }

    }
}
