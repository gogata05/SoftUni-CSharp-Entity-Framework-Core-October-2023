using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Artillery.Data.Models
{
    public class CountryGun
    {
        [ForeignKey(nameof(Country))]
        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey(nameof(Gun))]
        [Required]
        public int GunId { get; set; }
        public Gun Gun { get; set; }
    }
}
//CountryGun
//    ⦁	CountryId – Primary Key integer, foreign key (required)
//    ⦁	GunId – Primary Key integer, foreign key (required)
