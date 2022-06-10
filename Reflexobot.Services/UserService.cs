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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<NotifyEntity> GetNotifies()
        {
            return _userRepository.GetNotifies().ToList();
        }

        public async Task AddOrUpdateUserNotifyId(UserNotifyIds userPersonIds)
        {
            await _userRepository.AddOrUpdateUserNotifyId(userPersonIds);
        }
    }
}
