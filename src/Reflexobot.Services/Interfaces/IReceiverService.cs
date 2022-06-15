using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services.Inerfaces
{
    public interface IReceiverService
    {
        Task AddUpdate(UpdateEntity updateEntity);

        IEnumerable<Person> GetTeachers();

        Task AddOrUpdateUserPersonId(StudentPersonIds userPersonIds);

        Task<Person> GetPersonByStudentGuid(Guid studentGuid);

        IEnumerable<string> GetPhrases();

        IEnumerable<string> GetPhrasesByStudentGuid(Guid studentGuid);

        Task AddPhrase(int teacherId, string phrase);

        IEnumerable<ChatEntity> GetChats();
    }
}
