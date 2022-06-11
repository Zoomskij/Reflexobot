using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class StudentTaskId : BaseEntity
    {
        public long UserId { get; set; }
        public Guid TaskGuid { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
