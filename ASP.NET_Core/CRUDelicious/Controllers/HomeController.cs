using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models;
using System.Linq;


namespace CRUDelicious.Controllers
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
            List <Dishes> ReturnedDishes = dbContext.Dishes.ToList();
            ViewBag.AllRecipes = ReturnedDishes; 
            return View();
        }

        [HttpGet("new")]
        public IActionResult NewRecipe()
        {
            //System.Console.WriteLine("second");
            return View();
        }

        [HttpPost("addnew")]
        public IActionResult Addnew (Dishes dish)
        {
            if(ModelState.IsValid)
            {
                Dishes newDish = new Dishes
                {
                    Name = dish.Name,
                    Chef = dish.Chef,
                    Tastiness = dish.Tastiness,
                    Calories = dish.Calories,
                    Description = dish.Description
                
                };
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                System.Console.WriteLine(newDish);
                return RedirectToAction("Index");
            }
            else
            {
                System.Console.WriteLine("There were some problems....");
                return View("NewRecipe");
            }
        }

        [HttpGet("{id}")]
        public ViewResult ViewRecipe(int id)
        {

            Dishes ReturnedDish = dbContext.Dishes.FirstOrDefault(dish => dish.id == id);  //why can't you use "where(....)"?
            ViewBag.AllRecipes = ReturnedDish;
            ViewBag.ID = id; 
            return View("ViewRecipe");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ViewResult ShowRecipe(int id,Dishes editdish)
        {
            Dishes RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.id == id);
            ViewBag.OneDish = RetrievedDish;

            return View("EditRecipe");
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult EditRecipe(int id, Dishes editdish)
        {

            Dishes RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.id == id);
            ViewBag.OneDish = RetrievedDish;
            if(ModelState.IsValid)

            {
                RetrievedDish.Name = editdish.Name;
                RetrievedDish.Chef = editdish.Chef;
                RetrievedDish.Calories = editdish.Calories;
                RetrievedDish.Tastiness = editdish.Tastiness;
                RetrievedDish.Description = editdish.Description;
            
                dbContext.SaveChanges();
                System.Console.WriteLine("its hittingggggg");
                return RedirectToAction("Index");
            }
            else 
            {
                System.Console.WriteLine("hittting hereee!");
                return View("EditRecipe");
            }

        }




        [HttpGet("{id}/delete")]
        public IActionResult delete (int id, Dishes deletedish)
        {
            Dishes RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.id == id);
            dbContext.Dishes.Remove(RetrievedDish);
            dbContext.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
