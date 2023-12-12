using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using VaporStore.Data.Models;
namespace VaporStore.DataProcessor.ImportDto
{
    public class ImportUserCardDto
    {
        [Required]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$")]
        public string Cvc { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
//json

//Number – text, which consists of 4 pairs of 4 digits, separated by spaces (ex. "1234 5678 9012 3456") (required)
//Cvc – text, which consists of 3 digits (ex. "123") (required)
//Type – enumeration of type CardType, with possible values ("Debit", "Credit") (required)

//"Cards": [
//{
//    "Number": "1111 1111 1111 1111",
//    "CVC": "111",
//    "Type": "Debit"
//}
//]