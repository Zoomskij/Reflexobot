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
    public class CourseRepository : ICourseRepository
    {
        private readonly ReflexobotContext _context;
        private readonly DbSet<CourseEntity> _dbSet;
        public CourseRepository(ReflexobotContext context)
        {
            _context = context;
            _dbSet = context.Set<CourseEntity>();
        }
        public IEnumerable<CourseEntity> GetCourses()
        {
            var data = _dbSet.Include(l => l.Lessons).ThenInclude(t => t.Tasks).AsNoTracking();
            return data;
        }
    }
}
