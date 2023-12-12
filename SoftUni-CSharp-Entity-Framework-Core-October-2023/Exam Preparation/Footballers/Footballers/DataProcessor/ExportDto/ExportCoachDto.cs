using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Footballers.DataProcessor.ExportDto
{
    [XmlType("Coach")]
    public class ExportCoachDto
    {
        [XmlAttribute("FootballersCount")]
        public int FootballersCount { get; set; }

        [XmlElement("CoachName")]
        public string CoachName { get; set; }

        [XmlArray("Footballers")]
        public ExportCoachFootballerDto[] Footballers { get; set; }
    }
}
//⦁	Name – string with length [2, 40] (required)
//⦁	Nationality – string (required)
//⦁	Footballers – collection of type Footballer

//    < Coach FootballersCount = "5" >
//    < CoachName > Pep Guardiola </ CoachName >
//    < Footballers >
