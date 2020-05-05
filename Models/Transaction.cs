using System; 
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{

    public class Transaction
    {
        [Key]
        public int TransactionId {get; set;}

        [Required]
        public decimal Amount {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public int UserId {get; set;}

        public Users myUser {get; set;}
    }
}