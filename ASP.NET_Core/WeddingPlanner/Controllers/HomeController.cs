using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
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

        [HttpGet("dashboard")]
        public IActionResult Success()
        {
            var activeuser = dbContext.User.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            if (HttpContext.Session.GetInt32("UserId")!= null){
                ViewBag.User=activeuser;

                List<Wedding>AllWeddings=dbContext.Wedding
                    .OrderBy(q => q.Date)
                    .Include(w => w.Planner)
                    .Include(w =>w.Guests)
                    .ThenInclude(g => g.Attendee).ToList();
                ViewBag.AllWeddings=AllWeddings;

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

        //////////////////////////////////////// END OF LOGIN AND REGISTRATION ////////////////////////////////////////

        [HttpGet("wedding")]
        public IActionResult Wedding()
        {
            
            return View();
        }

        [HttpPost("NewWedding")]
        public IActionResult NewWedding(Wedding wedding)
        {   

            if(ModelState.IsValid) {
                if(wedding.Date <= DateTime.Today) 
                {
                    ModelState.AddModelError("Date", "Date cannot be in the past");
                    return View ("Wedding");
                }
                else 
                {
                    int? ActiveUserId = HttpContext.Session.GetInt32("UserId");
            
                    User User = dbContext.User.Where(u => u.UserId == ActiveUserId).SingleOrDefault();
                    Wedding newWedding = new Wedding 
                    {
                        WedderOne = wedding.WedderOne,
                        WedderTwo = wedding.WedderTwo,
                        Date = wedding.Date,
                        Address = wedding.Address,
                        Planner = User,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };

                    dbContext.Add(newWedding);
                    dbContext.SaveChanges();
                    return RedirectToAction ("Success");
                }
            }
            else {
                return View ("Wedding");
            }
        }

        [HttpGet("wedding/{WeddingId}")]
        public IActionResult ShowWedding(int WeddingId)
        {
            Wedding wedding = dbContext.Wedding.SingleOrDefault(p => p.WeddingId == WeddingId);
            ViewBag.wedding = wedding;
            return View();
        }

        [HttpGet("rsvp/{WeddingId}")]
        public IActionResult RSVP(int WeddingId)
        {
            int? userId = HttpContext.Session.GetInt32 ("UserId");

            User userToJoin = dbContext.User
                .FirstOrDefault (u => u.UserId == userId);

            Wedding weddingToJoin = dbContext.Wedding
                .Include (g => g.Guests)
                .FirstOrDefault (w => w.WeddingId == WeddingId);
            Guest newGuest = new Guest
            {
                UserId = (int) userId,
                WeddingId = WeddingId,
                Attendee = userToJoin,
                WeddingAttended = weddingToJoin
            };
            dbContext.Guests.Add(newGuest);
            dbContext.SaveChanges();
            return RedirectToAction ("Success");
        }

        [HttpGet("unrsvp/{WeddingId}")]
        public IActionResult UNRSVP(int WeddingId)
        {
            int? userId = HttpContext.Session.GetInt32 ("UserId");
            Guest rsvp = dbContext.Guests
                .FirstOrDefault (g => g.WeddingId == WeddingId && g.UserId == userId);

            dbContext.Guests.Remove(rsvp);
            dbContext.SaveChanges();
            return RedirectToAction ("Success");
        }

        [HttpGet("delete/{WeddingId}")]
        public IActionResult DeleteWedding(int WeddingId)
        {
            Wedding deletedWedding = dbContext.Wedding.FirstOrDefault(p => p.WeddingId == WeddingId);
            dbContext.Wedding.Remove(deletedWedding);
            dbContext.SaveChanges();
            return RedirectToAction ("Success");
        }


    }
}
