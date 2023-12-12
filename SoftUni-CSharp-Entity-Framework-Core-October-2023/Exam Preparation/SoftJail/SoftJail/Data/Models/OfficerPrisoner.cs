using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SoftJail.Data.Models
{
    public class OfficerPrisoner
    {
        [ForeignKey(nameof(Prisoner))]
        public int PrisonerId { get; set; }

        [Required]
        public  Prisoner Prisoner { get; set; }

        [ForeignKey(nameof(Officer))]
        public int OfficerId { get; set; }

        [Required]
        public  Officer Officer { get; set; }
    }
}
//pk,fk,req,?,max
//OfficerPrisoner
// ⦁	PrisonerId – integer, Primary Key
// ⦁	Prisoner – the officer's prisoner (required)
// ⦁	OfficerId – integer, Primary Key
// ⦁	Officer – the prisoner's officer (required)
