using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L12.Models;

namespace L12.Controllers
{
   public class ChartController : Controller
   {
      public IActionResult Bar()
      {
         PrepareData(1);
         ViewData["Chart"] = "bar";
         ViewData["Title"] = "Fitness Summary";
         ViewData["ShowLegend"] = "false";
         return View("Chart");
      }

      public IActionResult Pie()
      {
         PrepareData(0);
         ViewData["Chart"] = "pie";
         ViewData["Title"] = "Shooting Summary";
         ViewData["ShowLegend"] = "true";
         return View("Chart");
      }

      public IActionResult Line()
      {
         PrepareData(2);
         ViewData["Chart"] = "line";
         ViewData["Title"] = "Exam Summary";
         ViewData["ShowLegend"] = "false";
         return View("Chart");
      }

      private void PrepareData(int x)
      {
         int[] dataFitness  = new int[] { 0, 0, 0, 0, 0 };
         int[] dataShooting = new int[] { 0, 0, 0, 0, 0 };
         int[] dataExam     = new int[] { 0, 0, 0, 0, 0 };
         List<Cadet> list = DBUtl.GetList<Cadet>("SELECT * FROM Cadet");
         foreach (Cadet cdt in list)
         {
            dataFitness[CalcGrade(cdt.Fitness)]++;
            dataShooting[CalcGrade(cdt.Shooting)]++;
            dataExam[CalcGrade(cdt.Exam)]++;
         }

         string[] colors = new[] { "cyan", "lightgreen", "yellow", "pink", "lightgrey" };
         string[] grades = new[] { "A", "B", "C", "D", "F" };
         ViewData["Legend"] = "Cadets";
         ViewData["Colors"] = colors;
         ViewData["Labels"] = grades;
         if (x == 0)
            ViewData["Data"] = dataShooting;
         else if (x == 1)
            ViewData["Data"] = dataFitness;
         else
            ViewData["Data"] = dataExam;
      }

      private int CalcGrade(int score)
      {
         if (score >= 80) return 0;
         else if (score >= 70) return 1;
         else if (score >= 60) return 2;
         else if (score >= 50) return 3;
         else return 4;
      }

   }
}