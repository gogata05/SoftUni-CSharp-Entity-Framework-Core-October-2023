using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Medicines.Data.Models
{
    public class PatientMedicine
    {
        [ForeignKey(nameof(Patient))]
        [Required]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        [Required]
        [ForeignKey(nameof(Medicine))]
        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; }
    }
}
//pk,fk,req,max

//PatientMedicine
//    •	PatientId – integer, Primary Key, foreign key (required)
//    •	Patient – Patient
//    •	MedicineId – integer, Primary Key, foreign key (required)
//    •	Medicine – Medicine
