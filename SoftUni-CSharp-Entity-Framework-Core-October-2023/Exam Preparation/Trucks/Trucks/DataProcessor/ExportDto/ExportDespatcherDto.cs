using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Trucks.Data.Models;
namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")] 
    public class ExportDespatcherDto
    {
        [XmlAttribute("TrucksCount")]
        public int TrucksCount { get; set; }//?

        [XmlElement("DespatcherName")]
        public string DespatcherName { get; set; }

        [XmlArray("Trucks")]
        public ExportDespatcherTruckDto[] Trucks { get; set; }
    }
}

//•	Name – string with length [2, 40] (required)
//•	Trucks – collection of type Truck

//< Despatcher TrucksCount = "6" >
//< DespatcherName > Vladimir Hristov </ DespatcherName >
//< Trucks >
