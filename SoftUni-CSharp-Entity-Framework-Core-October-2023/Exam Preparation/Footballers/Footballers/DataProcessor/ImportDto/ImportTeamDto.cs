using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(@"^[A-Za-z0-9\s\.\-]+$")]//?
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Nationality { get; set; }

        [Required]
        public int Trophies { get; set; }

        public int[] Footballers { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range
//⦁	Name – string with length [3, 40].Should contain letters(lower and upper case), digits, spaces, a dot sign ('.') and a dash ('-'). (required)
//⦁	Nationality – string with length [2, 40] (required)
//⦁	Trophies – int (required)

//teams.json
//"Name": "Brentford F.C.",
//"Nationality": "The United Kingdom",
//"Trophies": "5",
//"Footballers": [
//28,
//28,
//39,