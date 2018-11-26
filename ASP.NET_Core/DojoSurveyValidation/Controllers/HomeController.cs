using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoSurveyValidation.Models;
namespace DojoSurveyValidation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet ("")] //type of request   //associated route string (exclude the leading /)
        public IActionResult Index () //Now .NET looks for a file called "Template.cshtml" ********
        {
            return View ();
        }

        [HttpPost ("result")] //type of request   //associated route string (exclude the leading /)
        public IActionResult Result(FormData student){

            if (ModelState.IsValid) {

                return View(student);
            } else {

                return View("Index");
            }

        }
    }
}
