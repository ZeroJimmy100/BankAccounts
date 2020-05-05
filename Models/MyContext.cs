using Microsoft.EntityFrameworkCore;
using System; 

namespace BankAccounts.Models
{

    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> TheUsers {get; set;}

        public DbSet<Transaction> TheTransactions {get; set;}

    }
}