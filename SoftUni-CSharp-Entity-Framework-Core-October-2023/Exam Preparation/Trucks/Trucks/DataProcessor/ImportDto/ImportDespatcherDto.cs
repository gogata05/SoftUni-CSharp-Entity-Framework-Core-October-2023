using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Despatcher")]
    public class ImportDespatcherDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name {get; set; }

        [XmlElement("Position")]
        [Required]
        public string Position { get; set; }

        [XmlArray("Trucks")]
        public ImportDespatcherTruckDto[] Trucks { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//•	Name – text with length [2, 40] (required)
//•	Position – text
//•	Trucks – collection of type Truck

//despatchers.xml
//< Name > Genadi Petrov </ Name >
//< Position > Specialist </ Position >
//< Trucks >