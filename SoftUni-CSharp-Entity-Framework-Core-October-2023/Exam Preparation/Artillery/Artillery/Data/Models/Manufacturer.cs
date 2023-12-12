using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Guns = new HashSet<Gun>();
        }
        public ICollection<Gun> Guns { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string ManufacturerName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Founded { get; set; }
    }
}
//pk,fk,req,?,max
//ctor
//Manufacturer
//    ⦁	Id – integer, Primary Key
//    ⦁	ManufacturerName – unique text with length [4…40] (required)
//    ⦁	Founded – text with length [10…100] (required)
//    ⦁	Guns – a collection of Gun
