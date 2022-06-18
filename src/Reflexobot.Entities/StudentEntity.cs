using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class StudentEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public long ChatId { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public List<StudentAchievment>? StudentAchievments { get; set; }
        public Guid? GroupGuid { get; set; }
        public GroupEntity Group { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
