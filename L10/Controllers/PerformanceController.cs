using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using L10.Models;
using Microsoft.AspNetCore.Authorization;

namespace L10.Controllers
{
   public class PerformanceController : Controller
   {
      [AllowAnonymous]
      public IActionResult About()
      {
         return View();
      }

      [Authorize(Roles = "manager, member")]
      public IActionResult Index()
      {
         DataTable dt = DBUtl.GetTable("SELECT * FROM Performance");
         return View("Index", dt.Rows);

      }

      [Authorize(Roles = "manager")]
      public IActionResult Create()
      {
         return View();
      }

      [Authorize(Roles = "manager")]
      [HttpPost]
      public IActionResult Create(Performance perform)
      {
            // TODO: L10 Task 4 - Complete the Create action to insert a performance record
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("Create");
            }
            else
            {
                string insert = @"INSERT INTO Performance(Title, Artist, PerformDT, Duration, Price, Chamber) VALUES ('{0}', '{1}', '{2:yyyy-MM-dd HH:mm}', {3}, {4}, '{5}')";

                int res = DBUtl.ExecSQL(insert, perform.Title, perform.Artist, perform.PerformDT, perform.Duration, perform.Price, perform.Chamber);

                if(res == 1)
                {
                    TempData["Message"] = "Performance Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("Index");
            }
      }

      [Authorize(Roles = "manager")]
      public IActionResult VerifyDate(DateTime performDT)
      {
         if (performDT < DateTime.Today.AddDays(14))
         {
            return Json($"Date 14 days in advance");
         }
         return Json(true);
      }



   }
}
// 20031509 Thng Yu Yang