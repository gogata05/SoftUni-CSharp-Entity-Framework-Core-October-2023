using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Play")] 
    public class ExportPlayDto
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Duration")]
        public string Duration { get; set; }

        [XmlAttribute("Rating")]
        public string Rating { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlArray("Actors")]
        public ExportPlayCastDto[] Actors { get; set; }
    }
}
//    ⦁	Title – text with length [4, 50] (required)
//    ⦁	Duration – TimeSpan in format {hours:minutes: seconds}, with a minimum length of 1 hour. (required)
//    ⦁	Rating – float in the range [0.00….10.00] (required)
//    ⦁	Genre – enumeration of type Genre, with possible values (Drama, Comedy, Romance, Musical) (required)

//    < Play Title = "A Raisin in the Sun" Duration = "01:40:00" Rating = "5.4" Genre = "Drama" >
//    < Actors >
