using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Boardgames.Data.Models
{
    public class Boardgame
    {
        public Boardgame()
        {
            this.BoardgamesSellers = new HashSet<BoardgameSeller>();
        }
        public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public int YearPublished { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        public string Mechanics { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }

        public Creator Creator { get; set; }
    }
}
//pk,fk,req,max
//ctor
//•	Id – int, Primary Key
//•	Name – string with length [10…20] (required)
//•	Rating – double in range [1…10.00] (required)
//•	YearPublished – int in range [2018…2023] (required)
//•	CategoryType – enumeration of type CategoryType, with possible values (Abstract, Children, Family, Party, Strategy) (required)
//•	Mechanics – string (string, not an array) (required)
//•	CreatorId – int, foreign key (required)
//•	Creator – Creator
//•	BoardgamesSellers – collection of type BoardgameSeller

