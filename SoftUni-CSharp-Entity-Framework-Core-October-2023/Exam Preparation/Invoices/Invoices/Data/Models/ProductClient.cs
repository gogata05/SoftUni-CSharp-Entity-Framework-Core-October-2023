﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Invoices.Data.Models
{
    public class ProductClient
    {
        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }


        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
//pk,fk,req,max
//⦁	ProductId – int, Primary Key, foreign key (required)
//⦁	Product – Product
//⦁	ClientId – int, Primary Key, foreign key (required)
//⦁	Client – Client
