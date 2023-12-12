using System.ComponentModel.DataAnnotations;
namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerMailDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+str\.$")]//?
        //consisting only of letters, spaces and numbers, which ends with "str." (required) (Example: "62 Muir Hill str.") 
        public string Address { get; set; }
    }
}
// ⦁	Description – text (required)
// ⦁	Sender – text (required)
// ⦁	Address – text, consisting only of letters, spaces and numbers, which ends with "str." (required) (Example: "62 Muir Hill str.")

//"Description": "Invalid FullName",
//"Sender": "Invalid Sender",
//"Address": "No Address"