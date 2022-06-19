using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class LessonEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<TaskEntity> Tasks {get;set;}
        [JsonIgnore]
        public Guid CourseGuid { get; set; }
        [JsonIgnore]
        public CourseEntity Course { get; set; }
        public Goal? Goal { get; set; }

    }
}
