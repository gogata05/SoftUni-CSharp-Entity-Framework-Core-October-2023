using Boardgames.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class ImportCreatorBoardgameDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string Name { get; set; }    

        [XmlElement("Rating")]
        [Required]
        [Range(1.00,10.00)]
        public double Rating { get; set; }

        [XmlElement("YearPublished")]
        [Required]
        [Range(2018,2023)]
        public int YearPublished { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        [Range(0,4)]
        public int CategoryType { get; set; }

        [XmlElement("Mechanics")]
        [Required]
        public string Mechanics { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range
//•	Name – string with length [10…20] (required)
//•	Rating – double in range [1…10.00] (required)
//•	YearPublished – int in range [2018…2023] (required)
//•	CategoryType – enumeration of type CategoryType, with possible values (Abstract, Children, Family, Party, Strategy) (required)
//•	Mechanics – string (string, not an array) (required)

//creators.xml
//< Boardgame >
//    < Name > 4 Gods </ Name >
//    < Rating > 7.28 </ Rating >
//    < YearPublished > 2017 </ YearPublished >
//    < CategoryType > 4 </ CategoryType >
//    < Mechanics > Area Majority / Influence, Hand Management, Set Collection, Simultaneous Action Selection, Worker Placement</Mechanics>
