using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Trucks.DataProcessor.ImportDto
{
    public class ImportClientDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name {get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Nationality { get; set; }

        [Required]
        public string Type { get; set; }

        public int[] Trucks { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//•	Name – string with length [3, 40] (required)
//•	Nationality – string with length [2, 40] (required)
//•	Type – string (required)

//"Name": "Kuenehne + Nagel (AG & Co.) KGKuenehne + Nagel (AG & Co.) KGKuenehne + Nagel (AG & Co.) KG",
//"Nationality": "The Netherlands",
//"Type": "golden",
//"Trucks": [
//1,
//68,
//73,
//17,
//98,
//98