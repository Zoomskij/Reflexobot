using Reflexobot.Entities;
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
    }
}
