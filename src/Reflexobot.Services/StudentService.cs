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
