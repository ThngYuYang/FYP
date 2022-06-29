using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L05.Models;
using System;
using System.Data;

namespace L05.Controllers
{
   public class OrganicController : Controller
   {
      public IActionResult Products()
      {
         string sql =
            @"SELECT OrgCode AS CODE, 
                     OrgDesc AS DESCRIPTION, 
                     Price AS PRICE, 
                     Gram AS GRAMS, 
                     Country AS COUNTRY
                FROM OrgProduct";

         DataTable dt = DBUtl.GetTable(sql);

         return View(dt);
      }

      public IActionResult Index()
      {
         return View("Organic");
      }

      public IActionResult SubscriptionList()
      {
         string sql =
         @"SELECT *
                FROM OrgSubscription";

         DataTable dt = DBUtl.GetTable(sql);

         return View(dt);
      }

      public IActionResult Subscription()
      {
         return View("Subscription");
      }

      public IActionResult Confirmation()
      {
         IFormCollection form = HttpContext.Request.Form;

         string name = form["Name"].ToString().Trim();
         string email = form["Email"].ToString().Trim();
         string refer = form["Refer"].ToString().Trim();
         string comments = form["Comments"].ToString().Trim();
         string gender = form["Gender"].ToString();

         // Read CheckBoxes  
         string recipe = form["Recipe"].ToString();
         string news = form["News"].ToString();
         string offers = form["Offers"].ToString();

         // Validate User Enters and Selects all Fields
         if (ValidUtl.CheckIfEmpty(name, email, refer, comments, gender))
         {
            ViewData["Message"] = "Please enter or select all fields";
            return View("Subscription");
         }

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

         // Add Record to Database
         string sql = @"INSERT INTO OrgSubscription
                          (UserName, SubEmail, Gender, Recipes, 
                           News, Offers, Referral, Comments)
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";

         string insert = String.Format(sql, name, email, 
                                            gender.Substring(0, 1),  // F or M
                                            recipe.Equals("Recipe"), // true or false
                                            news.Equals("News"),
                                            offers.Equals("Offers"),
                                            refer, comments);

         int rowsAffected = DBUtl.ExecSQL(insert);

         // Check Insert is Successful
         if (rowsAffected == 1)
         {
            Subscription s = new Subscription()
               {
                  UserName = name,
                  SubEmail = email,
                  Gender   = gender,
                  Interest = interest,
                  Referral = refer,
                  Comments = comments
               };
            return View("Confirmation", s);
         }
         else
         {
            ViewData["Message"] = DBUtl.DB_Message;
            return View("Subscription");
         }
      }

   }
}
