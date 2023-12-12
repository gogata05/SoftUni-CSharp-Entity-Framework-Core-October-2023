using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ExportDtos
{
    [XmlType("Property")]
    public class ExportPropertyDto
    {
        [XmlAttribute("postal-code")]
        public string PostalCode  { get; set; }

        [XmlElement("PropertyIdentifier")]
        public string PropertyIdentifier { get; set; }

        [XmlElement("Area")]
        public int Area { get; set; }

        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; }//DateTime
    }
}


//    < Property postal - code = "VA-90000" >
//    < PropertyIdentifier > VA - 90000.003.005.005 </ PropertyIdentifier >
//    < Area > 2300 </ Area >
//    < DateOfAcquisition > 28 / 08 / 2008 </ DateOfAcquisition >
