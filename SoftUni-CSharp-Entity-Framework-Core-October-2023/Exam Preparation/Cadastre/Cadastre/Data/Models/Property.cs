using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastre.Data.Models
{
    public class Property
    {
        public Property()
        {
            this.PropertiesCitizens = new HashSet<PropertyCitizen>();
        }
        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string PropertyIdentifier { get; set; }

        [Required]
        public int Area { get; set; }//not negative

        [Required]
        [MaxLength(500)]
        public string Details { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfAcquisition { get; set; }

        [Required]
        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }

        public virtual District District { get; set; }

    }
}
//pk,fk,req,max
//ctor

//Property
//    •	Id – int, Primary Key
//    •	PropertyIdentifier – string with length [16, 20] (required)
//    •	Area – int not negative (required)
//    •	Details - string with length[5, 500] (not required)
//    •	Address – string with length [5, 200] (required)
//    •	DateOfAcquisition – DateTime (required)
//    •	DistrictId – int, foreign key (required)
//    •	District – District
//    •	PropertiesCitizens - collection of type PropertyCitizen
