using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Invoices.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Addresses = new HashSet<Address>();
            this.ProductsClients = new HashSet<ProductClient>();
        }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ProductClient> ProductsClients { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string NumberVat { get; set; }
    }
}
//pk,fk,req,max
//ctor
//⦁	Id – int, Primary Key
//⦁	Name – string with length [10…25] (required)
//⦁	NumberVat – string with length [10…15] (required)
//⦁	Invoices – collection of type Invoicе
//⦁	Addresses – collection of type Address
//⦁	ProductsClients – collection of type ProductClient
