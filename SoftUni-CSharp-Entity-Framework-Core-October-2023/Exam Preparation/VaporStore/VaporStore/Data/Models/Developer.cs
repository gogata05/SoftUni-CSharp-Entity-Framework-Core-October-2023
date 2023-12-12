using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace VaporStore.Data.Models
{
    public class Developer
    {

        public Developer()
        {
            this.Games = new HashSet<Game>();
        }
        public ICollection<Game> Games { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
    }
}
