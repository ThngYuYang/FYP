using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L09.Models;

namespace L09.Controllers
{
   public class InjectionController : Controller
   {
      public IActionResult Demo()
      {
         ViewData["Table"] = DBUtl.GetTable("SELECT * FROM Fruit");
         return View();
      }

      [HttpPost]
      public IActionResult Insecure(IFormCollection form)
      {
         string sql = "INSERT INTO Fruit(name) VALUES('{0}')";
         string insert = String.Format(sql, form["fruit"].ToString());
         DBUtl.ExecSQL(insert);
         ViewData["SQL"]   = DBUtl.DB_SQL;
         ViewData["Table"] = DBUtl.GetTable("SELECT * FROM Fruit");
         return View("Demo");
      }

      [HttpPost]
      public IActionResult Secure(IFormCollection form)
      {
         string insert = "INSERT INTO Fruit(name) VALUES('{0}')";
         DBUtl.ExecSQL(insert, form["fruit"].ToString());
         ViewData["SQL"]   = DBUtl.DB_SQL;
         ViewData["Table"] = DBUtl.GetTable("SELECT * FROM Fruit");
         return View("Demo");
      }

      [HttpPost]
      public IActionResult Secure2(IFormCollection form)
      {
         string sql = "INSERT INTO Fruit(name) VALUES('{0}')";
         string insert = String.Format(sql, form["fruit"].ToString().EscQuote());
         DBUtl.ExecSQL(insert);
         ViewData["SQL"]   = DBUtl.DB_SQL;
         ViewData["Table"] = DBUtl.GetTable("SELECT * FROM Fruit");
         return View("Demo");
      }

   }
}