using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Boardgames.Data.Models
{
    public class Creator
    {
        public Creator()
        {
            this.Boardgames = new HashSet<Boardgame>();
        }
        public ICollection<Boardgame> Boardgames { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(7)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(7)]
        public string LastName { get; set; }
    }
}
//pk,fk,req,max
//ctor
//•	Id – int, Primary Key
//•	FirstName – string with length [2, 7] (required)
//•	LastName – string with length [2, 7] (required)
//•	Boardgames – collection of type Boardgame
