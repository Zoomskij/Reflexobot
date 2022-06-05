﻿using Reflexobot.Entities;
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

        Task AddOrUpdateUserPersonId(UserPersonIds userPersonIds);

        Task<Person> GetPersonByUserId(long userId);

        IEnumerable<string> GetPhrases();

        IEnumerable<string> GetPhrasesbyUserId(long userId);

        Task AddPhrase(int teacherId, string phrase);
    }
}
