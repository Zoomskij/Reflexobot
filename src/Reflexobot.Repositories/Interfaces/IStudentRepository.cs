using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        IQueryable<NotifyEntity> GetNotifies();
        Task AddOrUpdateUserNotifyId(StudentNotifyIds userNotifyIds);
    }
}
