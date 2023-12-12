using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporStore.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace VaporStore.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            this.GameTags = new HashSet<GameTag>();
        }
        public ICollection<GameTag> GameTags { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
