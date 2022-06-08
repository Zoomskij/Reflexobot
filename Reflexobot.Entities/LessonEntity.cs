using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class LessonEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<TaskEntity> Tasks {get;set;}
        public Guid CourseGuid { get; set; }
        public CourseEntity Course { get; set; }

    }
}
