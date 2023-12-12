using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medicines.Data.Models
{
    public class Medicine
    {
        public Medicine()
        {
            this.PatientsMedicines = new HashSet<PatientMedicine>();
        }
        public ICollection<PatientMedicine> PatientsMedicines { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Producer { get; set; }

        [Required]
        [ForeignKey(nameof(Pharmacy))]
        public int PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }
    }
}
//pk,fk,req,max
//ctor

//•	Medicines - collection of type Medicine
//    Medicine
//     •	Id – int, Primary Key
//    •	Name – string with length [3, 150] (required)
//    •	Price – decimal in range [0.01…1000.00] (required)
//    •	Category – Category enum (Analgesic = 0, Antibiotic, Antiseptic, Sedative, Vaccine)(required)
//    •	ProductionDate – DateTime (required)
//    •	ExpiryDate – DateTime (required)
//    •	Producer – string with length [3, 100] (required)
//    •	PharmacyId – int, foreign key (required)
//    •	Pharmacy – Pharmacy
//    •	PatientsMedicines - collection of type PatientMedicine
