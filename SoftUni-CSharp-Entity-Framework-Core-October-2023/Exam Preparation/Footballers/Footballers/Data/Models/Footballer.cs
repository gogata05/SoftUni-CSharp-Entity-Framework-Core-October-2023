using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footballers.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Footballers.Data.Models
{
    public class Footballer
    {
        public Footballer()
        {
            this.TeamsFootballers = new HashSet<TeamFootballer>();
        }
        public ICollection<TeamFootballer> TeamsFootballers { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public DateTime ContractStartDate { get; set; }

        [Required]
        public DateTime ContractEndDate { get; set; }

        [Required]
        public PositionType PositionType { get; set; }

        [Required]
        public BestSkillType BestSkillType { get; set; }

        [Required]
        [ForeignKey(nameof(Coach))]
        public int CoachId { get; set; }

        public Coach Coach { get; set; }
    }
}
//pk,fk,req,max
//ctor
//⦁	Id – int, Primary Key
//⦁	Name – string with length [2, 40] (required)
//⦁	ContractStartDate – date and time (required)
//⦁	ContractEndDate – date and time (required)
//⦁	Position - enumeration of type PositionType, with possible values (Goalkeeper, Defender, Midfielder, Forward) (required)
//⦁	BestSkill – enumeration of type BestSkillType, with possible values (Defence, Dribble, Pass, Shoot, Speed) (required)
//⦁	CoachId – int, foreign key (required)
//⦁	Coach – Coach 
//⦁	TeamsFootballers – collection of type TeamFootballer
