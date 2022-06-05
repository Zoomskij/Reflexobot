using Reflexobot.Entities;
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

        Task AddOrUpdateUserPersonId(UserPersonIds userPersonIds);

        Task<Person> GetPersonByUserId(long userId);

        IEnumerable<string> GetPhrases();

        IQueryable<string> GetPhrasesbyUserId(long userId);

        Task AddPhrase(int teacherId, string phrase);
    }
}
