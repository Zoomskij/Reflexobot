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
        Task<StudentEntity> GetStudentByChatIdAsync(long chatId);
        Task<StudentEntity> GetStudentByUserIdAsync(int userId);
        Task AddStudentAsync(StudentEntity student);
        Task UpdateActiveScenarioAsync(Guid studentGuid, Guid scenarioGuid);


    }
}
