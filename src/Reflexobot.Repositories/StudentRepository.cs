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
    public class StudentRepository : IStudentRepository
    {
        private readonly ReflexobotContext _context;
        //private readonly DbSet<UpdateEntity> _dbSet;

        public StudentRepository(ReflexobotContext context)
        {
            _context = context;
            //_dbSet = context.Set<UpdateEntity>();
        }


        public IQueryable<NotifyEntity> GetNotifies()
        {
            var dbSet = _context.Set<NotifyEntity>();
            return dbSet.AsNoTracking();
        }

        public async Task AddOrUpdateUserNotifyId(StudentNotifyIds userNotifyIds)
        {
            DbSet<StudentNotifyIds> dbSet = _context.Set<StudentNotifyIds>();
            var currentNotify = await dbSet.FirstOrDefaultAsync(x => x.StudentGuid == userNotifyIds.StudentGuid);
            if (currentNotify != null)
            {
                currentNotify.NotifyGuid = currentNotify.NotifyGuid;
                _context.Entry(currentNotify).CurrentValues.SetValues(currentNotify);
            }
            else
            {
                await dbSet.AddAsync(userNotifyIds);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<StudentEntity> GetStudentByChatIdAsync(long chatId)
        {
            DbSet<StudentEntity> dbSet = _context.Set<StudentEntity>();
            return await dbSet.FirstOrDefaultAsync(x => x.ChatId == chatId);
        }
        public async Task<StudentEntity> GetStudentByUserIdAsync(int userId)
        {
            DbSet<StudentEntity> dbSet = _context.Set<StudentEntity>();
            return await dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        
        public async Task AddStudentAsync(StudentEntity student)
        {
            DbSet<StudentEntity> dbSet = _context.Set<StudentEntity>();
            await dbSet.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActiveScenarioAsync(Guid studentGuid, Guid scenarioGuid)
        {
            DbSet<StudentEntity> dbSet = _context.Set<StudentEntity>();
            var currentStudent = await dbSet.FirstOrDefaultAsync(x => x.Guid == studentGuid);
            if (currentStudent != null)
            {
                currentStudent.ActiveScenarioGuid = scenarioGuid;
                _context.Entry(currentStudent).CurrentValues.SetValues(currentStudent);
            }

            await _context.SaveChangesAsync();
        }
    }
}
