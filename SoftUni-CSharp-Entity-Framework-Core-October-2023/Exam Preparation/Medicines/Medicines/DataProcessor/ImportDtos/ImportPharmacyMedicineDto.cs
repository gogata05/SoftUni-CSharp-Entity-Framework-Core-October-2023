using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportPharmacyMedicineDto
    {
        [XmlAttribute("category")]
        [Required]
        [Range(0,4)]
        public int Category { get; set; }

        [XmlElement("Name")]
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; }


        [XmlElement("Price")]
        [Required]
        [Range(0.01,1000.00)]
        public decimal Price { get; set; }//double?


        [XmlElement("ProductionDate")]
        [Required]
        public string ProductionDate { get; set; }//DateTime?


        [XmlElement("ExpiryDate")]
        [Required]
        public string ExpiryDate { get; set; }//DateTime?


        [XmlElement("Producer")]
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Producer { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range

//Medicine
//    •	Id – int, Primary Key
//    •	Name – string with length [3, 150] (required)
//    •	Price – decimal in range [0.01…1000.00] (required)
//    •	Category – Category enum (Analgesic = 0, Antibiotic, Antiseptic, Sedative, Vaccine)(required)
//    •	ProductionDate – DateTime (required)
//    •	ExpiryDate – DateTime (required)
//    •	Producer – string with length [3, 100] (required)
//    •	PharmacyId – int, foreign key (required)
//    •	Pharmacy – Pharmacy
//    •	PatientsMedicines - collection of type PatientMedicine

//pharmacies.xml
//< Medicine category = "1" >
//    < Name > Ibuprofen </ Name >
//    < Price > 8.50 </ Price >
//    < ProductionDate > 2022 - 02 - 10 </ ProductionDate >
//    < ExpiryDate > 2025 - 02 - 10 </ ExpiryDate >
//    < Producer > ReliefMed Labs </ Producer >
