using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cadastre.DataProcessor.ImportDtos
{
    public class ImportCitizenDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        public string BirthDate { get; set; }//DateTime?

        [Required]
        public string MaritalStatus { get; set; }

        public int[] Properties { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range

//json
//"FirstName": "Ivan",
//"LastName": "Georgiev",
//"BirthDate": "12-05-1980",
//"MaritalStatus": "Married",
//"Properties": [ 17, 29 ]



//Citizen
//    •	Id – integer, Primary Key
//    •	FirstName – text with length [2, 30] (required)
//    •	LastName – text with length [2, 30] (required)
//    •	BirthDate – DateTime (required)
//    •	MaritalStatus - MaritalStatus enum (Unmarried = 0, Married, Divorced, Widowed)(required)
//    •	PropertiesCitizens - collection of type PropertyCitizen
