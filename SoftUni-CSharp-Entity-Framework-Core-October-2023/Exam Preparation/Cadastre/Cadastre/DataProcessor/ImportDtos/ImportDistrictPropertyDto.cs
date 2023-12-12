using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("Property")]
    public class ImportDistrictPropertyDto
    {
        [Required]
        [XmlElement("PropertyIdentifier")]
        [MinLength(16)]
        [MaxLength(20)]
        public string PropertyIdentifier { get; set; }

        [Required]
        [XmlElement("Area")]
        [Range(0, 2147483647)]//is this correct?
        public int Area { get; set; }//not negative

        [Required]
        [XmlElement("Details")]
        [MinLength(5)]
        [MaxLength(500)]
        public string Details { get; set; }

        [Required]
        [XmlElement("Address")]
        [MinLength(5)]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; }//DateTime?
    }
}
//req,"?",min,max,range,regular,email,enum int range

//xml
//< Property >
//    < PropertyIdentifier > SF - 10000.001.001.001 </ PropertyIdentifier >
//    < Area > 71 </ Area >
//    < Details > One - bedroom apartment </ Details >
//    < Address > Apartment 5, 23 Silverado Street, Sofia</Address>
//    <DateOfAcquisition>15/03/2022</DateOfAcquisition>

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