using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Message")]
    public class ExportPrisonerMailDto
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
//xml

//      < Message >
//      < Description > !? sdnasuoht evif - ytnewt rof deksa uoy ro orez artxe na ereht sI</Description> 
//      </Message> 