﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatre.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public sbyte RowNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Play))]
        public int PlayId { get; set; }

        public Play Play { get; set; }

        [Required]
        [ForeignKey(nameof(Theatre))]
        public int TheatreId { get; set; }

        public Theatre Theatre { get; set; }
    }
}
//pk,fk,req,?,max
//Ticket
//    ⦁	Id – integer, Primary Key
//    ⦁	Price – decimal in the range [1.00….100.00] (required)
//    ⦁	RowNumber – sbyte in range [1….10] (required)
//    ⦁	PlayId – integer, foreign key (required)
//    ⦁	TheatreId – integer, foreign key (required)
