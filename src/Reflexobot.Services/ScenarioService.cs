using AutoMapper;
using Reflexobot.Entities;
using Reflexobot.Models;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Helpers;
using Reflexobot.Services.Interfaces;
using Reflexobot.Services.Mapper;
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
        public readonly IMapper _mapper;
        public ScenarioService(IScenarioRepository scenarioRepository, IMapper mapper)
        {
            _scenarioRepository = scenarioRepository;
            _mapper = mapper;
        }

        public IEnumerable<Scenario> Get()
        {
            return _scenarioRepository.Get();
        }

        public IEnumerable<ScenarioDto> GetTree()
        {
            var scenarios = _scenarioRepository.Get().ToList();
            var dto = _mapper.Map<List<ScenarioDto>>(scenarios);
            var tree = dto.GenerateScenarioTree(c => c.Id, c => c.ParrentGuid);
            return tree;
        }
        public async Task AddAsync(string text, Guid parrentGuid)
        {
            var scenario = new Scenario
            {
                ParrentGuid = parrentGuid,
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
