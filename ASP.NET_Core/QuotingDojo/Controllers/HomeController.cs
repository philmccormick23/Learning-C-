using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(UserQuotes data)
        {   
            if(!ModelState.IsValid) {
                return View("Index");
            }
            else {
                string query = $"INSERT INTO userquotes (name, quote, created_at, updated_at) VALUES ('{data.Name}', '{data.Quote}', NOW(), NOW());";
                DbConnector.Execute(query);
                ViewData["Message"] = "Quote added to the database!";
                return RedirectToAction("Quotes");
            }  
        }
        
        [HttpGet("quotes")]
        public IActionResult Quotes()
        {
            ViewData["Message"] = "Your contact page.";
            List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM userquotes ORDER BY Created_at DESC");
            ViewBag.AllUsers = AllUsers;
            return View();
        }
    }
}
