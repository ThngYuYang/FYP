using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L02.Controllers
{
   public class OrganicController : Controller
   {
      public IActionResult Index()
      {
         return View(); // Views/Organic/Index.cshtml
      }

      public IActionResult Subscription()
      {
         return View(); // Views/Organic/Subscription.cshtml
      }

      public IActionResult Confirmation()
      {
         // Use IFormCollection for shorter coding
         IFormCollection form = HttpContext.Request.Form;

         // Remove leading/trailing spaces for TextFields
         string name     = form["Name"].ToString().Trim();
         string email    = form["Email"].ToString().Trim();
         string refer    = form["Refer"].ToString().Trim();
         string comments = form["Comments"].ToString().Trim();

         // ToString() for RadioButtons - change null to empty 
         string gender = form["Gender"].ToString();

         // ToString() for CheckBoxes - change null to empty 
         string recipe = form["Recipe"].ToString();
         string news   = form["News"].ToString();
         string offers = form["Offers"].ToString();

         // Get CheckBoxes for Interest
         string interest = "";
         if (recipe.Equals("Recipe"))
            interest += "Recipe, ";
         if (news.Equals("News"))
            interest += "News, ";
         if (offers.Equals("Offers"))
            interest += "Offers, ";
         if (!interest.Equals(""))
            interest = interest.Substring(0, interest.Length - 2);

         // Display View
         ViewData["name"] = name;
         ViewData["email"] = email;
         ViewData["gender"] = gender;
         ViewData["interest"] = interest ;
         ViewData["refer"] = refer;
         ViewData["comments"] = comments;

         return View(); // Views/Organic/Confirmation.cshtml
      }
   }
}
//20031509 Thng Yu Yang
