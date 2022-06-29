using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L06.Models;
using System;
using System.Data;
using System.IO;

namespace L06.Controllers
{
   public class TravelController : Controller
   {
      private IWebHostEnvironment _env;

      public TravelController(IWebHostEnvironment environment)
      {
         _env = environment;
      }

      public IActionResult Index()
      {
         DataTable dt = DBUtl.GetTable("SELECT * FROM Travel");
         return View("Main", dt.Rows);
      }

      public IActionResult Add()
      {
         return View();
      }

      public IActionResult AddPost(IFormFile photo)
      {
         // TODO: L06 Task 3a - Get Values Posted from the View
         IFormCollection form = HttpContext.Request.Form;
         string title    = form["Title"].ToString().Trim();
         string city     = form["City"].ToString().Trim();
         string story    = form["Story"].ToString().Trim();
         string tripDate = form["TripDate"].ToString().Trim();
         string duration = form["Duration"].ToString().Trim();
         string spending = form["Spending"].ToString().Trim();

         string message = "";
         if (ValidUtl.CheckIfEmpty(title, city, story))
         {
            message = "Please enter Title, City and Story <br/>";
         }

         if (!tripDate.IsDate("yyyy-MM-dd"))
         {
            message += "Empty or Invalid Trip Date <br/>";
         }

         if (!duration.IsInteger() || !spending.IsNumeric())
         {
            message += "Invalid Duration or Spending <br/>";
         }

         if (photo == null)
         {
            message += "Please upload a photo";
         }

         if (!message.Equals(""))
         {
            ViewData["Message"] = message;
            ViewData["MsgType"] = "warning";
            return View("Add");
         }

            // TODO: L06 Task 3b - Call Method to Upload Photo
            string picfilename = DoPhotoUpload(photo);


            // TODO: L06 Task 3c - Insert Record into Travel Table
            string sql = @"INSERT INTO Travel(Title, City, TripDate, Duration, Spending, Story, Picture)
                        VALUES('{0}', '{1}', '{2}', {3}, {4}, '{5}', '{6}')";
            string insert = String.Format(sql, title.EscQuote(), city.EscQuote(), tripDate, duration,spending, story.EscQuote(), picfilename.EscQuote());

            if (DBUtl.ExecSQL(insert) == 1)
            {
                TempData["Message"] = "Trip Sucessfully Added";
                TempData["MsgType"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
                return View("Add");
            }
            
      }

      public IActionResult Edit(string id)
      {
         if (id == null)
            return RedirectToAction("Index");

         string sql = String.Format("SELECT * FROM Travel WHERE id = {0}", id);
         DataTable ds = DBUtl.GetTable(sql);
         if (ds.Rows.Count == 1)
         {
            Trip trip = new Trip()
            {
               ID        = Int32.Parse(id),
               Title     = ds.Rows[0]["Title"].ToString(),
               City      = ds.Rows[0]["City"].ToString(),
               Story     = ds.Rows[0]["Story"].ToString(),
               TripDate  = (DateTime)ds.Rows[0]["TripDate"],
               Duration  = (int)ds.Rows[0]["Duration"],
               Spending  = (double)ds.Rows[0]["Spending"],
               PhotoFile = ds.Rows[0]["picture"].ToString()
            };
            return View(trip);
         }
         else
         {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Index");
         }
      }

      public IActionResult EditPost()
      {
         // TODO: L06 Task 5a - Get Values Posted from the View
         IFormCollection form = HttpContext.Request.Form;
         string id    = form["ID"].ToString().Trim();    // Hidden Field
         string story = form["Story"].ToString().Trim();

         // TODO: L06 Task 5b - Update Record in Travel Table
         string sql = "UPDATE Travel SET Story='{1}' WHERE Id={0}";
         string update = String.Format(sql, id, story);
         if (DBUtl.ExecSQL(update) == 1)
         {
            TempData["Message"] = "Trip Sucessfully Updated";
            TempData["MsgType"] = "success";
         }
         else
         {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
         }
         return RedirectToAction("Index");
      }

      public IActionResult Delete(string id)
      {
         if (id == null)
         {
            return RedirectToAction("Index");
         }

         string sql = String.Format("SELECT * FROM Travel WHERE id={0}", id);
         DataTable ds = DBUtl.GetTable(sql);
         if (ds.Rows.Count != 1)
         {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
         }
         else
         {
            string photoFile = ds.Rows[0]["picture"].ToString();
            string fullpath = Path.Combine(_env.WebRootPath, "photos/" + photoFile);

            // TODO: L06 Task 6a - Delete the Photo from the Web Server
            System.IO.File.Delete(fullpath);

            // TODO: L06 Task 6b - Delete Record From Travel Table
            sql = "DELETE FROM Travel WHERE id={0}";
            string delete = String.Format(sql, id);
            int res = DBUtl.ExecSQL(delete);
            if (res == 1)
            {
               TempData["Message"] = "Travel Sucessfully Deleted";
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

      private string DoPhotoUpload(IFormFile photo)
      {
         string fext = Path.GetExtension(photo.FileName);
         string uname = Guid.NewGuid().ToString();
         string fname = uname + fext;
         string fullpath = Path.Combine(_env.WebRootPath, "photos/" + fname);
         FileStream fs = new FileStream(fullpath, FileMode.Create);
         photo.CopyTo(fs);
		 fs.Close();
         return fname;
      }

   }
}
// 20031509 Thng Yu Yang