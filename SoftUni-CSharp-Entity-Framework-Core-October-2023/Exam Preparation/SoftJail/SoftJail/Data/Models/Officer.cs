using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SoftJail.Data.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Officer
    {
        public Officer()
        {
            this.OfficerPrisoners = new HashSet<OfficerPrisoner>();
        }
        public virtual ICollection<OfficerPrisoner> OfficerPrisoners { get; set; }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public Position Position { get; set; }

        [Required]
        public Weapon Weapon { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Required]
        public Department Department { get; set; }

    }
}
//pk,fk,req,max,?
//ctor
//Officer
//⦁	Id – integer, Primary Key
//⦁	FullName – text with min length 3 and max length 30 (required)
//⦁	Salary – decimal (non-negative, minimum value: 0) (required)
//⦁	Position – Position enumeration with possible values: "Overseer, Guard, Watcher, Labour"(required)
//⦁	Weapon – Weapon enumeration with possible values: "Knife, FlashPulse, ChainRifle, Pistol, Sniper"(required)
//⦁	DepartmentId – integer, foreign key (required)
//⦁	Department – the officer's department (required)
//⦁	OfficerPrisoners – collection of type OfficerPrisoner

