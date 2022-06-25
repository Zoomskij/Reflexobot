using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services.Interfaces
{
    public interface IScenarioService
    {
        IEnumerable<Scenario> Get();
        Task AddAsync(string text);
        Task UpdateAsync(Scenario scenario);
        Task DeleteAsync(Guid guid);
    }
}
