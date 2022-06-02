using Microsoft.EntityFrameworkCore;
using Reflexobot.Data;
using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;

namespace Reflexobot.Repositories
{
    public class UpdateRepository : IUpdateRepository
    {
        private readonly ReflexobotContext _context;
        private readonly DbSet<UpdateEntity> _dbSet;

        public UpdateRepository(ReflexobotContext context)
        {
            _context = context;
            _dbSet = context.Set<UpdateEntity>();
        }

        public async Task AddUpdate(UpdateEntity update)
        {
            await _dbSet.AddAsync(update);
            await _context.SaveChangesAsync();
        }
    }
}