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
            var data = _dbSet.Include(x => x.Goal).AsNoTracking();
            return data;
        }

        public async Task<CourseEntity> GetCourse(Guid guid)
        {
            var data = await _dbSet.Include(x => x.Goal).FirstOrDefaultAsync(x => x.Guid == guid);
            return data;
        }

        public IEnumerable<LessonEntity> GetLessonEntitiesByCourseGuid(Guid guid)
        {
            DbSet<LessonEntity> dbSet = _context.Set<LessonEntity>();
            var data = dbSet.Where(x => x.CourseGuid == guid).AsNoTracking();
            return data;

        }
        public IEnumerable<TaskEntity> GetTasksByLessonGuid(Guid guid)
        {
            DbSet<TaskEntity> dbSet = _context.Set<TaskEntity>();
            var data = dbSet.Where(x => x.LessonGuid == guid).AsNoTracking();
            return data;
        }
    }
}
