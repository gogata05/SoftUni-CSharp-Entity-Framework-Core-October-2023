using System.ComponentModel.DataAnnotations;
namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^The [A-Z][a-zA-Z]*$")]//?
        public string Nickname { get; set; }

        [Required]
        [Range(18,65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string? ReleaseDate { get; set; }

        //[Range(typeof(decimal),"0.0", "9223372036854775807.0")]
        [Range(0, 9223372036854775807)]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public ImportPrisonerMailDto[] Mails { get; set; }
    }
}
//⦁	FullName – text with min length 3 and max length 20 (required)
//⦁	Nickname – text starting with "The " and a single word only of letters with an uppercase letter for beginning(example: The Prisoner) (required)
//⦁	Age – integer in the range [18, 65] (required)
//⦁	IncarcerationDate – Date (required)
//⦁	ReleaseDate – Date
//⦁	Bail – decimal (non-negative, minimum value: 0)
//⦁	CellId - integer, foreign key
//⦁	Cell – the prisoner's cell

//json
//"FullName": "",
//"Nickname": "The Wallaby",
//"Age": 32,
//"IncarcerationDate": "29/03/1957",
//"ReleaseDate": "27/03/2006",
//"Bail": null,
//"CellId": 5,
//"Mails": [