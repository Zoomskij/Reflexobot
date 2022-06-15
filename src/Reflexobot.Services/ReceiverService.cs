using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;
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

        public async Task AddOrUpdateUserPersonId(StudentPersonIds userPersonIds)
        {
            await _updateRepository.AddOrUpdateUserPersonId(userPersonIds);
        }

        public async Task<Person> GetPersonByStudentGuid(Guid studentGuid)
        {
            return await _updateRepository.GetPersonByStudentGuid(studentGuid);
        }

        public IEnumerable<string> GetPhrases()
        {
            return _updateRepository.GetPhrases().ToList();
        }

        public IEnumerable<string> GetPhrasesByStudentGuid(Guid studentGuid)
        {
            return _updateRepository.GetPhrasesByStudentGuid(studentGuid).ToList();
        }

        public async Task AddPhrase(int teacherId, string phrase)
        {
            await _updateRepository.AddPhrase(teacherId, phrase);
        }

        public IEnumerable<ChatEntity> GetChats()
        {
            return _updateRepository.GetChats().ToList().GroupBy(x => x.Id).SelectMany(x => x);
        }
    }
}
