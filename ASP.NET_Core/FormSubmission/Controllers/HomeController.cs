using System;
using System.Collections.Generic;
using FormSubmission.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormSubmission.Controllers {
    public class HomeController : Controller //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet ("")] //type of request   //associated route string (exclude the leading /)
        public IActionResult Index () //Now .NET looks for a file called "Template.cshtml" ********
        {
            return View ();
        }

        [HttpPost ("result")] //type of request   //associated route string (exclude the leading /)
        public IActionResult Create (User student) {
            if (ModelState.IsValid) {
                ViewBag.FirstName = student.FirstName;
                ViewBag.LastName = student.LastName;
                ViewBag.Age = student.Age;
                ViewBag.Email = student.Email;
                // do somethng!  maybe insert into db?  then we will redirect
                return View("Result");
            } else {

                return View("Index");
            }
        }
    }
}