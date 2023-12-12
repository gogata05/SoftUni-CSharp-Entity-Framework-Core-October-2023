using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTheatreTicketDto
    {
        [Required]
        [Range(1.00,100.00)]
        public decimal Price { get; set; }

        [Required]
        [Range(1,10)]
        public sbyte RowNumber { get; set; }

        [Required]
        public int PlayId { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//    ⦁	Price – decimal in the range [1.00….100.00] (required)
//    ⦁	RowNumber – sbyte in range [1….10] (required)
//    ⦁	PlayId – integer, foreign key (required)

//"Price": 7.63,
//"RowNumber": 5,
//"PlayId": 4