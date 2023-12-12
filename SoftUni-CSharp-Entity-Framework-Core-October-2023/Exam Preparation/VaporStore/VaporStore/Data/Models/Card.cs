using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        public ICollection<Purchase> Purchases { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        [MaxLength(3)]
        public string Cvc { get; set; }

        //[Required]
        public CardType Type { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        //[Required]
        public User User { get; set; }
    }
}
//Number – text, which consists of 4 pairs of 4 digits, separated by spaces (ex. "1234 5678 9012 3456") (required)
//Cvc – text, which consists of 3 digits (ex. "123") (required)
