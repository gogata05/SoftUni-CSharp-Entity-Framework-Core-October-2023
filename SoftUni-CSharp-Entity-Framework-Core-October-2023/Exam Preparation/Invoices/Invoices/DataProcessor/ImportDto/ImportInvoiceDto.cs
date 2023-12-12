using Invoices.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Invoices.DataProcessor.ImportDto
{
    public class ImportInvoiceDto
    {
        [Required]
        [Range(1000000000, 1500000000)]
        public int Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Range(0,2)]
        public int CurrencyType { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}
//req,?,min,max,range,regular,email,enum range

//⦁	Number – int in range  [1,000,000,000…1,500,000,000] (required)
//⦁	IssueDate – DateTime (required)
//⦁	DueDate – DateTime (required)
//⦁	Amount – decimal (required)
//⦁	CurrencyType – enumeration of type CurrencyType, with possible values (BGN, EUR, USD) (required)
//⦁	ClientId – int, foreign key (required)

//invoices.json
//"Number": 1427940691,
//"IssueDate": "2022-08-29T00:00:00",
//"DueDate": "2022-10-28T00:00:00",
//"Amount": 913.13,
//"CurrencyType": 1,
//"ClientId": 1