using Microsoft.AspNetCore.Mvc;
using Reflexobot.Entities;
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
        public async Task AddScenario(ScenarioAddDto scenarioAddDto)
        {
            await _scenarioService.AddAsync(scenarioAddDto.Text, scenarioAddDto.Command, scenarioAddDto.Type, scenarioAddDto.ParrentGuid);
        }

        [HttpPut]
        [Route("")]
        public async Task UpdateScenario(ScenarioAddDto scenarioAddDto)
        {
            Scenario scenario = new Scenario()
            {
                Guid = scenarioAddDto.Guid,
                Command = scenarioAddDto.Command,
                Text = scenarioAddDto.Text,
                Type = scenarioAddDto.Type
            };

            await _scenarioService.UpdateAsync(scenario);
        }

        [HttpDelete]
        [Route("{guid}")]
        public async Task DeleteScenario(Guid guid)
        {
            await _scenarioService.DeleteAsync(guid);
        }

    }
}
public class ScenarioAddDto
{
    public string Text { get; set; }
    public Guid Guid { get; set; }
    public Guid ParrentGuid { get; set; }
    public string? Command { get; set; }
    public byte? Type { get; set; }
}