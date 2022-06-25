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
        public IActionResult GetAchievments()
        {
            var scenarios = _scenarioService.Get().OrderBy(x => x.CreatedDate);
            
            List<ScenarioDto> scenarioDtoList = new List<ScenarioDto>();

            foreach (var scenario in scenarios)
            {
                scenarioDtoList.Add(new ScenarioDto
                {
                    Id = scenario.Guid,
                    ParrentGuid = scenario.ParrentGuid,
                    CreatedDate = scenario.CreatedDate,
                    Command = scenario.Command,
                    Label = !string.IsNullOrWhiteSpace(scenario.Text) ? scenario.Text : scenario.Command,
                    Children = new List<ScenarioDto>()
                });
            }

            var groups = scenarioDtoList.GroupBy(x => x.ParrentGuid);
            List<ScenarioDto> scenarioDto = new List<ScenarioDto>();

            foreach (var group in groups)
            {
                foreach (var item in group)
                {
                    var b = group.Select(x => x);
                    scenarioDto.Add(new ScenarioDto
                    {
                        Id = item.Id,
                        ParrentGuid = item.ParrentGuid,
                        CreatedDate = item.CreatedDate,
                        Command = item.Command,
                        Label = item.Label,
                        Children = group.Select(x => x).ToList()
                    });
                }
            }

            return Ok(scenarioDto);
        }

    }
}
