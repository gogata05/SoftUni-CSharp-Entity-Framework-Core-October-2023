﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Boardgames.Data.Models
{
    public class Seller
    {
        public Seller()
        {
            this.BoardgamesSellers = new HashSet<BoardgameSeller>();
        }
        public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Website { get; set; }
    }
}
//pk,fk,req,max,?
//ctor
//•	Id – int, Primary Key
//•	Name – string with length [5…20] (required)
//•	Address – string with length [2…30] (required)
//•	Country – string (required)
//•	Website – a string (required). First four characters are "www.", followed by upper and lower letters, digits or '-' and the last three characters are ".com".
//•	BoardgamesSellers – collection of type BoardgameSeller
