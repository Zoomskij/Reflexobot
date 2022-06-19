﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Guid LessonGuid { get; set; }
        [JsonIgnore]
        public LessonEntity Lesson { get; set; }
        public Goal? Goal { get; set; }
    }
}
