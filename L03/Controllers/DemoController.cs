using Microsoft.AspNetCore.Mvc;
using L03.Models;
using System;

namespace L03.Controllers
{
   public class DemoController : Controller
   {
      public IActionResult Number(int id)
      {
         return View("Number" + id);
      }

      public IActionResult Index()
      {
         return View(); // Which cshtml?
      }

      public IActionResult StringExtMethods()
      {
         return View();
      }

      public IActionResult Stretch()
      {
         string line = HttpContext.Request.Form["Line"];
         string result = "NOTHING!";
         // TODO: L03 TASK 2 Use the Extension Method
         result = line.Stretch();
         ViewData["Result"] = result;
         return View("StringExtMethods");
      }

      public IActionResult UpperLower()
      {
         string line = HttpContext.Request.Form["Line"];
         string result = line.UpperLower();
         ViewData["Result"] = result;
         return View("StringExtMethods");
      }

      public IActionResult ShowTriangle()
      {
         Triangle shape = new Triangle();

         Random rnd = new Random();
         shape.Side1 = rnd.Next(1, 5);
         shape.Side2 = rnd.Next(1, 5);
         shape.Side3 = rnd.Next(1, 5);

         return View("TriView", shape);
      }

      public IActionResult ShowRectangle()
      {
         Rectangle square = new Rectangle();
         square.Side1 = 3;
         square.Side2 = 3;
         return View("RectView", square);
      }

   }
}
//20031509 Thng Yu Yang