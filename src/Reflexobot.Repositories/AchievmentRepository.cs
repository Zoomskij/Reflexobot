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
    public class AchievmentRepository : IAchievmentRepository
    {
        private readonly ReflexobotContext _context;
        private readonly DbSet<Achievment> _dbSet;
        public AchievmentRepository(ReflexobotContext context)
        {
            _context = context;
            _dbSet = context.Set<Achievment>();
        }

        public IEnumerable<Achievment> GetAchievments()
        {
            var data = _dbSet.AsNoTracking();
            return data;
        }

        public async Task AddAsync(Achievment achievment)
        {
            await _dbSet.AddAsync(achievment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Achievment achievment)
        {
            var currentAchievment = await _dbSet.FirstOrDefaultAsync(x => x.Guid == achievment.Guid);
            if (currentAchievment != null)
            {
                currentAchievment.Description = achievment.Description;
                _context.Entry(currentAchievment).CurrentValues.SetValues(currentAchievment);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid guid)
        {
            var currentAchievment = await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid);
            if (currentAchievment != null)
            {
                _dbSet.Remove(currentAchievment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
