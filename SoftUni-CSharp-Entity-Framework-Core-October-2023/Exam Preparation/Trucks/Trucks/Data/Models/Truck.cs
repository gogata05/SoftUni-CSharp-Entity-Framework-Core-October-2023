﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucks.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Trucks.Data.Models
{
    public class Truck
    {
        public Truck()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }
        public ICollection<ClientTruck> ClientsTrucks { get; set; }

        [Key]
        public int Id {get; set; }

        [MaxLength(8)]
        public string RegistrationNumber { get; set; }

        [Required]
        [MaxLength(17)]
        public string VinNumber { get; set; }

        public int TankCapacity { get; set; }

        public int CargoCapacity { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        public MakeType MakeType { get; set; }

        [Required]
        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId { get; set; }
        
        public Despatcher Despatcher { get; set; }
    }
}
//pk,fk,req,max,?
//ctor
//•	Id – int, Primary Key
//•	RegistrationNumber – string with length 8. First two characters are upper letters [A-Z], followed by four digits and the last two characters are upper letters [A-Z] again.
//•	VinNumber – string with length 17 (required)
//•	TankCapacity – int in range [950…1420]
//•	CargoCapacity – int in range [5000…29000]
//•	CategoryType – enumeration of type CategoryType, with possible values (Flatbed, Jumbo, Refrigerated, Semi) (required)
//•	MakeType – enumeration of type MakeType, with possible values (Daf, Man, Mercedes, Scania, Volvo) (required)
//•	DespatcherId – int, foreign key (required)
//•	Despatcher – Despatcher 
//•	ClientsTrucks – collection of type ClientTruck

