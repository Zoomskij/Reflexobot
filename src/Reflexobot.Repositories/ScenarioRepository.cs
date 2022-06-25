using Microsoft.EntityFrameworkCore;
using Reflexobot.Data;
using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories
{
    public class ScenarioRepository : IScenarioRepository
    {
        private readonly ReflexobotContext _context;
        private readonly DbSet<Scenario> _dbSet;
        public ScenarioRepository(ReflexobotContext context)
        {
            _context = context;
            _dbSet = context.Set<Scenario>();
        }

        public IEnumerable<Scenario> Get()
        {
            var data = _dbSet.AsNoTracking();
            return data;
        }

        public async Task AddAsync(Scenario scenario)
        {
            await _dbSet.AddAsync(scenario);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Scenario scenario)
        {
            var currentScenario = await _dbSet.FirstOrDefaultAsync(x => x.Guid == scenario.Guid);
            if (currentScenario != null)
            {
                currentScenario.Text = scenario.Text;
                _context.Entry(currentScenario).CurrentValues.SetValues(currentScenario);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid guid)
        {
            var currentScenario = await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid);
            if (currentScenario != null)
            {
                 _dbSet.Remove(currentScenario);
                await _context.SaveChangesAsync();
            }

        }
    }
}
