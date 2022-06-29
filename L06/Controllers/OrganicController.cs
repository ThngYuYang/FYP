using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L06.Models;
using System;
using System.Data;

namespace L06.Controllers
{
   public class OrganicController : Controller
   {
      public IActionResult Index()
      {
         return View("Organic");
      }

      public IActionResult Products()
      {
         string sql = "SELECT * FROM OrgProduct";
         DataTable dt = DBUtl.GetTable(sql);
         return View(dt.Rows);
      }

      #region "Product Add"
      public IActionResult ProductAdd()
      {
         return View();
      }

      public IActionResult ProductAddPost()
      {
         IFormCollection form = HttpContext.Request.Form;
         string orgCode    = form["pcode"].ToString().Trim();
         string orgDesc    = form["pdesc"].ToString().Trim();
         string orgPrice   = form["price"].ToString().Trim();
         string orgGram    = form["weight"].ToString().Trim();
         string orgCountry = form["country"].ToString().Trim();

         if (ValidUtl.CheckIfEmpty(orgCode, orgDesc, orgPrice, orgGram, orgCountry))
         {
            ViewData["Message"] = "Please enter all fields";
            ViewData["MsgType"] = "warning";
            return View("ProductAdd");
         }

         if (!orgCode.IsInteger())
         {
            ViewData["Message"] = "Product Code must be integer";
            ViewData["MsgType"] = "warning";
            return View("ProductAdd");
         }

         if (!orgPrice.IsNumeric())
         {
            ViewData["Message"] = "Price must be numeric";
            ViewData["MsgType"] = "warning";
            return View("ProductAdd");
         }

         if (!orgGram.IsInteger())
         {
            ViewData["Message"] = "Weight must be integer";
            ViewData["MsgType"] = "warning";
            return View("ProductAdd");
         }

         string sql = @"INSERT INTO OrgProduct(OrgCode, OrgDesc, Price, Gram, Country) 
                        VALUES({0}, '{1}', {2}, {3}, '{4}')";
         string insert = String.Format(sql, orgCode, orgDesc, orgPrice, orgGram, orgCountry);
         int res = DBUtl.ExecSQL(insert);
         if (res == 1)
         {
            TempData["Message"] = "Product Added";
            TempData["MsgType"] = "success";
         }
         else
         {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
         }
         return RedirectToAction("Products");
      }
      #endregion

      #region "Product Edit"
      public IActionResult ProductEdit(String id)
      {
         string sql = "SELECT * FROM OrgProduct WHERE OrgCode={0}";
         string select = String.Format(sql, id);
         DataTable dt = DBUtl.GetTable(select);
         if (dt.Rows.Count == 1)
         {
            OrgProduct product = new OrgProduct
            {
               OrgCode = (int)dt.Rows[0]["OrgCode"],
               Gram = (int)dt.Rows[0]["Gram"],
               Price = (double)dt.Rows[0]["Price"],
               OrgDesc = dt.Rows[0]["OrgDesc"].ToString(),
               Country = dt.Rows[0]["Country"].ToString()
            };
            return View(product);
         }
         else
         {
            TempData["Message"] = "Product Not Found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Products");
         }
      }


      public IActionResult ProductEditPost(String id)
      {
         IFormCollection form = HttpContext.Request.Form;
         string orgCode = form["pcode"].ToString().Trim();
         string orgDesc = form["pdesc"].ToString().Trim();
         string orgPrice = form["price"].ToString().Trim();
         string orgGram = form["weight"].ToString().Trim();
         string orgCountry = form["country"].ToString().Trim();

         string sql = @"UPDATE OrgProduct
                           SET OrgDesc = '{1}',
                               Price   = {2},
                               Gram    = {3},
                               Country = '{4}'
                         WHERE OrgCode = {0}";

         string update = String.Format(sql, orgCode, orgDesc, orgPrice, orgGram, orgCountry);
         int res = DBUtl.ExecSQL(update);
         if (res == 1)
         {
            TempData["Message"] = "Product Updated";
            TempData["MsgType"] = "success";
         }
         else
         {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
         }
         return RedirectToAction("Products");
      }
      #endregion

      #region "Product Delete"
      public IActionResult ProductDelete(String id)
      {
         string sql = "DELETE FROM OrgProduct WHERE OrgCode={0}";
         string delete = String.Format(sql, id);
         int res = DBUtl.ExecSQL(delete);
         if (res == 1)
         {
            TempData["Message"] = "Product Deleted";
            TempData["MsgType"] = "success";
         }
         else
         {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
         }
         return RedirectToAction("Products");
      }
      #endregion

      #region "Subscription"

      public IActionResult SubscriptionList()
      {
         string sql = "SELECT * FROM OrgSubscription";

         DataTable dt = DBUtl.GetTable(sql);

         return View(dt.Rows);
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
               Gender = gender,
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

      public IActionResult SubscriptionDelete(String id)
      {
         string sql = "DELETE FROM OrgSubscription WHERE Sno={0}";
         string delete = String.Format(sql, id);
         int res = DBUtl.ExecSQL(delete);
         if (res == 1)
         {
            TempData["Message"] = "Product Deleted";
            TempData["MsgType"] = "success";
         }
         else
         {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
         }
         return RedirectToAction("SubscriptionList");
      }
      #endregion

   }
}
