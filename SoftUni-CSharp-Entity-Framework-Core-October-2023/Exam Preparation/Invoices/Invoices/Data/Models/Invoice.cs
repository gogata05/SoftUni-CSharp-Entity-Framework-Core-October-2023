using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1500000000)]
        public int Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
//pk,fk,req,max
//⦁	Id – int, Primary Key
//⦁	Number – int in range  [1,000,000,000…1,500,000,000] (required)
//⦁	IssueDate – DateTime (required)
//⦁	DueDate – DateTime (required)
//⦁	Amount – decimal (required)
//⦁	CurrencyType – enumeration of type CurrencyType, with possible values (BGN, EUR, USD) (required)
//⦁	ClientId – int, foreign key (required)
//⦁	Client – Client
