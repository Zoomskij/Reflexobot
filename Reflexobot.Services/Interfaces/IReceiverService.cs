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

        Task<Person> GetPersonByUserId(long userId);

        IEnumerable<string> GetPhrases();

        IEnumerable<string> GetPhrasesbyUserId(long userId);

        Task AddPhrase(int teacherId, string phrase);
    }
}
