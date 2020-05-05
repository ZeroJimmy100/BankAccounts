using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BankAccounts.Models{

    public class Users
    {

        [Key]
        public int UserId {get; set;}

        [Required]
        [Display(Name = "First Name")]
        public string FirstName {get; set;}

        [Required]
        [Display(Name = "Last Name")]
        public string LastName {get; set;}

        [Required]
        [Display(Name="Email Address")]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string PassWord {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [Display(Name="Confirm Password")]
        [Compare("PassWord")]
        [DataType(DataType.Password)]
        public string ConfirmPW {get; set;}

        public List<Transaction> AllTransaction {get; set;}
    }
    
}