using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers {
    public class HomeController : Controller {
        private YourContext dbContext;
        public HomeController (YourContext context) {
            dbContext = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ();
        }

        [HttpGet ("login")]
        public IActionResult Login () {
            return View ();
        }

        [HttpPost]
        [Route ("Register")]
        public IActionResult Register (User adduser) {
            if (ModelState.IsValid) {
                if (dbContext.User.Any (user => user.Email == adduser.Email)) {
                    ModelState.AddModelError ("Email", "Email already in use!");
                    return View ("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                User AddUserinDB = new User {
                    FirstName = adduser.FirstName,
                    LastName = adduser.LastName,
                    Email = adduser.Email,
                    Password = adduser.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                AddUserinDB.Password = Hasher.HashPassword (AddUserinDB, AddUserinDB.Password);
                dbContext.Add (AddUserinDB);
                dbContext.SaveChanges ();
                AddUserinDB = dbContext.User.SingleOrDefault (user => user.Email == AddUserinDB.Email);

                var activeuser = dbContext.User.SingleOrDefault (user => user.Email == adduser.Email);
                HttpContext.Session.SetString ("Loggedinuser", activeuser.FirstName + " " + activeuser.LastName);
                HttpContext.Session.SetInt32 ("UserId", AddUserinDB.UserId);
                ViewBag.UserName = HttpContext.Session.GetString ("Loggedinuser");
                System.Console.WriteLine (HttpContext.Session.GetString ("Loggedinuser"));

                return RedirectToAction ("Account", new { accnumber = HttpContext.Session.GetInt32 ("UserId") });
            } else {
                return View ("Index", adduser);
            }
        }

        [HttpPost ("login")]
        public IActionResult Login (LoginUser loginuser) {
            if (ModelState.IsValid) {
                var Hasher = new PasswordHasher<User> ();
                User usercheck = dbContext.User.SingleOrDefault (user => user.Email == loginuser.loginemail);
                if (usercheck == null || 0 == Hasher.VerifyHashedPassword (usercheck, usercheck.Password, loginuser.loginpw)) {
                    ViewBag.Message = "You could not be logged in. Please try again.";
                    return View ("Login");
                } else {
                    var activeuser = dbContext.User.SingleOrDefault (user => user.Email == loginuser.loginemail);
                    HttpContext.Session.SetString ("Loggedinuser", activeuser.FirstName + " " + activeuser.LastName);
                    HttpContext.Session.SetInt32 ("UserId", usercheck.UserId);
                    return RedirectToAction ("Account", new { accnumber = HttpContext.Session.GetInt32 ("UserId") });
                }
            } else {
                return View ("Login");
            }

        }

        [HttpGet ("success")]
        public IActionResult Success () {
            var activeuser = dbContext.User.SingleOrDefault (user => user.UserId == HttpContext.Session.GetInt32 ("UserId"));
            if (HttpContext.Session.GetInt32 ("UserId") != null) {
                ViewBag.User = activeuser;
                return View ();
            } else {
                return View ("Index");
            }

        }

        [HttpGet ("Logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return View ("Index");
        }

        ///////////////////////////////////// END OF LOGIN AND REGISTRATION  ///////////////////////////////////// 

        [HttpGet ("account/{accnumber}")]
        public IActionResult Account (int accnumber) {
            List<Transaction> MyTransactions = dbContext.Transactions.Where (transaction => transaction.UserId == accnumber).OrderByDescending (transaction => transaction.CreatedAt).ToList ();
            ViewBag.MyTransactions = MyTransactions;
            Console.WriteLine (ViewBag.MyTransactions.Count);
            ViewBag.Balance = 0;
            foreach (Transaction trans in MyTransactions) {
                ViewBag.Balance += trans.Amount;
            }
            if (ViewBag.Balance <= 0) {
                ViewBag.Minimum = 0;
            } else {
                ViewBag.Minimum = 0 - ViewBag.Balance;
            }
            ViewBag.Accountholder = dbContext.User.SingleOrDefault (user => user.UserId == accnumber).FirstName;
            return View ();
        }

        [HttpPost ("Transact")]
        public IActionResult Transact (double Amount) {
            Console.WriteLine ($"The amount received from the transaction form is {Amount}.");
            User Transactor = dbContext.User.SingleOrDefault (user => user.UserId == HttpContext.Session.GetInt32 ("UserId"));
            double balance = 0;
            List<Transaction> MyTransactions = dbContext.Transactions.Where(transaction => transaction.UserId == Transactor.UserId).ToList();
            foreach (Transaction trans in MyTransactions) {
                balance += (double) trans.Amount;
            }
            if (balance + Amount >= 0 && Transactor != null) {
                // new transaction
                Transaction NewTransaction = new Transaction {
                Amount = (decimal) Amount,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Transactor.UserId
                };
                dbContext.Transactions.Add(NewTransaction);
                dbContext.SaveChanges ();
                return RedirectToAction("Account", new { accnumber = HttpContext.Session.GetInt32("UserId")});
            } else {
                // return error message
                return View ("Account");
            }

        }
    }
}