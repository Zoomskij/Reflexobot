using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Guid = Guid.NewGuid();
        }

        [Key]
        public Guid Guid { get; set; }
    }
}
