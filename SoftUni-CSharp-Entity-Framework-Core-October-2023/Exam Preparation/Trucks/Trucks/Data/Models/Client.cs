using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Trucks.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }
        public ICollection<ClientTruck> ClientsTrucks { get; set; }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
//pk,fk,req,max,?
//ctor
//•	Id – int, Primary Key
//•	Name – string with length [3, 40] (required)
//•	Nationality – string with length [2, 40] (required)
//•	Type – string (required)
//•	ClientsTrucks – collection of type ClientTruck
