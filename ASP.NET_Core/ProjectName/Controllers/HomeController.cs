using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ProjectName.Controllers //be sure to use your own project's namespace!
{
    public class HomeController : Controller //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet ("")] //type of request   //associated route string (exclude the leading /)
        public string Index () //returning basic data (string)
        {
            return "Hello World from HelloController!";
        }

        [HttpGet ("user/{username}/{location}")] //passing info from the URL
        public string HelloUser (string username, string location) {
            return $"Hello {username} from {location}";
        }

        [HttpGet ("template")]
        public ViewResult Template () //Now .NET looks for a file called "Template.cshtml" ********
        {

            return View ();
        }

        [HttpGet ("redirect")]
        public RedirectToActionResult Redirect () //Redirect to template
        {
            return RedirectToAction ("Template");
        }

        [HttpGet ("redirectToHelloUser")]
        public RedirectToActionResult RedirectHome () //Redirect to a template, passing additional data
        {
            return RedirectToAction ("HelloUser", new { username = "Devon", Location = "Seattle" });
        }

        [HttpGet("json/{username}/{location}")]
        public JsonResult jsonUser(string username, string location) {    //This results a JSON object to the browser
            var response = new{user=username, place=location};
            return Json (response);
        }

    }
}