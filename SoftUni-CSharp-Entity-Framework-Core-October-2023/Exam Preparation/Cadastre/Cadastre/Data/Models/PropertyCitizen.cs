﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cadastre.Data.Models
{
    public class PropertyCitizen
    {
        [Required]
        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }


        [Required]
        [ForeignKey(nameof(Citizen))]
        public int CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}
//pk,fk,req,max

//PropertyCitizen
//    •	PropertyId – integer, Primary Key, foreign key (required)
//    •	Property – Property
//    •	CitizenId – integer, Primary Key, foreign key (required)
//    •	Citizen – Citizen
