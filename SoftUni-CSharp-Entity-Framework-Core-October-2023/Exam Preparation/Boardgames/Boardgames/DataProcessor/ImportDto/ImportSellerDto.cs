using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellerDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Name {get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        //[RegularExpression(@"www\.[A-Za-z0-9\-]+\.[a-z]{3}")]//?
        [RegularExpression(@"www\.[A-Za-z0-9\-]+\.com")]

        public string Website { get; set; }

        public int[] Boardgames { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range

//•	Name – string with length [5…20] (required)
//•	Address – string with length [2…30] (required)
//•	Country – string (required)
//•	Website – a string (required). First four characters are "www.", followed by upper and lower letters, digits or '-' and the last three characters are ".com".

//"Name": "6am",
//"Address": "The Netherlands",
//"Country": "Belgium",
//"Website": "www.6pm.com",
//"Boardgames": [
//1,
//105,
//1,
//5,
//15