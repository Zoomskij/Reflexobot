using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class UserPersonIds : BaseEntity
    {
        public long UserId { get; set; }
        public int PersonId { get; set; }
    }
}
