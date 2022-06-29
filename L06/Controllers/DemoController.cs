using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace L06.Controllers
{
   public class DemoController : Controller
   {
      #region "File Upload"

      private IWebHostEnvironment _env;
      public DemoController(IWebHostEnvironment environment)
      {
         _env = environment;
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

      public IActionResult UploadFile()
      {
         return View();
      }

      public IActionResult UploadFilePost(IFormFile picture)
      {
         if (picture == null)
         {
            ViewData["Message"] = "Please select Picture to upload";
            ViewData["MsgType"] = "warning";
            return View("UploadFile");
         }

         string fname = DoPhotoUpload(picture);
         ViewData["Picture"] = fname;
         ViewData["Message"] = "Picture successfully uploaded";
         ViewData["MsgType"] = "success";

         return View("UploadFile");
      }

      #endregion

      #region "Hidden and Readonly Input Text"

      public IActionResult TextFields()
      {
         return View();
      }

      public String TextFieldsPost()
      {
         IFormCollection form = HttpContext.Request.Form;
         string text1 = form["text1"].ToString(); // Normal
         string text2 = form["text2"].ToString(); // Hidden
         string text3 = form["text3"].ToString(); // Readonly

         string output = " Normal : {0}\n Hidden : {1}\n Readonly : {2}";
         return String.Format(output, text1, text2, text3);
      }

      #endregion




   }
}