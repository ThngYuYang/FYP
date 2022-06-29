using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L03.Controllers
{
   public class ParentController : Controller
   {
      public IActionResult Volunteer()
      {
         return View();
      }

      public IActionResult Submit()
      {
         // Use IFormCollection for shorter coding
         IFormCollection form = HttpContext.Request.Form;

         // Reading Input from TextFields
         string name = form["Name"].ToString().Trim();
         string mobile = form["Mobile"].ToString().Trim();
         string postal = form["Postal"].ToString().Trim();

         // Reading Input from Dropdown List
         string activity = form["Activity"].ToString();

         // Reading Input from RadioButtons
         string title = form["Title"].ToString();

         // Reading Input from CheckBoxes
         string mon = form["Mon"].ToString();
         string wed = form["Wed"].ToString();
         string fri = form["Fri"].ToString();

         // Validation - Mandatory Fields must be entered
         // TODO: L03 TASK 4 Validate user enters or selects all fields
         if (ValidUtl.CheckIfEmpty(name, mobile, postal, activity, title))
         {
            ViewData["Message"] = "Please enter all fields";
            return View("Volunteer");
         }

         // Validation - Mobile Phone must be eight digits
         // TODO: L03 TASK 5 Validate Mobile Phone must be eight digits
         if (!mobile.IsInteger() || mobile.Length != 8)
         {
            ViewData["Message"] = "Invalid Mobile Phone";
            return View("Volunteer");
         }

         // Determine Number of Days Checked
         int days = 0;
         string daysSelected = "";

         if (mon.Equals("Mon"))
         {
            days++;
            daysSelected += "Monday, ";
         }
         if (wed.Equals("Wed"))
         {
            days++;
            daysSelected += "Wednesday, ";
         }
         if (fri.Equals("Fri"))
         {
            days++;
            daysSelected += "Friday, ";
         }

         // Validation - At least ONE day must be checked
         if (days == 0)
         {
            ViewData["Message"] = "Please check Days";
            return View("Volunteer");
         }

         daysSelected = daysSelected.Substring(0, daysSelected.Length - 2);

         // Display Acknowledge View
         ViewData["FullName"] = title + " " + name;
         ViewData["Activity"] = activity;
         ViewData["Days"] = daysSelected;
         ViewData["Credits"] = CalcCreditPoint(activity, days);
         ViewData["HP"] = mobile;
         return View("Submit");
      }

      private int CalcCreditPoint(string activity, int days)
      {
         int credits = 0;
         if (activity.Equals("Story Telling") ||
             activity.Equals("Art and Craft"))
         {
            credits = days * 10;
         }
         else if (activity.Equals("Traffic Control"))
         {
            credits = days * 5;
         }
         else if (activity.Equals("Music Appreciation"))
         {
            credits = days * 15;
         }
         return credits;
      }

   }
}
//20031509 Thng Yu Yang