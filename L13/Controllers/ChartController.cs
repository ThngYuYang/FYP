using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using L13.Models;

namespace L13.Controllers
{
   public class ChartController : Controller
   {

      // TODO: L13 Task 01b - Use [AllowAnonymous] or [Authorise] to specify access control

      [Authorize(Roles = "manager")]
      public IActionResult Duration()
      {
         // TODO: L13 Task 11a - Prepare the Data
         PrepareData(1); // Change Zero to the correct number
         ViewData["Chart"] = "pie";
         ViewData["Title"] = "Duration";
         ViewData["ShowLegend"] = "true";
         return View("Summary");
      }
      [Authorize(Roles = "manager")]
      public IActionResult Genre()
      {
         // TODO: L13 Task 11b - Prepare the Data
         PrepareData(2); // Change Zero to the correct number
         ViewData["Chart"] = "line";
         ViewData["Title"] = "Genre";
         ViewData["ShowLegend"] = "false";
         return View("Summary");
      }

      [Authorize(Roles = "manager")]
      public IActionResult Price()
      {
         // TODO: L13 Task 11c - Prepare the Data
         PrepareData(3); // Change Zero to the correct number
         ViewData["Chart"] = "bar";
         ViewData["Title"] = "Price";
         ViewData["ShowLegend"] = "false";
         return View("Summary");
      }

      private void PrepareData(int x)
      {
         int[] duration = new int[] { 0, 0, 0, 0, 0 };
         int[] genre = new int[] { 0, 0, 0, 0 };
         int[] price = new int[] { 0, 0, 0 };
         List<Movie> list = DBUtl.GetList<Movie>("SELECT * FROM Movie");
         foreach (Movie movie in list)
         {
            if (movie.Duration < 80) duration[0]++;
            else if (movie.Duration < 95) duration[1]++;
            else if (movie.Duration < 110) duration[2]++;
            else if (movie.Duration < 125) duration[3]++;
            else duration[4]++;

            if (movie.GenreId == 1) genre[0]++;
            else if (movie.GenreId == 2) genre[1]++;
            else if (movie.GenreId == 3) genre[2]++;
            else genre[3]++;

            if (movie.Price < 10) price[0]++;
            else if (movie.Price < 15) price[1]++;
            else price[2]++;
         }

         if (x == 1)
         {
            ViewData["Legend"] = "Movies by Duration";
            ViewData["Colors"] = new[] { "violet", "green", "blue", "orange", "red" };
            ViewData["Labels"] = new[] { "Very Short", "Short", "Normal", "Long", "Very Long" };
            ViewData["Data"] = duration;
         }
         else if (x == 2)
         {
            ViewData["Legend"] = "Movies by Genre";
            ViewData["Colors"] = new[] { "violet", "orange", "blue", "red" };
            ViewData["Labels"] = new[] { "All/Unknown", "Action", "Comedy", "Drama" };
            ViewData["Data"] = genre;
         }
         else if (x == 3)
         {
            ViewData["Legend"] = "Movies by Price";
            ViewData["Colors"] = new[] { "green", "blue", "red" };
            ViewData["Labels"] = new[] { "Discounted", "Normal", "Premium" };
            ViewData["Data"] = price;
         }
         else
         {
            ViewData["Legend"] = "Nothing";
            ViewData["Colors"] = new[] { "gray", "darkgray", "black" };
            ViewData["Labels"] = new[] { "X", "Y", "Z" };
            ViewData["Data"] = new int[] { 1, 2, 3};
         }

      }

   }

}
//20031509 Thng Yu Yang