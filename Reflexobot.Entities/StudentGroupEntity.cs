using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class StudentGroup : BaseEntity
    {
        public Guid StudentGuid { get; set; }
        public Guid GroupGuid { get; set; }
        public StudentEntity Student { get; set; }
        public GroupEntity Group { get; set; }
    }
}
