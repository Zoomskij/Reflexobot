using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _userRepository;
        public StudentService(IStudentRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<StudentEntity> GetStudentByChatIdAsync(long chatId)
        {
            var student = await _userRepository.GetStudentByChatIdAsync(chatId);
            return student;
        }

        public async Task<StudentEntity> GetStudentByUserIdAsync(int userId)
        {
            var student = await _userRepository.GetStudentByUserIdAsync(userId);
            return student;
        }

        public async Task AddStudentAsync(StudentEntity student)
        {
            await _userRepository.AddStudentAsync(student);
        }
        public async Task UpdateActiveScenarioAsync(Guid studentGuid, Guid scenarioGuid)
        {
            await _userRepository.UpdateActiveScenarioAsync(studentGuid, scenarioGuid);
        }
        
        public IEnumerable<NotifyEntity> GetNotifies()
        {
            return _userRepository.GetNotifies().ToList();
        }

        public async Task AddOrUpdateUserNotifyId(StudentNotifyIds userPersonIds)
        {
            await _userRepository.AddOrUpdateUserNotifyId(userPersonIds);
        }


    }
}
