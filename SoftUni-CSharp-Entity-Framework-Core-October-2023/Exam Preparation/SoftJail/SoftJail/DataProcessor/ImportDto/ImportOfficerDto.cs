
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficerDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [XmlElement("Money")]
        //[Range(typeof(decimal),"0.0","9223372036854775807.0")]
        [Range(0, 9223372036854775807)]
        public decimal Money { get; set; }

        [Required]
        [XmlElement("Position")]
        public string Position {get; set; }

        [Required]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        [Required]
        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public ImportOfficerPrisonerDto[] Prisoners { get; set; }
    }
}
//req,?,min,max,range,regular,email,enum range

//⦁	FullName – text with min length 3 and max length 30 (required)
//⦁	Salary – decimal (non-negative, minimum value: 0) (required)
//⦁	Position – Position enumeration with possible values: "Overseer, Guard, Watcher, Labour"(required)
//⦁	Weapon – Weapon enumeration with possible values: "Knife, FlashPulse, ChainRifle, Pistol, Sniper"(required)
//⦁	DepartmentId – integer, foreign key (required)

//ImportOfficersPrisoners.xml
//< Officer >
//    < Name > Minerva Kitchingman </ Name >
//    < Money > 2582 </ Money >
//    < Position > Invalid </ Position >
//    < Weapon > ChainRifle </ Weapon >
//    < DepartmentId > 2 </ DepartmentId >
//    < Prisoners >