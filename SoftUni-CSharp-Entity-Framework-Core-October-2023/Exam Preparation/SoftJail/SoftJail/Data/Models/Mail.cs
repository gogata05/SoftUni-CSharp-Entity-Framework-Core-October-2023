﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [ForeignKey(nameof(Prisoner))]
        public int PrisonerId { get; set; }
        
        [Required]
        public Prisoner Prisoner { get; set; }
    }
}
//pk,fk,req,?,max
//Mail
// ⦁	Id – integer, Primary Key
// ⦁	Description – text (required)
// ⦁	Sender – text (required)
// ⦁	Address – text, consisting only of letters, spaces and numbers, which ends with "str." (required) (Example: "62 Muir Hill str.")
// ⦁	PrisonerId - integer, foreign key(required)
// ⦁	Prisoner – the mail's Prisoner (required)
