using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L02.Models;

namespace L02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
              public IActionResult Welcome()
      {
         // Create the model to be used in the View
         Greeting greet =
            new Greeting("SOI Students",
                           "C236 Module Chair",
                           "Welcome to Web App Dev in .NET",
                           @"This is a very useful module for your FYP.  
                     Study hard and All the BEST!");

         // Put the model in ViewData 
         ViewData["Hello"] = greet;

         // Use the default Welcome.cshtml view in Views/Home folder
         return View();
      }

    }
}
//20031509 Thng Yu Yang
