using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using LoginAndReg.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace LoginAndReg.Controllers
{
    
    public class HomeController : Controller
    {
        private YourContext dbContext;
 
        // here we can "inject" our context service into the constructor
        public HomeController(YourContext context)
        {
            dbContext = context;
        }

        // GET: /Home/
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User adduser)
        {
            if(ModelState.IsValid)
            {
                if (dbContext.User.Any(user => user.Email == adduser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User AddUserinDB = new User
                {
                    FirstName = adduser.FirstName,
                    LastName = adduser.LastName,
                    Email = adduser.Email,
                    Password = adduser.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                AddUserinDB.Password = Hasher.HashPassword(AddUserinDB, AddUserinDB.Password);
                dbContext.Add(AddUserinDB);
                dbContext.SaveChanges();
                AddUserinDB = dbContext.User.SingleOrDefault(user => user.Email == AddUserinDB.Email);

                var activeuser = dbContext.User.SingleOrDefault(user => user.Email ==adduser.Email);
                HttpContext.Session.SetString("Loggedinuser", activeuser.FirstName + " " + activeuser.LastName);
                HttpContext.Session.SetInt32("UserId", AddUserinDB.UserId);
                ViewBag.UserName=HttpContext.Session.GetString("Loggedinuser");
                System.Console.WriteLine(HttpContext.Session.GetString("Loggedinuser"));

                
                return RedirectToAction ("Success");
            }
            else
            {
                return View("Index", adduser);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser loginuser)
        {
            if(ModelState.IsValid) {
                var Hasher = new PasswordHasher<User>();
                User usercheck = dbContext.User.SingleOrDefault(user => user.Email == loginuser.loginemail);
                if (usercheck == null || 0 == Hasher.VerifyHashedPassword(usercheck, usercheck.Password, loginuser.loginpw))
                {
                    ViewBag.Message = "You could not be logged in. Please try again.";
                    return View("Login");
                }
                else
                {
                    var activeuser = dbContext.User.SingleOrDefault(user => user.Email ==loginuser.loginemail);
                    HttpContext.Session.SetString("Loggedinuser", activeuser.FirstName + " " + activeuser.LastName);
                    HttpContext.Session.SetInt32("UserId", usercheck.UserId);
                    return RedirectToAction("Success");
                }
            }

            else {
                return View("Login");
            }
            
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            var activeuser = dbContext.User.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            if (HttpContext.Session.GetInt32("UserId")!= null){
                ViewBag.User=activeuser;
                return View();
            }
            else {
                return View("Index");
            }
            
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }


    }
}
