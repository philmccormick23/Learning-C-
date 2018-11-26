using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.Models {
    public class Transaction {

        [Key]
        public int TransactionId { get; set; }

        [Required]
        [RegularExpression (@"^\d+\.\d{0,2}$")]
        public Decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User Accountholder { get; set; }
    }
}