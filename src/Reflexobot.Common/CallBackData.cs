using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Common
{
    public class CallBackData
    {
        public string Command { get; set; }
        public Guid Guid { get; set; }
        public int Position { get; set; }
        public int Count { get; set; }
    }
}
