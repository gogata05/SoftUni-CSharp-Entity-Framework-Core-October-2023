using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Artillery.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }
        public ICollection<CountryGun> CountriesGuns { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string CountryName { get; set; }

        [Required]
        public int ArmySize { get; set; }
    }
}
//pk,fk,req,?,max
//ctor
//Country
//    ⦁	Id – integer, Primary Key
//    ⦁	CountryName – text with length [4, 60] (required)
//    ⦁	ArmySize – integer in the range [50_000….10_000_000] (required)
//    ⦁	CountriesGuns – a collection of CountryGun
