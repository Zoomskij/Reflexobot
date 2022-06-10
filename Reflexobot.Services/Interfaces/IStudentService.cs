using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<NotifyEntity> GetNotifies();
        Task AddOrUpdateUserNotifyId(StudentNotifyIds userPersonIds);
    }
}
