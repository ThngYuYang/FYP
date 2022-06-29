using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L02.Controllers
{
    public class ParentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Volunteer()
        {
            return View(); //Calls Volunteer.cshtml
        }


        public IActionResult Submit()
        {
            // Use IFormCollection for shorter coding
            IFormCollection form = HttpContext.Request.Form;

            // Read the RadioButtons 
            string title = form["Title"].ToString();

            // Read the TextFields name, postal and mobile
            string name = form["Name"].ToString().Trim();
            string postal = form["Postal"].ToString().Trim();
            string mobile = form["Mobile"].ToString().Trim();
            // ....

            // Read the drop-down list
            string activity = form["Activity"].ToString();

            // Read each of the CheckBoxes 
            string day1 = form["Mon"].ToString();
            string day2 = form["Wed"].ToString();
            string day3 = form["Fri"].ToString();

            // Determine Number of Days ticked
            int days = 0;
            string daysSelected = "";

            if (day1.Equals("Mon"))
            {
              days += 1;
              daysSelected += "Monday, ";
            }
            if (day2.Equals("Wed"))
            {
                days +=1;
                daysSelected += "Wednesday, ";
            }
            if (day3.Equals("Fri"))
            {
                days += 1;
                daysSelected += "Friday, ";
            }
            if(!daysSelected.Equals(""))
            {
                daysSelected = daysSelected.Substring(0, daysSelected.Length - 2);
            }
            // Passing Data to the View
            ViewData["volunteer"] = title + " " + name;
            ViewData["activity"] = activity;
            ViewData["day"] = daysSelected;    
            ViewData["points"] = CalcCreditPoint(activity, days); 

            return View(); // Calls Submit.cshtml

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
