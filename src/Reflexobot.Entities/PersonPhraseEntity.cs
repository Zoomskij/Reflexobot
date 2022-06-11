using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class PersonPhraseEntity : BaseEntity
    {
        public int PersonId { get; set; }
        public string Phrase { get; set; }
    }
}
