using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<NotifyEntity> GetNotifies();
        Task AddOrUpdateUserNotifyId(UserNotifyIds userNotifyIds);
    }
}
