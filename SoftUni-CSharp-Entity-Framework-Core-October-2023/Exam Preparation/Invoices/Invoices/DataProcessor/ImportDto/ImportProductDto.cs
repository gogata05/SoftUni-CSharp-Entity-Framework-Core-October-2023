using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Invoices.DataProcessor.ImportDto
{
    public class ImportProductDto
    {
        [Required]
        [MinLength(9)]
        [MaxLength(30)]
        public string Name {get; set; }

        [Required]
        [Range(5.00,1000.00)]
        public decimal Price { get; set; }

        [Required]
        [Range(0,4)]
        public int CategoryType { get; set; }

        public int[] Clients { get; set; }
    }
}
//req,?,min,max,range,regular,email,enum range


//⦁	Name – string with length [9…30] (required)
//⦁	Price – decimal in range [5.00…1000.00] (required)
//⦁	CategoryType – enumeration of type CategoryType, with possible values (ADR, Filters, Lights, Others, Tyres) (required)

//products.json
//"Name": "ADR plate",
//"Price": 14.97,
//"CategoryType": 1,
//"Clients": [
//1,
//105,
//1,
//5,
//15