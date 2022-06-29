using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L02.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult SayHello1()
        {
            DoGreeting();
            return View(); 
        }

        private void DoGreeting()
        {
            DateTime current = DateTime.Now;
            string greet = String.Format("It's {0:HHmm}hrs. ", current);
            if (current.Hour < 12)
                greet += "Good Morning";
            else if (current.Hour < 18)
                greet += "Good Afternoon";
            else
                greet += "Good Evening";
            ViewData["Greeting"] = greet;
        }
        public IActionResult SayHello5()
        {
            DoGreeting();
            return View(); // Views/Demo/SayHello5.cshtml
        }

        public IActionResult SayHello5_Post()
        {
            DoGreeting();
            string name = HttpContext.Request.Form["Name"];
            string salute = HttpContext.Request.Form["Gender"].ToString();
            string memship = HttpContext.Request.Form["Membership"];
            string smoke = HttpContext.Request.Form["Smoke"].ToString();

            ViewData["Message"] = $"Hello {salute} {name} ({memship}), Wecome!";

            // Check whether checkbox is ticked or not. 
            // If "smoke" variable matches the value attribute in the <form>, 
            // then it is ticked.
            if (String.Equals(smoke, "Smoking"))
                ViewData["Message"] += " Please smoke at the designated places.";

            return View("SayHello5"); // Views/Demo/SayHello5.cshtml
        }

    }
}
//20031509 Thng Yu Yang
