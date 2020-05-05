using System; 
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{

    public class Login
    {

        [Required]
        [Display(Name="Email Address")]
        public string Email {get; set;}

        [Required] 
        [Display(Name="Password")]
        public string PassWord {get; set;}
    }
}