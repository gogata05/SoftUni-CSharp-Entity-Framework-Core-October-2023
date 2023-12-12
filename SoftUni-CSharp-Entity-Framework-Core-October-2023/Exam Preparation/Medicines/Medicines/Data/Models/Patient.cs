using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace Medicines.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.PatientsMedicines = new List<PatientMedicine>();
        }
        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public AgeGroup AgeGroup { get; set; }

        [Required]
        public Gender Gender { get; set; }

    }
}
//pk,fk,req,max
//ctor

//Patient
//    •	Id – int, Primary Key
//    •	FullName – string with length [5, 100] (required)
//    •	AgeGroup – AgeGroup enum (Child = 0, Adult, Senior)(required)
//    •	Gender – Gender enum (Male = 0, Female)(required)
//    •	PatientsMedicines - collection of type PatientMedicine
