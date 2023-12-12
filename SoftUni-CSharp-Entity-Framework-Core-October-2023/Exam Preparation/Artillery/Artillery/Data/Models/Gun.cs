using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artillery.Data.Models.Enums;
namespace Artillery.Data.Models
{
    public class Gun
    {
        public Gun()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }
        public ICollection<CountryGun> CountriesGuns { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        [Required]
        public int GunWeight { get; set; }

        [Required]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        public int Range { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [Required]
        [ForeignKey(nameof(Shell))]
        public int ShellId { get; set; }
      
        public Shell Shell { get; set; }
    }
}
//pk,fk,req,?,max
//ctor
//Gun
//    ⦁	Id – integer, Primary Key
//    ⦁	ManufacturerId – integer, foreign key (required)
//    ⦁	GunWeight– integer in range [100…1_350_000] (required)
//    ⦁	BarrelLength – double in range [2.00….35.00] (required)
//    ⦁	NumberBuild – integer
//    ⦁	Range – integer in range [1….100_000] (required)
//    ⦁	GunType – enumeration of GunType, with possible values (Howitzer, Mortar, FieldGun, AntiAircraftGun, MountainGun, AntiTankGun) (required)
//    ⦁	ShellId – integer, foreign key (required)
//    ⦁	CountriesGuns – a collection of CountryGun