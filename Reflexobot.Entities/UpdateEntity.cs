using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class UpdateEntity
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public int MessageId { get; set; }
        [ForeignKey("MessageId")]
        public MessageEntity Message { get; set; }
    }
}
