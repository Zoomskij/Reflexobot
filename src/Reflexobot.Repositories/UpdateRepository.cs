using Microsoft.EntityFrameworkCore;
using Reflexobot.Data;
using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;
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
            try
            {
                await _dbSet.AddAsync(update);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public IQueryable<Person> GetTeachers()
        {
            DbSet<Person> dbSet = _context.Set<Person>();
            var data = dbSet.AsNoTracking();
            return data;
        }
        public async Task AddOrUpdateUserPersonId(StudentPersonIds userPersonIds)
        {
            DbSet<StudentPersonIds> dbSet = _context.Set<StudentPersonIds>();
            var currentTeacher = await dbSet.FirstOrDefaultAsync(x => x.StudentGuid == userPersonIds.StudentGuid);
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

        public async Task<Person> GetPersonByStudentGuid(Guid studentGuid)
        {
            DbSet<StudentPersonIds> dbSet = _context.Set<StudentPersonIds>();
            var userPerson = await dbSet.FirstOrDefaultAsync(x => x.StudentGuid == studentGuid);
            if (userPerson == null)
                return null;
            var dbSetPerson = _context.Set<Person>();
            return await dbSetPerson.FirstOrDefaultAsync(x => x.Id == userPerson.PersonId);
        }

        public IEnumerable<string> GetPhrases()
        {
            DbSet<PersonPhraseEntity> dbSetPhrases = _context.Set<PersonPhraseEntity>();
            var userPhrases = dbSetPhrases.Select(x => x.Phrase);
            return userPhrases;
        }

        public IQueryable<string> GetPhrasesByStudentGuid(Guid studentGuid)
        {
            DbSet<StudentPersonIds> dbSet = _context.Set<StudentPersonIds>();
            var userPerson = dbSet.FirstOrDefault(x => x.StudentGuid == studentGuid);
            if (userPerson == null)
                return null;


            DbSet<PersonPhraseEntity> dbSetPhrases = _context.Set<PersonPhraseEntity>();
            var userPhrases = dbSetPhrases.Where(x => x.PersonId == userPerson.PersonId).Select(x => x.Phrase);
            return userPhrases;
        }

        public async Task AddPhrase(int teacherId, string phrase)
        {
            DbSet<PersonPhraseEntity> dbSetPhrases = _context.Set<PersonPhraseEntity>();
            PersonPhraseEntity phraseEntity = new PersonPhraseEntity
            {
                PersonId = teacherId,
                Phrase = phrase
            };
            await dbSetPhrases.AddAsync(phraseEntity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ChatEntity> GetChats()
        {
            DbSet<ChatEntity> dbSetChat= _context.Set<ChatEntity>();
            var chats = dbSetChat.AsNoTracking();

            return chats;
        }
    }
}