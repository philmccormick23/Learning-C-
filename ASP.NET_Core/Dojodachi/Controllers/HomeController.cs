using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        public Random chance = new Random();

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {   
            if(HttpContext.Session.GetString("status") == null){   ///default characteristics of the dojodachi

                HttpContext.Session.SetString("status", "They are fine!"); //first value is the key, second is the value 
                HttpContext.Session.SetString("alive", "alive");
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("energy", 50);
                HttpContext.Session.SetInt32("meals", 3);
                HttpContext.Session.SetString("message", "Do something with your Dojodachi!");
            }

            int? fullness = HttpContext.Session.GetInt32("fullness");  //defined for the if checks 
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals");
            string message = HttpContext.Session.GetString("message");
            string status = HttpContext.Session.GetString("status");
            string alive = HttpContext.Session.GetString("alive");

            ViewBag.fullness = (int) fullness;  //ability to view these variables in the Views templates
            ViewBag.happiness = (int) happiness;
            ViewBag.energy = (int) energy;
            ViewBag.meals = (int) meals;
            ViewBag.message = message;
            ViewBag.status = status;
            ViewBag.alive = alive;

            if(fullness ==100 && happiness == 100 && energy == 100)
            {
                HttpContext.Session.SetString("message", "Congratulations! You Won!");
            }
            if(fullness == 0 && happiness == 0 && energy == 0)
            {
                HttpContext.Session.SetString("message", "Your Dojodachi has passed away..");
            }
            if(fullness == 0)
            {
                HttpContext.Session.SetString("message", "Oh no! Your Dachi has lost the will to live!");
            }
            
            return View("Index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {   
            int? meals = HttpContext.Session.GetInt32("meals");
            int? fullness = HttpContext.Session.GetInt32("fullness") + chance.Next(5,11);

            if(meals > 0)
            {
                meals -= 1;
                HttpContext.Session.SetInt32("meals", (int) meals);
                HttpContext.Session.SetInt32("fullness", (int) fullness);
                HttpContext.Session.SetString("message", "You fed your pet");
            }
            if(meals == 0)
            {
                HttpContext.Session.SetString("message", "You have no more meals!");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? happiness = HttpContext.Session.GetInt32("happiness") + chance.Next(5,10);
        if(energy > 0)
        {  
            energy -=5;
            HttpContext.Session.SetInt32("energy", (int) energy);
            HttpContext.Session.SetInt32("happiness", (int) happiness);
            HttpContext.Session.SetString("message", "You played with your dog");
        
        }
        if (energy == 0)
        {
            HttpContext.Session.SetString("message", "You need a cup of coffee");
        }
        if (happiness == 0)
        {
            HttpContext.Session.SetString("message", "You are a sad PANDA :( ");
        }
        return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals") + chance.Next(1,3);
            HttpContext.Session.SetString("message", "You put your dog to work");
        
        if (energy > 0)
        {
            energy -=5;
            HttpContext.Session.SetInt32("energy", (int) energy);
            HttpContext.Session.SetInt32("meals", (int) meals);
        }
        return RedirectToAction("Index");
        }



        [HttpGet]
        [Route("sleep")]

        public IActionResult Sleep()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? fullness = HttpContext.Session.GetInt32("fullness");
            HttpContext.Session.SetString("message", "You went to sleep");
        
        if (energy > 0)
        {
            energy +=15;
            HttpContext.Session.SetInt32("energy", (int) energy);
        }
        if (happiness == 0)
        {
            happiness -=5;
            HttpContext.Session.SetInt32("happiness", (int) happiness);
        }
        if (fullness > 0)
        {
            fullness -=5;
            HttpContext.Session.SetInt32("fullness", (int) fullness);
        }
        return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("clear")]
        public IActionResult Clear()
        {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
        }
    }
}
