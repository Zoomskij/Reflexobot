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

        public IQueryable<Person> GetTeachers()
        {
            DbSet<Person> dbSet = _context.Set<Person>();
            var data = dbSet.AsNoTracking();
            return data;
        }
        public async Task AddOrUpdateUserPersonId(UserPersonIds userPersonIds)
        {
            DbSet<UserPersonIds> dbSet = _context.Set<UserPersonIds>();
            var currentTeacher = await dbSet.FirstOrDefaultAsync(x => x.UserId == userPersonIds.UserId);
            if (currentTeacher != null)
            {
                currentTeacher.PersonId = userPersonIds.PersonId;
                _context.Entry(currentTeacher).CurrentValues.SetValues(currentTeacher);
            }
            else
            {
                await dbSet.AddAsync(userPersonIds);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetPersonByUserId(long userId)
        {
            DbSet<UserPersonIds> dbSet = _context.Set<UserPersonIds>();
            var userPerson = await dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userPerson == null)
                return null;
            var dbSetPerson = _context.Set<Person>();
            return await dbSetPerson.FirstOrDefaultAsync(x => x.Id == userPerson.PersonId);
        }

        public IQueryable<string> GetPhrasesbyUserId(long userId)
        {
            DbSet<UserPersonIds> dbSet = _context.Set<UserPersonIds>();
            var userPerson = dbSet.FirstOrDefault(x => x.UserId == userId);
            if (userPerson == null)
                return null;


            DbSet<PersonPhraseEntity> dbSetPhrases = _context.Set<PersonPhraseEntity>();
            var userPhrases = dbSetPhrases.Where(x => x.PersonId == userPerson.PersonId).Select(x => x.Phrase);
            return userPhrases;
        }
    }
}