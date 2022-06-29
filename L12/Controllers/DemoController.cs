using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L12.Models;
using System.Collections.Generic;

namespace L12.Controllers
{
   public class DemoController : Controller
   {
      public IActionResult QueryString()
      {
         IQueryCollection query = HttpContext.Request.Query;
         ViewData["q1"] = query["keyA"]; 
         ViewData["q2"] = query["keyB"];
         ViewData["q3"] = query["keyC"];
         return View();
      }

      public IActionResult QueryString2(string keyA, string keyB, string keyC)
      {
         ViewData["q1"] = keyA;
         ViewData["q2"] = keyB;
         ViewData["q3"] = keyC;
         return View("QueryString");
      }

      public IActionResult CustomTag()
      {
         return View();
      }

      public IActionResult QRCode()
      {
         return View();
      }

     


   }
}