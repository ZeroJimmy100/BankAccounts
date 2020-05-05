using Microsoft.EntityFrameworkCore;
using System; 

namespace BankAccounts.Models
{

    public class WrapperViewModel
    {
        public Users NewUser {get; set;}

        public Transaction NewTransaction {get; set;}
    }
}