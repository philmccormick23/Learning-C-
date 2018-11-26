using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ChefsDishes.Models;
using System.Linq;

namespace ChefsDishes.Controllers
{   
    public class HomeController : Controller
    {
        private YourContext dbContext;
        public HomeController(YourContext context)
        {
            dbContext = context;
        }

        // GET: /Home/
        [HttpGet("")]
        public IActionResult Index()
        {
            List <Chef> AllChefs = dbContext.Chefs.Include(Dish=>Dish.Dishes).ToList();
            ViewBag.allchefs = AllChefs;
            return View();
        }

        [HttpGet("AddChef")]

        public IActionResult AddChef()
        {
            return View("AddChef");
        }

        [HttpPost("AddChef")]
        public IActionResult AddChef(Chef chef)
        {
            if(ModelState.IsValid)
            {
                if(chef.Birthday >= DateTime.Today)
                {
                    ModelState.AddModelError("Birthday", "Birthday must be from the past!");
                    return View("AddChef");
                }
                Chef newChef = new Chef
                {
                    First_Name = chef.First_Name,
                    Last_Name = chef.Last_Name,
                    Birthday = chef.Birthday,
                };
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddChef");
            }
        }

        [HttpGet("Dishes")]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes.Include(chef => chef.Creator).ToList();
            ViewBag.alldishes = AllDishes;
            return View("Dishes");
        }

        [HttpGet("AddDish")]
        public IActionResult AddDishView()
        {
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.allchefs = AllChefs;
            return View("AddDish");
        }

        [HttpPost("AddDish")]
        public IActionResult AddDish(Dish dish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                List<Chef> AllChefs = dbContext.Chefs.ToList();
                ViewBag.allchefs = AllChefs;
                return View("AddDish", dish);
            }
        }
    }
}
