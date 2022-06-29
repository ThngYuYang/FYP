using Microsoft.AspNetCore.Mvc;
using L04.Models;
using System.Collections.Generic;
using System.Data;

namespace L04.Controllers
{
   public class DemoController : Controller
   {
      public IActionResult Country()
      {
         return View();
      }

      public IActionResult CountryBS()
      {
         return View();
      }

      public IActionResult PolytechnicBS()
      {
         return View();
      }

      public IActionResult DoSelect()
      {
         return View();
      }

      public IActionResult DoSelectPost()
      {
         string sql = HttpContext.Request.Form["sql"].ToString().Trim();

         DataTable dt = DBUtl.GetTable(sql);
         if (dt == null)
            ViewData["Message"] = DBUtl.DB_Message;

         return View("DoSelect", dt);
      }

      public IActionResult GetTable1()
      {
         ViewData["dbtable"] = DBUtl.GetTable("SELECT * FROM BwPublisher");
         return View();
      }

      public IActionResult GetTable2()
      {
         DataTable dt = DBUtl.GetTable("SELECT * FROM BwPublisher");
         return View(dt);
      }

      public IActionResult RazorLoopAndIf()
      {
         List<Candidate> candList =
            new List<Candidate> {
               new Candidate() {RegNo=101, Name="Kevin Ong",   Gender="M", Clearance=true},
               new Candidate() {RegNo=212, Name="Dewi Aisha",  Gender="F", Clearance=false},
               new Candidate() {RegNo=333, Name="Panda McKoo", Gender="M", Clearance=false},
               new Candidate() {RegNo=104, Name="Alex Kurien", Gender="M", Clearance=true},
               new Candidate() {RegNo=105, Name="Chris Koh",   Gender="M", Clearance=true},
               new Candidate() {RegNo=197, Name="Jalaludin",   Gender="M", Clearance=true}
            };
         return View(candList);
      }

     
   }

}

