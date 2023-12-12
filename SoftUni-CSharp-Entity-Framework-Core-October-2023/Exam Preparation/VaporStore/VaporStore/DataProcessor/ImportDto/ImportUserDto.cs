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
    public class ImportUserDto
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        [Required]
        public ImportUserCardDto[] Cards { get; set; }
    }
}
//json

//Username – text with length [3, 20] (required)
//FullName – text, which has two words, consisting of Latin letters. Both start with an upper letter and are followed by lower letters. The two words are separated by a single space (ex. "John Smith") (required)
//Email – text(required)
//Age – integer in the range [3, 103] (required)
//Cards – collection of type Card 

//[
//{
//    "FullName": "",
//    "Username": "invalid",
//    "Email": "invalid@invalid.com",
//    "Age": 20,
//    "Cards": [
//    {
//        "Number": "1111 1111 1111 1111",
//        "CVC": "111",
//        "Type": "Debit"
//    }
//    ]
//},