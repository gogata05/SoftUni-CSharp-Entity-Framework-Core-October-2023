using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporStore.Data.Models;
namespace VaporStore.DataProcessor.ImportDto
{
    public class ImportGameDto
    {
        [Required]
        public string Name {get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        
        public string[] Tags { get; set; }//Each game must have at least one tag.
    }
}
//json

//Name – text (required) 
//Price – decimal (non-negative, minimum value: 0) (required)
//ReleaseDate – Date(required)
//Developer – the game's developer (required) 
//Genre – the game's genre (required) 
//GameTags - collection of type GameTag. Each game must have at least one tag.

//{
//    "Name": "Invalid",
//    "Price": -5,
//    "ReleaseDate": "2013-07-09",
//    "Developer": "Valid Dev",
//    "Genre": "Valid Genre",
//    "Tags": [

//    "Valid Tag"
//        ]
//},
