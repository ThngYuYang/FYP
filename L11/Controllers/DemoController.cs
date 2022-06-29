using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L11.Models;
using System;
using System.Collections.Generic;

namespace L11.Controllers
{
   public class DemoController : Controller
   {
      public IActionResult ListCadets()
      {
         List<Cadet> list = DBUtl.GetList<Cadet>("SELECT * FROM Cadet");
         return View(list);
      }

      [HttpGet]
      public IActionResult EditCadet(string id)
      {
         string select = "SELECT * FROM Cadet WHERE CNo='{0}'";
         List<Cadet> list = DBUtl.GetList<Cadet>(select, id);
         if (list.Count == 1)
         {
            return View(list[0]);
         }
         else
         {
            TempData["Message"] = "Cadet not found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("ListCadets");
         }
      }

      [HttpPost]
      public IActionResult EditCadet(Cadet cdt)
      {
         if (!ModelState.IsValid)
         {
            ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "warning";
            return View("EditCadet");
         }
         else
         {
            string update =
               @"UPDATE Cadet
                    SET CName='{1}', ClassGrp='{2}', 
                        Shooting={3}, Fitness={4}, Exam={5}
                  WHERE CNo='{0}'";
            int res = DBUtl.ExecSQL(update, cdt.CNo, cdt.CName, cdt.ClassGrp,
                                            cdt.Shooting, cdt.Fitness, cdt.Exam);
            if (res == 1)
            {
               TempData["Message"] = "Cadet Updated";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
            return RedirectToAction("ListCadets");
         }
      }

      public IActionResult Employees()
      {
         return View();
      }
   }
}