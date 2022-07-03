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

        public async Task AddAsync(CourseEntity course)
        {
            await _dbSet.AddAsync(course);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CourseEntity course)
        {
            var currentCourse = await _dbSet.FirstOrDefaultAsync(x => x.Guid == course.Guid);
            if (currentCourse != null)
            {
                currentCourse.Description = course.Name;
                currentCourse.Img = course.Img;
                currentCourse.Goal = course.Goal;
                _context.Entry(currentCourse).CurrentValues.SetValues(currentCourse);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid guid)
        {
            var currentCourse = await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid);
            if (currentCourse != null)
            {
                _dbSet.Remove(currentCourse);
                await _context.SaveChangesAsync();
            }
        }
    }
}
