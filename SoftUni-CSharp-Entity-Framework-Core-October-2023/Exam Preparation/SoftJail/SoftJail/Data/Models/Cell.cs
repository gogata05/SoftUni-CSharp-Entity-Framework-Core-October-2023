using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Cell
    {
        public Cell()
        {
            this.Prisoners  = new HashSet<Prisoner>();
        }
        public ICollection<Prisoner> Prisoners { get; set; }


        [Key]
        public int Id { get; set; }

        [Required]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }

        [Required]//!
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Required]
        public Department Department { get; set; }
        
    }
}
//pk,fk,req,max,?
//ctor
//Cell
//⦁	Id – integer, Primary Key
//⦁	CellNumber – integer in the range [1, 1000] (required)
//⦁	HasWindow – bool (required)
//⦁	DepartmentId – integer, foreign key (required)
//⦁	Department – the cell's department (required)
//⦁	Prisoners – collection of type Prisoner
