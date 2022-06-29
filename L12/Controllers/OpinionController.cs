using Microsoft.AspNetCore.Mvc;
using L12.Models;
using System;
using System.Collections.Generic;

namespace L12.Controllers
{
   public class OpinionController : Controller
   {

      // TODO: L12 Task 1 - Fill in the port number of the localhost
      const string DOMAIN = "localhost:15585";

      //const string DOMAIN = "http://c236dotnet.azurewebsites.net";

      public IActionResult List()
      {
         List<Poll> list = DBUtl.GetList<Poll>("SELECT * FROM Poll");
         return View(list);
      }

      [HttpGet]
      public IActionResult Create()
      {
         return View();
      }

      [HttpPost]
      public IActionResult Create(Poll poll)
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
               @"INSERT INTO Poll(PollGUID, Question, ChoiceA, ChoiceB) 
                 VALUES('{0}', '{1}', '{2}', '{3}')";

            poll.PollGUID = Guid.NewGuid().ToString();
            int res = DBUtl.ExecSQL(insert, poll.PollGUID, poll.Question,
                                            poll.ChoiceA, poll.ChoiceB);
            if (res == 1)
            {
               return Redirect($"/Opinion/Survey/{poll.PollGUID}");
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
               return RedirectToAction("List");
            }
         }
      }

      public IActionResult Delete(string id)
      {
         string delete = "Delete FROM Poll WHERE PollGUID='{0}'";
         int res = DBUtl.ExecSQL(delete, id);
         if (res == 1)
         {
            TempData["Message"] = "Survey Deleted";
            TempData["MsgType"] = "success";
         }
         else
         {
            TempData["Message"] = "Survey not found";
            TempData["MsgType"] = "warning";
         }
         return RedirectToAction("List");
      }

      public IActionResult Survey(string id)
      {
         string select = "SELECT * FROM Poll WHERE PollGUID='{0}'";
         List<Poll> list = DBUtl.GetList<Poll>(select, id);
         if (list.Count == 1)
         {
            ViewData["Url"] = $"http://{DOMAIN}/Opinion/Vote/{id}";
            return View(list[0]);
         }
         else
         {
            TempData["Message"] = "Survey not found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("List");
         }
      }

      public IActionResult Vote(string id, string choice)
      {
         string update = "";
         if (choice.Equals("A"))
         {
            update = "UPDATE Poll SET CountA = CountA + 1 WHERE PollGUID='{0}'";
         }
         else if (choice.Equals("B"))
         {
            update = "UPDATE Poll SET CountB = CountB + 1 WHERE PollGUID='{0}'";
         }
         else
         {
            TempData["Message"] = "Invalid Vote";
            TempData["MsgType"] = "warning";
            return RedirectToAction("List");
         }

         int res = DBUtl.ExecSQL(update, id, choice);
         if (res == 1)
         {
            string qn = id;
            return Redirect($"/Opinion/Outcome/{id}");
         }
         else
         {
            TempData["Message"] = "Update not successful";
            TempData["MsgType"] = "warning";
            return RedirectToAction("List");
         }
      }

      public IActionResult Outcome(string id)
      {
         string select = "SELECT * FROM Poll WHERE PollGUID='{0}'";
         List<Poll> list = DBUtl.GetList<Poll>(select, id);
         if (list.Count == 1)
         {
            return View(list[0]);
         }
         else
         {
            TempData["Message"] = "Survey not found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("List");
         }
      }

      public IActionResult GetOutcome(string id)
      {
         string select = "SELECT * FROM Poll WHERE PollGUID='{0}'";
         List<Poll> list = DBUtl.GetList<Poll>(select, id);
         if (list.Count == 1)
         {
            return Json(new int[] { list[0].CountA, list[0].CountB });
         }
         return Json(new int[] { 0, 0 });
      }
   }
}
// 20031509 Thng Yu Yang