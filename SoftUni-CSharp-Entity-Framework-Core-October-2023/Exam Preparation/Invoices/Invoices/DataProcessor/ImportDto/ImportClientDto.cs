using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Client")] 
    public class ImportClientDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(10)]
        [MaxLength(25)]
        public string Name { get; set; }

        [XmlElement("NumberVat")]
        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string NumberVat { get; set; }

        [XmlArray("Addresses")]
        public ImportClientAddressDto[] Addresses { get; set; }
    }
}
//req,?,min,max,range,regular,email
//⦁	Name – string with length [10…25] (required)
//⦁	NumberVat – string with length [10…15] (required)
//⦁	Addresses – collection of type Address

//clients.xml
//< Client >
//< Name > LiCB </ Name >
//    < NumberVat > BG5464156654654654 </ NumberVat >
//    < Addresses >