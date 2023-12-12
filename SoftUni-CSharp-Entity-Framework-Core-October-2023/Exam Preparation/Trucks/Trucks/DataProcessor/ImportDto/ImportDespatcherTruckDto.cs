using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Truck")]
    public class ImportDespatcherTruckDto
    {
        [XmlElement("RegistrationNumber")]
        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        [RegularExpression(@"^[A-Z]{2}[0-9]{4}[A-Z]{2}$")]//?
        public string RegistrationNumber { get; set; }

        [XmlElement("VinNumber")]
        [Required]
        [MinLength(17)]
        [MaxLength(17)]
        public string VinNumber { get; set; }

        [XmlElement("TankCapacity")]
        [Range(950,1420)]
        public int TankCapacity { get; set; }

        [XmlElement("CargoCapacity")]
        [Range(5000,29000)]
        public int CargoCapacity { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        public int CategoryType { get; set; }

        [XmlElement("MakeType")]
        [Required]
        public int MakeType { get; set; }

    }
}
//req,"?",min,max,range,regular,email,enum range

//•	Id – int, Primary Key
//•	RegistrationNumber – string with length 8.      First two characters are upper letters [A-Z], followed by four digits and the last two characters are upper letters [A-Z] again.
//•	VinNumber – string with length 17 (required)
//•	TankCapacity – int in range [950…1420]
//•	CargoCapacity – int in range [5000…29000]
//•	CategoryType – enumeration of type CategoryType, with possible values (Flatbed, Jumbo, Refrigerated, Semi) (required)
//•	MakeType – enumeration of type MakeType, with possible values (Daf, Man, Mercedes, Scania, Volvo) (required)

//xml
//< Truck >
//< RegistrationNumber > CB0796TP </ RegistrationNumber >
//< VinNumber > YS2R4X211D5318181 </ VinNumber >
//< TankCapacity > 1000 </ TankCapacity >
//< CargoCapacity > 23999 </ CargoCapacity >
//< CategoryType > 0 </ CategoryType >
//< MakeType > 3 </ MakeType >

