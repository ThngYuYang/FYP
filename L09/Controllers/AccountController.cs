using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using L09.Models;
using Microsoft.AspNetCore.Authorization;

namespace L09.Controllers
{
   public class AccountController : Controller
   {
      [Authorize]
      public IActionResult Logoff(string returnUrl = null)
      {
         HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
         return RedirectToAction("Index", "Travel");
      }

      [AllowAnonymous]
      public IActionResult Login(string returnUrl = null)
      {
         TempData["ReturnUrl"] = returnUrl;
         return View();
      }

      [AllowAnonymous]
      [HttpPost]
      public IActionResult Login(UserLogin user)
      {
         if (!AuthenticateUser(user.UserID, user.Password, 
                               out ClaimsPrincipal principal))
         {
            ViewData["Message"] = "Incorrect User ID or Password";
            return View();
         }
         else
         {
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               principal);

            // Update the Last Login Timestamp of the User
            string update = "UPDATE TravelUser SET LastLogin=GETDATE() WHERE UserId='{0}'";
            DBUtl.ExecSQL(update, user.UserID);

            if (TempData["returnUrl"] != null)
            {
               string returnUrl = TempData["returnUrl"].ToString();
               if (Url.IsLocalUrl(returnUrl))
                  return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Travel");
         }
      }

      [AllowAnonymous]
      public IActionResult Forbidden()
      {
         return View();
      }

      private bool AuthenticateUser(string uid, string pw,
                                       out ClaimsPrincipal principal)
      {
         principal = null;


         string sql = @"SELECT * FROM TravelUser 
                         WHERE UserId = '{0}' AND UserPw = HASHBYTES('SHA1', '{1}')";

         // TODO: L09 Task 1 - Make Login Secure, use the new way of calling DBUtl
         DataTable ds = DBUtl.GetTable(sql, uid, pw);
         if (ds.Rows.Count == 1)
         {
            principal =
               new ClaimsPrincipal(
                  new ClaimsIdentity(
                     new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, uid),
                        new Claim(ClaimTypes.Name, ds.Rows[0]["FullName"].ToString()),
                        new Claim(ClaimTypes.Role, ds.Rows[0]["UserRole"].ToString())
                     },
                     CookieAuthenticationDefaults.AuthenticationScheme));
            return true;
         }
         return false;
      }

   }
}
// 20031509 Thng Yu Yang