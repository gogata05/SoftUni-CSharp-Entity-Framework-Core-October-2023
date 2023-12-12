using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Manufacturer")]
    public class ImportManufacturerDto
    {
        [XmlElement("ManufacturerName")]
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string ManufacturerName { get; set; }

        [XmlElement("Founded")]
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Founded { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//⦁	ManufacturerName – unique text with length [4…40] (required)
//⦁	Founded – text with length [10…100] (required)

//manufacturers.xml
//< Manufacturer >
//< ManufacturerName > BAE Systems </ ManufacturerName >
//< Founded > 30 November 1999, London, England</Founded>