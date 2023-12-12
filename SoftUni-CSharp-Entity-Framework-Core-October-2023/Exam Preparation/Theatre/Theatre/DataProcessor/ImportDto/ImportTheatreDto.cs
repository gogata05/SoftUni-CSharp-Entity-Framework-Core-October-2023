using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTheatreDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name {get; set; }

        [Required]
        [Range(1,10)]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Director { get; set; }

        public ImportTheatreTicketDto[] Tickets { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//    ⦁	Name – text with length [4, 30] (required)
//    ⦁	NumberOfHalls – sbyte between [1…10] (required)
//    ⦁	Director – text with length [4, 30] (required)

//"Name": "",
//"NumberOfHalls": 7,
//"Director": "Ulwin Mabosty",
//"Tickets": []