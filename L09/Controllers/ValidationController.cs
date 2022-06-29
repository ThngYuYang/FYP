using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L09.Models;

namespace L09.Controllers
{
   public class ValidationController : Controller
   {
      public IActionResult Demo()
      {
         DemoData dd =
            new DemoData()
            {
               DateFieldA = DateTime.Today,
               DateFieldB = DateTime.Now,
            };
         return View(dd);
      }

      [HttpPost]
      public IActionResult Demo(DemoData dd)
      {
         if (!ModelState.IsValid)
         {
            ViewData["Message"] = "ModelState is NOT valid";
            ViewData["MsgType"] = "warning";
         }
         else
         {
            ViewData["Message"] = "ModelState is VALID";
            ViewData["MsgType"] = "info";
         }
         return View();
      }
   }
}