using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class StudentNotifyIds : BaseEntity
    {
        public Guid StudentGuid { get; set; }
        public Guid NotifyGuid { get; set; }
    }
}
