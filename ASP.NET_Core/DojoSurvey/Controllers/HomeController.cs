using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DojoSurvey.Models;
namespace DojoSurvey.Controllers //be sure to use your own project's namespace!
{
    public class HomeController : Controller //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet ("")] //type of request   //associated route string (exclude the leading /)
        public IActionResult Index () //Now .NET looks for a file called "Template.cshtml" ********
        {
            return View ();
        }

        [HttpPost ("result")] //type of request   //associated route string (exclude the leading /)
        public IActionResult Result(FormData student){
            ViewBag.name = student.Name;
            ViewBag.location = student.Location;
            ViewBag.language = student.Language;
            ViewBag.comment = student.Comment;
            System.Console.WriteLine("Submitted");

            return View();
        }
    }
}