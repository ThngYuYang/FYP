using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace L10.Controllers
{
   public class DemoController : Controller
   {
      public IActionResult SendEmail()
      {
         return View();
      }

      [HttpPost]
      public IActionResult SendEmail(IFormCollection form)
      {
         // Old Method of reading values from Forms
         string custname = form["CustName"].ToString().Trim();
         string email    = form["Email"].ToString().Trim();
         string subject  = form["Subject"].ToString().Trim();
         string message  = form["Message"].ToString().Trim();

         string template = @"Hi {0},
                               <p>{1}</p>
                               <p>Your redemption code for free gift is <b>{2}</b>.</p>
                             Marketing Manager";
         string giftcode = Guid.NewGuid().ToString().Substring(0, 12);
         string body = String.Format(template, custname, message, giftcode);
         string result;
         if (EmailUtl.SendEmail(email, subject, body, out result))
         {
            ViewData["Message"] = "Email Successfully Sent";
            ViewData["MsgType"] = "success";
         }
         else
         {
            ViewData["Message"] = result;
            ViewData["MsgType"] = "warning";
         }

         return View();
      }

      public IActionResult DatePicker()
      {
         return View();
      }

      [HttpPost]
      public IActionResult DatePicker(IFormCollection form)
      {
         string dateA     = form["DateA"].ToString().Trim();
         string dateTimeB = form["DateTimeB"].ToString().Trim();
         string dateTimeC = form["DateTimeC"].ToString().Trim();
         ViewData["Message"] = $"[{dateA}] [{dateTimeB}] [{dateTimeC}]";
         ViewData["MsgType"] = "info";
         return View();
      }

   }
}