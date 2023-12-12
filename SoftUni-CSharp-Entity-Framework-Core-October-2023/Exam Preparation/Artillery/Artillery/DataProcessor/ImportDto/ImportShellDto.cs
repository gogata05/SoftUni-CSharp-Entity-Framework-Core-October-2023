using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ImportShellDto
    {
        [XmlElement("ShellWeight")]
        [Required]
        [Range(2,1680)]
        public double ShellWeight { get; set; }

        [XmlElement("Caliber")]
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Caliber { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//Shell
//    •	Id – integer, Primary Key
//    •	ShellWeight – double in range  [2…1_680] (required)
//    •	Caliber – text with length [4…30] (required)
//    •	Guns – a collection of Gun

//< Shell >
//    < ShellWeight > 50 </ ShellWeight >
//    < Caliber > 155 mm(6.1 in) </ Caliber >