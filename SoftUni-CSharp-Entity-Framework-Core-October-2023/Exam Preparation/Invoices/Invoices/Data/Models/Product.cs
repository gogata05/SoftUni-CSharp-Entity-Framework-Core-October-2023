using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace Invoices.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.ProductsClients = new HashSet<ProductClient>();
        }
        public virtual ICollection<ProductClient> ProductsClients { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public decimal Price { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }
    }
}
//pk,fk,req,max
//ctor
//⦁	Id – int, Primary Key
//⦁	Name – string with length [9…30] (required)
//⦁	Price – decimal in range [5.00…1000.00] (required)
//⦁	CategoryType – enumeration of type CategoryType, with possible values (ADR, Filters, Lights, Others, Tyres) (required)
//⦁	ProductsClients – collection of type ProductClient

