using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services
{
    public class ScenarioService : IScenarioService
    {
        private readonly IScenarioRepository _scenarioRepository;
        public ScenarioService(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;
        }

        public IEnumerable<Scenario> Get()
        {
            return _scenarioRepository.Get();
        }
        public async Task AddAsync(string text)
        {
            var scenario = new Scenario
            {
                Text = text
            };
            await _scenarioRepository.AddAsync(scenario);
        }
        public async Task UpdateAsync(Scenario scenario)
        {
            await _scenarioRepository.UpdateAsync(scenario);
        }
        public async Task DeleteAsync(Guid guid)
        {
            await _scenarioRepository.DeleteAsync(guid);
        }
    }
}
