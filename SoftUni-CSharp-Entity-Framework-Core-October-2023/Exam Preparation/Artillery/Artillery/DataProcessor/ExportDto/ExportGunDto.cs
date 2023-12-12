using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Artillery.DataProcessor.ExportDto
{
    [XmlType("Gun")] 
    public class ExportGunDto
    {
        [XmlAttribute("Manufacturer")]
        public string Manufacturer { get; set; }

        [XmlAttribute("GunType")]
        public string GunType { get; set; }

        [XmlAttribute("GunWeight")]
        public int GunWeight { get; set; }

        [XmlAttribute("BarrelLength")]
        public double BarrelLength { get; set; }

        [XmlAttribute("Range")]
        public int Range { get; set; }

        [XmlArray("Countries")]
        public ExportGunCountryDto[] Countries { get; set; }
    }
}
//    ⦁	ManufacturerId – integer, foreign key (required)
//    ⦁	GunWeight– integer in range [100…1_350_000] (required)
//    ⦁	BarrelLength – double in range [2.00….35.00] (required)
//    ⦁	NumberBuild – integer
//    ⦁	Range – integer in range [1….100_000] (required)
//    ⦁	GunType – enumeration of GunType, with possible values (Howitzer, Mortar, FieldGun, AntiAircraftGun, MountainGun, AntiTankGun) (required)
//    ⦁	ShellId – integer, foreign key (required)
//    ⦁	CountriesGuns – a collection of CountryGun

//    < Gun Manufacturer = "Krupp" GunType = "Mortar" GunWeight = "1291272" BarrelLength = "8.31" Range = "14258" >
//    < Countries >
//    < Country Country = "Sweden" ArmySize = "5437337" />
//    < Country Country = "Portugal" ArmySize = "9523599" />
//    </ Countries >
