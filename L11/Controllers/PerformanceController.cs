using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using L11.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace L11.Controllers
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
         if (!ModelState.IsValid)
         {
            ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "warning";
            return View("Create");
         }
         else
         {
            string insert =
               @"INSERT INTO Performance(Title, Artist, PerformDT, Duration, Price, Chamber) VALUES
                   ('{0}', '{1}', '{2:yyyy-MM-dd HH:mm}', {3}, {4},	'{5}')";

            int res = DBUtl.ExecSQL(insert, perform.Title, perform.Artist, perform.PerformDT,
                                            perform.Duration, perform.Price, perform.Chamber);
            if (res == 1)
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
      public IActionResult Update(int id)
      {
            // TODO: L11 Task 3 : Complete HttpGet Update action 
            // Get the record from the database using the id
            // If the record is found, pass the model to the View
            // Otherwise, redirect to the Index page with the message "Perfromance record does not exist"
            string select = "SELECT * FROM Performance WHERE Pid={0}";
            List<Performance> list = DBUtl.GetList<Performance>(select, id);
            if (list.Count == 1)
            {
                return View(list[0]);
            }
            else
            {
                TempData["Message"] = "Performance not found";
                TempData["MsgType"] = "warning";
                return RedirectToAction("Index");
            }
      }

      [Authorize(Roles = "manager")]
      [HttpPost]
      public IActionResult Update(Performance perform)
        {
            // TODO: L11 Task 4 : Complete HttpPost Update action 
            // Check the ModelState
            // If not valid, display the message "Invalid Input" in the same View 
            // Otherwise, 
            //    Write SQL Update statement
            //    Execute the statement with model's properties
            //    Check for success
            //    If success, redirect to the Index page with "Performance Updated"
            //    Otherwise, redirect to the Index page with db error message
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("Update");
            }
            else
            {
                string update =
                    @"UPDATE Performance SET Title= '{1}', Artist = '{2}', PerformDT = '{3:yyyy-MM-dd HH:mm}', Duration = {4}, Price = '{5}', Chamber = '{6}' WHERE Pid = {0}";
                int res = DBUtl.ExecSQL(update, perform.Pid, perform.Title, perform.Artist, perform.PerformDT, perform.Duration, perform.Price, perform.Chamber);
                if (res == 1)
                {
                    TempData["Message"] = "Performance Updated";
                    TempData["MsgType"] = "sucess";
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
      public IActionResult Delete(int id)
      {
         string select = @"SELECT * FROM Performance WHERE Pid={0}";
         DataTable ds = DBUtl.GetTable(select, id);
         if (ds.Rows.Count != 1)
         {
            TempData["Message"] = "Performance does not exist";
            TempData["MsgType"] = "warning";
         }
         else
         {
            string delete = "DELETE FROM Performance WHERE Pid={0}";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
               TempData["Message"] = "Performance Deleted";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
         }
         return RedirectToAction("Index");
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