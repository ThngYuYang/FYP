using Microsoft.AspNetCore.Mvc;

namespace L07.Controllers
{
   public class HomeController : Controller
   {
      public IActionResult Index()
      {
         return Redirect("~/start.html");
      }
   }
}