using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FYP.Models;

namespace FYP.Controllers
{
   public class ChartController : Controller
   {

      [Authorize(Roles = "manager")]
      public IActionResult Amount()
      {
         PrepareData(1); // Change Zero to the correct number
         ViewData["Chart"] = "bar";
         ViewData["Title"] = "Amount";
         ViewData["ShowLegend"] = "false";
         return View("Summary");
      }

      private void PrepareData(int x)
      {
         int[] testscore = new int[] { 0, 0, 0, 0, 0 };
         List<Score> list = DBUtl.GetList<Score>("SELECT * FROM Score");
         foreach (Score score in list)
         {
            if (score.TestScore > 75) testscore[0]++;
            else if (score.TestScore > 60) testscore[1]++;
            else if (score.TestScore > 50) testscore[2]++;
            else if (score.TestScore > 45) testscore[3]++;
            else testscore[4]++;
         }

         if (x == 1)
         {
            ViewData["Legend"] = "Test Scores by Amount";
            ViewData["Colors"] = new[] { "violet", "green", "blue", "orange", "red" };
            ViewData["Labels"] = new[] { "A", "B", "C", "D", "F" };
            ViewData["Data"] = testscore;
         }
         else
         {
            ViewData["Legend"] = "Nothing";
            ViewData["Colors"] = new[] { "gray", "darkgray", "black" };
            ViewData["Labels"] = new[] { "X", "Y", "Z" };
            ViewData["Data"] = new int[] { 1, 2, 3};
         }

      }

   }

}
//20031509 Thng Yu Yang