using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class UserPersonIds
    {
        //todo: fast hack for not create key by two columns
        [Key]
        public Guid guid { get; set; }  = Guid.NewGuid();
        public int UserId { get; set; }
        public int PersonId { get; set; }
    }
}
