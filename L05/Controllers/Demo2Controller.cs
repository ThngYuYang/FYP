using Microsoft.AspNetCore.Mvc;

namespace L05.Controllers
{
   public class Demo2Controller : Controller
   {
      public IActionResult UseTempData()
      {
         TempData["Data1"] = "Pineapple";
         ViewData["Data2"] = "Orange";
         return RedirectToAction("ShowTempData", "Demo");
      }

   }
}