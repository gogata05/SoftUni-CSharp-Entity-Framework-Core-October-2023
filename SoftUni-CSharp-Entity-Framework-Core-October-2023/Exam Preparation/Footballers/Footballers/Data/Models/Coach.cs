using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Footballers.Data.Models
{
    public class Coach
    {
        public Coach()
        {
            this.Footballers = new HashSet<Footballer>();
        }
        public ICollection<Footballer> Footballers { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string Nationality { get; set; }
    }
}
//pk,fk,req,max
//ctor
//⦁	Id – int, Primary Key
//⦁	Name – string with length [2, 40] (required)
//⦁	Nationality – string (required)
//⦁	Footballers – collection of type Footballer
