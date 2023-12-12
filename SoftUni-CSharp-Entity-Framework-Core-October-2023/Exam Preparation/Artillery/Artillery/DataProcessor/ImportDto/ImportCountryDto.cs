using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Country")] 
    public class ImportCountryDto
    {
        [XmlElement("CountryName")]
        [Required]
        [MinLength(4)]
        [MaxLength(60)]
        public string CountryName { get; set; }

        [XmlElement("ArmySize")]
        [Required]
        [Range(50000, 10000000)]
        public int ArmySize { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//⦁	CountryName – text with length [4, 60] (required)
//⦁	ArmySize – integer in the range [50_000….10_000_000] (required)

//countries.xml
//< Country >
//< CountryName > Afghanistan </ CountryName >
//< ArmySize > 1697064 </ ArmySize >