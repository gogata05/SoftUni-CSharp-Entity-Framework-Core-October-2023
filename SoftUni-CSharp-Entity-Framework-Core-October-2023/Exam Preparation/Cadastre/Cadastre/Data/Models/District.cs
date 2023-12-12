using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastre.Data.Enumerations;
using System.ComponentModel.DataAnnotations;
namespace Cadastre.Data.Models
{
    public class District
    {
        public District()
        {
            this.Properties = new HashSet<Property>();
        }
        public virtual ICollection<Property> Properties { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8)]
        public string PostalCode { get; set; }

        [Required]
        public virtual Region Region { get; set; }
    }
}
//pk,fk,req,max
//ctor

//District
//    •	Id – int, Primary Key
//    •	Name – string with length [2, 80] (required)
//    •	PostalCode – string with length 8. All postal codes must have the following structure:starting with two capital letters, followed by e dash '-', followed by five digits. Example: SF - 10000(required)
//    •	Region – Region enum (SouthEast = 0, SouthWest, NorthEast, NorthWest)(required)
//    •	Properties - collection of type Property
