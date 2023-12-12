using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Footballer")]
    public class ImportCoachFootballerDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("ContractStartDate")]
        [Required]
        public string ContractStartDate { get; set; }

        [XmlElement("ContractEndDate")]
        [Required]
        public string ContractEndDate { get; set; }

        [XmlElement("BestSkillType")]
        [Required]
        [Range(0,4)]
        public int BestSkillType { get; set; }

        [XmlElement("PositionType")]
        [Required]
        [Range(0,3)]
        public int PositionType { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//⦁	Name – string with length [2, 40] (required)
//⦁	ContractStartDate – date and time (required)
//⦁	ContractEndDate – date and time (required)
//⦁	BestSkill – enumeration of type BestSkillType, with possible values (Defence, Dribble, Pass, Shoot, Speed) (required)
//⦁	Position - enumeration of type PositionType, with possible values (Goalkeeper, Defender, Midfielder, Forward) (required)

//< Footballer >
//    < Name > Benjamin Bourigeaud </ Name >
//    < ContractStartDate > 22 / 03 / 2020 </ ContractStartDate >
//    < ContractEndDate > 24 / 02 / 2025 </ ContractEndDate >
//    < BestSkillType > 2 </ BestSkillType >
//    < PositionType > 2 </ PositionType >