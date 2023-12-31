﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Medicines.Data.Models
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            this.Medicines = new HashSet<Medicine>();
        }
        public ICollection<Medicine> Medicines { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(14)]
        public string PhoneNumber { get; set;}

        [Required]
        public bool IsNonStop { get; set; }
        
    }
}
//pk,fk,req,max
//ctor

//Pharmacy
//    •	Id – integer, Primary Key
//    •	Name – text with length [2, 50] (required)
//    •	PhoneNumber – text with length 14. (required)
//o All phone numbers must have the following structure: three digits enclosed in parentheses, followed by a space, three more digits, a hyphen, and four final digits: 
//    	Example-> (123) 456 - 7890
//    •	IsNonStop – bool  (required)
//    •	Medicines - collection of type Medicine
