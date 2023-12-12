using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Medicine")]
    public class ExportPatientMedicineDto
    {
        [XmlAttribute("Category")]
            public string Category { get; set; }

            [XmlElement("Name")]
            public string Name { get; set; }

            [XmlElement("Price")]
            public string Price { get; set; }

            [XmlElement("Producer")]
            public string Producer { get; set; }

            [XmlElement("BestBefore")]
            public string BestBefore { get; set; }//DateTime?

    }
}
//< Medicine Category = "antibiotic" >
//    < Name > Aleve(Naproxen) </ Name >
//    < Price > 10.50 </ Price >
//    < Producer > HealthCare Pharma </ Producer >
//    < BestBefore > 2025 - 09 - 01 </ BestBefore >
