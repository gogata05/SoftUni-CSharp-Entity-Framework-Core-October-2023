﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [Range(0,2)]
        public int AgeGroup { get; set; }

        [Required]
        [Range(0,1)]
        public int Gender { get; set; }

        public int[] Medicines { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum int range

//Patient
//    •	Id – integer, Primary Key
//    •	FullName – text with length [5, 100] (required)
//    •	AgeGroup – AgeGroup enum (Child = 0, Adult, Senior)(required)
//    •	Gender – Gender enum (Male = 0, Female)(required)
//    •	PatientsMedicines - collection of type PatientMedicine

//json
//"FullName": "Ivan Petrov",
//"AgeGroup": "1",
//"Gender": "0",
//"Medicines": [
//15,
//23