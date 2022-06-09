using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class StudentCourseEntity : BaseEntity
    {
        public Guid StudentGuid { get; set; }
        public Guid CourseGuid { get; set; }
        public StudentEntity Student { get; set; }
        public CourseEntity Course { get; set; }
        public int State { get; set; }
        public int Percent { get; set; }
    }
}
