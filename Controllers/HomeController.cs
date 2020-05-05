using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        public HomeController(MyContext context){
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Login Page
        [HttpGet("login")]
        public IActionResult LoginUser()
        {

            return View("Login");
        }

        // Success login User
        [HttpPost("Login/Success")]
        public IActionResult LogUser(Login userSubmission){
             if(ModelState.IsValid)
            {

                Users userInDb = dbContext.TheUsers.FirstOrDefault(u => u.Email == userSubmission.Email);


                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
               
                var hasher = new PasswordHasher<Login>();

                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.PassWord, userSubmission.PassWord);

                int? IDvariable = HttpContext.Session.GetInt32("UserID");
                
                HttpContext.Session.SetInt32("UserID", userInDb.UserId);
                
                if(result == 0){

                }
                return RedirectToAction("Success", new {UserId = userInDb.UserId });
            }
            return View("Login");
        }

           // Register User
        [HttpPost("Register")]

        public IActionResult Register(Users user){
        if(ModelState.IsValid){
            if(dbContext.TheUsers.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
                return View("Index");
            }

                PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                user.PassWord = Hasher.HashPassword(user, user.PassWord);

                dbContext.Add(user);
                dbContext.SaveChanges();

                int? IDvariable = HttpContext.Session.GetInt32("UserID");
                
                HttpContext.Session.SetInt32("UserID", user.UserId);

                return RedirectToAction("Success", new {UserId = user.UserId});
            
        }
        else{
            
            return View("Index");
        }

    }

    // Success Page
    [HttpGet]
    [Route("account/{UserId}")]

    public IActionResult Success(WrapperViewModel allInfo, int UserId){
        Users thisUser = dbContext.TheUsers.Include(use => use.AllTransaction).FirstOrDefault(u => u.UserId == UserId);
        allInfo.NewUser = thisUser;
        Transaction thisTrans = dbContext.TheTransactions.FirstOrDefault(t => t.UserId == thisUser.UserId);
        allInfo.NewTransaction = thisTrans;
        int? IDvariable = HttpContext.Session.GetInt32("UserID");

        if(IDvariable != UserId){
            return View("Index");
        }

        return View("Account", allInfo);
    }

    [HttpPost("DepositWithdraw/{UserId}")]
    public IActionResult DepositOrWithdraw(WrapperViewModel money, int UserId){
        int? IDvariable = HttpContext.Session.GetInt32("UserID");
        Users thisUser = dbContext.TheUsers.Include(use => use.AllTransaction).FirstOrDefault(u => u.UserId == UserId);
        money.NewTransaction.UserId = thisUser.UserId;
        if(ModelState.IsValid){
            if(money.NewTransaction.Amount < 0){
                dbContext.Add(money.NewTransaction);
                dbContext.SaveChanges();
            }else if(money.NewTransaction.Amount > 0){
                dbContext.Add(money.NewTransaction);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Success", new {UserId = thisUser.UserId});
        }else{
            return View("Account");
        }
    }

    [HttpPost("Logout/{UserId}")]
    public IActionResult Logout(int UserId){
        HttpContext.Session.Clear();
        return View("Index");
    }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
