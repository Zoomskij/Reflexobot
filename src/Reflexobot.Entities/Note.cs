using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class Note : BaseEntity
    {
        public DateTime CreatedDate => DateTime.Now;
        public string Text { get; set; }
        public Guid StudentGuid { get; set; }
        [ForeignKey("StudentGuid")]
        public StudentEntity Student { get; set; }

    }
}
