using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FYP.Models;

namespace FYP.Controllers
{
   public class ScoreController : Controller
   {
      [AllowAnonymous]
      public IActionResult About()
      {
         return View();
      }
      [Authorize(Roles = "manager, member")]
      public IActionResult ListTestScore()
      {

         List<Score> score = DBUtl.GetList<Score>(
               @"SELECT * FROM StudentScore");
         return View(score);

      }

      [HttpPost]
      public IActionResult AddScore(Score newscore)
      {
         if (!ModelState.IsValid)
         {
            ViewData["Scores"] = GetListScores();
            ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "warning";
            return View("AddScores");
         }
         else
         {
            string insert =
               @"INSERT INTO StudentScore(ID, StudentName, TestScore) 
                 VALUES('{0}', '{1}', {2})";
            int result = DBUtl.ExecSQL(insert, GetListScores().Count, newscore.StudentName, newscore.TestScore);

            if (result == 1)
            {
               TempData["Message"] = "Student Test Score successfully recorded";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
            return RedirectToAction("ListTestScore");
         }
      }

      [HttpGet]
      [Authorize(Roles = "manager")]
      public IActionResult EditScore(string id)
      {
          
         // Get the record from the database using the id
         string scoreSql = @"SELECT ID, StudentName, TestScore
                               FROM StudentScore
                              WHERE StudentScore.Id = '{0}'";
         List<Score> lstScore = DBUtl.GetList<Score>(scoreSql, id);

         // If the record is found, pass the model to the View
         if (lstScore.Count == 1)
         {
            ViewData["Genres"] = GetListScores();
            return View(lstScore[0]);
         }
         else
         // Otherwise redirect to the movie list page
         {
            TempData["Message"] = "Test Score not found.";
            TempData["MsgType"] = "warning";
            return RedirectToAction("ListTestScore");
         }
      }

      [HttpPost]
      [Authorize(Roles = "manager")]
      public IActionResult EditScore(Score score)
      {
         // TODO: L13 Task 09 - Update database table 
         // Check the state of the model ((Ref Week 9). 
          if (!ModelState.IsValid)
            {
                ViewData["Genres"] = GetListScores();
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("EditTestScore");
            }
            // Write the SQL statement
            string update = @"UPDATE StudentScore SET StudentName='{1}', TestScore={2}
                                WHERE Id={0}";
            // Execute the SQL statement in a secure manner
            int result = DBUtl.ExecSQL(update, score.StudentName,score.TestScore);
            // Check the result and branch
            // If successful set a TempData success Message and MsgType
            if (result == 1)
            {
                TempData["Message"] = "Test Score Updated";
                TempData["MsgType"] = "success";
            }
            // If unsuccessful, set a TempData message that equals the DBUtl error message
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["Msgtype"] = "danger";
            }
            // Call the action ListMovies to show the result of the update
            return RedirectToAction("ListTestScore");
      }

      [Authorize(Roles = "manager")]
      public IActionResult DeleteScore(int id)
      {
         string select = @"SELECT * FROM StudentScore
                              WHERE Id={0}";
         DataTable ds = DBUtl.GetTable(select, id);
         if (ds.Rows.Count != 1)
         {
            TempData["Message"] = "Student Test Score record no longer exists.";
            TempData["MsgType"] = "warning";
         }
         else
         {
            string delete = "DELETE FROM StudentScore WHERE Id={0}";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
               TempData["Message"] = "Student Test Score record has been deleted";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
         }
         return RedirectToAction("ListTestScore");
      }

      [Authorize(Roles = "manager")]
      private List<Score> GetListScores()
      {
         // Get a list of all genres from the database
         string scoreSql = @"SELECT * FROM StudentScore";
         List<Score> lstScore = DBUtl.GetList<Score>(scoreSql);
         return lstScore;
      }
   }
}
//20031509 Thng Yu Yang