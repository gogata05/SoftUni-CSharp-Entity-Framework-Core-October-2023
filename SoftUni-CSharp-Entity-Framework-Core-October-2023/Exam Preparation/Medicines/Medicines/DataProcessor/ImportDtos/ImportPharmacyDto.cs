using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [XmlAttribute("non-stop")]
        [RegularExpression(@"^(true|false)$")]
        [Required]
        public string IsNonStop { get; set; }//?

        [Required]
        [XmlElement("Name")]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [XmlElement("PhoneNumber")]
        [MinLength(14)]
        [MaxLength(14)]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$")]//?
        public string PhoneNumber { get; set; }

        [XmlArray("Medicines")]
        public ImportPharmacyMedicineDto[] Medicines { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range

//Pharmacy
//    •	Id – int, Primary Key
//    •	Name – string with length [2, 50] (required)
//    •	PhoneNumber – string with length 14. (required)
//o All phone numbers must have the following structure: three digits enclosed in parentheses, followed by a space, three more digits, a hyphen, and four final digits: 
//    	Example-> (123) 456 - 7890
//    •	IsNonStop – bool  (required)
//    •	Medicines - collection of type Medicine


//pharmacies.xml
//< Pharmacy non - stop = "true" >
//    < Name > Vitality </ Name >
//    < PhoneNumber > (123) 456 - 7890 </ PhoneNumber >
//    < Medicines >
