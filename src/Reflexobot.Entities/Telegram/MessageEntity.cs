using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reflexobot.Entities.Telegram
{ 
    public class MessageEntity : BaseEntity
    {
        public int MessageId { get; set; }
        public long ChatId { get; set; }
        public ChatEntity Chat { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
    }
}