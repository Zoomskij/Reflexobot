using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories.Interfaces
{
    public interface IUpdateRepository
    {
        Task AddUpdate(UpdateEntity update);
        IQueryable<Person> GetTeachers();

        Task AddOrUpdateUserPersonId(StudentPersonIds userPersonIds);

        Task<Person> GetPersonByStudentGuid(Guid studentGuid);

        IEnumerable<string> GetPhrases();

        IQueryable<string> GetPhrasesByStudentGuid(Guid studentGuid);

        Task AddPhrase(int teacherId, string phrase);

        IQueryable<ChatEntity> GetChats();
    }
}
