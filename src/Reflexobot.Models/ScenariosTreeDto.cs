using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Models
{
    public class ScenariosTreeDto
    {
        public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? ParrentGuid { get; set; } = null;
        public string Command { get; set; }
        public string Text { get; set; }
        public ScenariosTreeDto Children { get; set; }
    }
}
