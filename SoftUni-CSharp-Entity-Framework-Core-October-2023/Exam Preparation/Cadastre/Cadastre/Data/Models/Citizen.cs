using Cadastre.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Cadastre.Data.Models
{
    public class Citizen
    {
        public Citizen()
        {
            this.PropertiesCitizens = new HashSet<PropertyCitizen>();
        }
        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }

        [Key]
        public int Id {get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        public virtual DateTime BirthDate { get; set; }

        [Required]
        public virtual MaritalStatus MaritalStatus { get; set; }


    }
}
//pk,fk,req,max
//ctor

//Citizen
//    •	Id – integer, Primary Key
//    •	FirstName – text with length [2, 30] (required)
//    •	LastName – text with length [2, 30] (required)
//    •	BirthDate – DateTime (required)
//    •	MaritalStatus - MaritalStatus enum (Unmarried = 0, Married, Divorced, Widowed)(required)
//    •	PropertiesCitizens - collection of type PropertyCitizen
