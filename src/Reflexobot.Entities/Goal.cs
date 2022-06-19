using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class Goal : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
    }
}
