using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Trucks.Data.Models
{
    public class Despatcher
    {
        public Despatcher()
        {
            this.Trucks = new HashSet<Truck>();
        }
        public ICollection<Truck> Trucks { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name {get; set; }

        public string Position { get; set; }//string?

    }
}
//pk,fk,req,max,?
//ctor
//•	Id – int, Primary Key
//•	Name – string with length [2, 40] (required)
//•	Position – string
//•	Trucks – collection of type Truck
