using Reflexobot.Entities;
using Reflexobot.Repositories;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IUpdateRepository _updateRepository;
        public ReceiverService(IUpdateRepository updateRepository) 
        {
            _updateRepository = updateRepository;
        }


        public async Task AddUpdate(UpdateEntity updateEntity)
        {
            await _updateRepository.AddUpdate(updateEntity);
        }

        public IEnumerable<Person> GetTeachers()
        {
            var persons = _updateRepository.GetTeachers();
            return persons.ToList();
        }

        public async Task AddOrUpdateUserPersonId(UserPersonIds userPersonIds)
        {
            await _updateRepository.AddOrUpdateUserPersonId(userPersonIds);
        }

        public async Task<Person> GetPersonByUserId(long userId)
        {
            return await _updateRepository.GetPersonByUserId(userId);
        }
    }
}
