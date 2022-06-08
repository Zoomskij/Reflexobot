using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseEntity> GetCourses();
    }
}
