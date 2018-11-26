using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Controllers
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
        [HttpGet]
        [Route("")]
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
[       HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }


        [HttpGet("success")]
        public IActionResult Success()
        {
            var activeuser = dbContext.User.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            if (HttpContext.Session.GetInt32("UserId")!= null){
                ViewBag.AllMessages = dbContext.Messages
                    .Include(p => p.User)
                    .OrderByDescending(post => post.CreatedAt)
                    .Include(post => post.Comments)
                    .ThenInclude(comment => comment.User)
                    .ToList();
                ViewBag.User=activeuser;
                return View();
            }
            else {
                return View("Index");
            }
            
        }

        ////////////////////////////////////////////// END OF LOGIN AND REGISTRATION ///////////////////////////////////////

        [HttpPost("PostMessage")]
        public IActionResult postamessage (Message message)
        {
            Message NewMessage = new Message 
            {
                MessageText=message.MessageText,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
                UserId = (int) HttpContext.Session.GetInt32("UserId")
            };

            dbContext.Add(NewMessage);
            dbContext.SaveChanges();

            return RedirectToAction("Success");
        }

        [HttpPost("PostComment")]
        public IActionResult postacomment (string CommentText, int MessageId)
        {
            Comment NewComment = new Comment 
            {
                CommentText = CommentText,
                MessageId=MessageId,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
                UserId = (int) HttpContext.Session.GetInt32("UserId")
            };

            dbContext.Add(NewComment);
            dbContext.SaveChanges();

            return RedirectToAction("Success");
        }


    }
}
