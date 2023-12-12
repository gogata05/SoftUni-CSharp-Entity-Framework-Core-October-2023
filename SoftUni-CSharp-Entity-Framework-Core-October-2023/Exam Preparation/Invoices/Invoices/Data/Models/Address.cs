using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Invoices.Data.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string StreetName { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        [MaxLength(15)]
        public string City { get; set; }

        [Required]
        [MaxLength(15)]
        public string Country { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
//pk,fk,req,max
//⦁	Id – int, Primary Key
//⦁	StreetName – string with length [10…20] (required)
//⦁	StreetNumber – int (required)
//⦁	PostCode – string (required)
//⦁	City – string with length [5…15] (required)
//⦁	Country – string with length [5…15] (required)
//⦁	ClientId – int, foreign key (required)
//⦁	Client – Client

