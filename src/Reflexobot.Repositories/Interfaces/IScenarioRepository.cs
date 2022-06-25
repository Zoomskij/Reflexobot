using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories.Interfaces
{
    public interface IScenarioRepository
    {
        IEnumerable<Scenario> Get();
        Task AddAsync(Scenario note);
        Task UpdateAsync(Scenario note);
        Task DeleteAsync(Guid noteGuid);
    }
}
