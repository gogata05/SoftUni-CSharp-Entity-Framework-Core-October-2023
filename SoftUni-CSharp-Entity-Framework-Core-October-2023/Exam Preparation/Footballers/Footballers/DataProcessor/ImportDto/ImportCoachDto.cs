using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Coach")]
    public class ImportCoachDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("Nationality")]
        [Required]
        public string Nationality { get; set; }

        [XmlArray("Footballers")]
        public ImportCoachFootballerDto[] Footballers { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range
//⦁	Name – string with length [2, 40] (required)
//⦁	Nationality – string (required)
//⦁	Footballers – collection of type Footballer

//< Coach >
//    < Name > Bruno Genesio </ Name >
//    < Nationality > France </ Nationality >
//    < Footballers >