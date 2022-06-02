using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Service.Services.Inerfaces
{
    public interface IReceiverService
    {
        Task<string[]> GetPhrases();
    }
}
