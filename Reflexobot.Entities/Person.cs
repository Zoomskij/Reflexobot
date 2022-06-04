using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Entities
{
    public class Person : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
    }
}
