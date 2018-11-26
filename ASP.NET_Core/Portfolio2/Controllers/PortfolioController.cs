using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Portfolio2.Controllers //be sure to use your own project's namespace!
{
    public class PortfolioController : Controller //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet ("")] //type of request   //associated route string (exclude the leading /)
        public ViewResult Portfolio () //Now .NET looks for a file called "Template.cshtml" ********
        {
            return View ();
        }

        [HttpGet ("projects")] //type of request   //associated route string (exclude the leading /)
        public ViewResult Project () //Now .NET looks for a file called "Template.cshtml" ********
        {
            return View ();
        }

        [HttpGet ("contact")] //type of request   //associated route string (exclude the leading /)
        public ViewResult Contact () //Now .NET looks for a file called "Template.cshtml" ********
        {
            return View ();
        }

        


    }
}