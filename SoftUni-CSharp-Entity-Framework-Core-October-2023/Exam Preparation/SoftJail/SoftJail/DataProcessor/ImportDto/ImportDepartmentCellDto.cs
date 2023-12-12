
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentCellDto
    {
        [Required]
        [Range(1,1000)]
        public int CellNumber { get; set; }
        [Required]
        public bool HasWindow { get; set; }
    }
}
//⦁	CellNumber – integer in the range [1, 1000] (required)
//⦁	HasWindow – bool (required)

//"CellNumber": 101,
//"HasWindow": true