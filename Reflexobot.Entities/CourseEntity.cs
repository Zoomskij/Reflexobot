﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class CourseEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<LessonEntity> Lessons { get; set; }
        public IEnumerable<GroupEntity> Groups { get; set; }
    }
}