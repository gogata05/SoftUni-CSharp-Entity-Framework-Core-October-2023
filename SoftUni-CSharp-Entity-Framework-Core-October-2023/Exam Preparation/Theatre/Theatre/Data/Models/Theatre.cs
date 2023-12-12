using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Theatre.Data.Models
{
    public class Theatre
    {
        public Theatre()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        public ICollection<Ticket> Tickets { get; set; } 

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MaxLength(30)]
        public string Director { get; set; }
    }
}
//pk,fk,req,?,max
//ctor
//Theatre
//    ⦁	Id – integer, Primary Key
//    ⦁	Name – text with length [4, 30] (required)
//    ⦁	NumberOfHalls – sbyte between [1…10] (required)
//    ⦁	Director – text with length [4, 30] (required)
//    ⦁	Tickets – a collection of type Ticket
