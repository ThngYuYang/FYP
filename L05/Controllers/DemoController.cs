using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace L05.Controllers
{
   public class DemoController : Controller
   {
      public IActionResult UseTempData()
      {
         TempData["Data1"] = "Apple";
         ViewData["Data2"] = "Orange";
         return RedirectToAction("ShowTempData");
      }

      public IActionResult ShowTempData()
      {
         ViewData["Data3"] = "Banana";
         return View();
      }

      public IActionResult NavBar()
      {
         return View();
      }

      public IActionResult Alerts()
      {
         return View();
      }

      public IActionResult England()
      {
         return View();
      }

      public IActionResult Finland()
      {
         return View("England");
      }

      public IActionResult Holland()
      {
         return RedirectToAction("England");
      }




   }
}