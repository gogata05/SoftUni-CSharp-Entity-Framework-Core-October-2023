using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("District")] 
    public class ImportDistrictDto
    {
        [Required]
        [XmlAttribute("Region")]
        public string Region { get; set; }

        [Required]
        [XmlElement("Name")]
        [MinLength(2)]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [XmlElement("PostalCode")]
        [MinLength(8)]
        [MaxLength(8)]
        [RegularExpression(@"^[A-Z]{2}-[0-9]{5}$")]//is this correct?
        public string PostalCode { get; set; }

        [XmlArray("Properties")]
        public ImportDistrictPropertyDto[] Properties { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range

//xml
//< District Region = "SouthWest" >
//    < Name > Sofia </ Name >
//    < PostalCode > SF - 10000 </ PostalCode >
//    < Properties >



//District
//    •	Id – int, Primary Key
//    •	Name – string with length [2, 80] (required)
//    •	PostalCode – string with length 8. All postal codes must have the following structure:starting with two capital letters, followed by e dash '-', followed by five digits. Example: SF - 10000(required)
//    •	Region – Region enum (SouthEast = 0, SouthWest, NorthEast, NorthWest)(required)
//    •	Properties - collection of type Property
