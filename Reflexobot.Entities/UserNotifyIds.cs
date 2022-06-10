using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class UserNotifyIds : BaseEntity
    {
        public long UserId { get; set; }
        public int NotifyId { get; set; }
    }
}
