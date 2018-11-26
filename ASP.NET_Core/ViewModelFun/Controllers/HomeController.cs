using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ViewModelFun.Models;

namespace ViewModelFun.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Messages";
            Message msg = new Message()
            {
                content = "This is a MESSAGE......................."
            };
            return View(msg);
        }
        [Route("numbers")]
        public IActionResult Numbers()
        {
            ViewData["Title"] = "Here are some numbers!";
            Numbers num = new Numbers()
            {
                numList = new List<int> {1,2,3,10,42,5}
            };
            return View(num);

        }
        [Route("users")]
        public IActionResult Users()
        {
            ViewData["Title"] = "Here are some Users!";
            Users users = new Users()
            {
                userList = new List<string> {"Kyle Hawkins", "Danica Tomber", "Ragnar Lofbrook", "Julius Ceasar", "Alexander of Macedonia"}
            };
            return View(users);
        }
        [Route("user")]
        public IActionResult OneUser()
        {
            ViewData["Title"] = "Here is a User!";
            User user = new User()
            {
                FirstName = "Kyle",
                LastName = "Kyle Hawkins"
            };
            return View(user);
        }
    }
}
