using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Artillery.Data.Models
{
    public class Shell
    {
        public Shell()
        {
            this.Guns = new HashSet<Gun>();
        }
        public ICollection<Gun> Guns { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public double ShellWeight { get; set; }

        [Required]
        [MaxLength(30)]
        public string Caliber { get; set; }
    }
}
//pk,fk,req,?,max
//ctor
//Shell
//    ⦁	Id – integer, Primary Key
//    ⦁	ShellWeight – double in range  [2…1_680] (required)
//    ⦁	Caliber – text with length [4…30] (required)
//    ⦁	Guns – a collection of Gun
