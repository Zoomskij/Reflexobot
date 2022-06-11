using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class StudentAchievment : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public long UserId { get; set; }
        public Guid AchievmentGuid { get; set; }
        [ForeignKey("AchievmentGuid")]
        public Achievment Achievment { get; set; }
    }
}
