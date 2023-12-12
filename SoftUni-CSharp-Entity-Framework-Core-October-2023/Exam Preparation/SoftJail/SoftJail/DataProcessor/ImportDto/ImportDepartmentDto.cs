
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }

        public ImportDepartmentCellDto[] Cells { get; set; }
    }
}
//⦁	Name – text with min length 3 and max length 25 (required)
//⦁	Cells - collection of type Cell

//json
//"Name": "",
//"Cells": [
//{