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
    }
}
