using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using L09.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace L09.Controllers
{
   public class AdminController : Controller
   {
      // TODO: L09 Task 6 - Add only the admin role to the [Authorize] attributes (4x)
      [Authorize(Roles = "admin")]
      public IActionResult Users()
      {
         List<TravelUser> list = DBUtl.GetList<TravelUser>("SELECT * FROM TravelUser");
         return View(list);
      }

      [Authorize(Roles = "admin")]
      public IActionResult CreateUser()
      {
         return View();
      }

      [Authorize(Roles = "admin")]
      [HttpPost]
      public IActionResult CreateUser(TravelUser usr)
      {
            // TODO: L09 Task 5 - Write secure code to insert TravelUser into database
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "InvalidInput";
                ViewData["MsgType"] = "warning";
                return View("CreateUser");
            }
            else
            {
                String insert = @"INSERT INTO TravelUser(UserID,UserPW,FullName,Email,Dob,UserRole) VALUES('{0}', HASHBYTES('SHA1', '{1}'), '{2}', '{3}', '{4:yyyy-MM-dd}', '{5}')";

                if(DBUtl.ExecSQL(insert,usr.UserId,usr.UserPw, usr.FullName, usr.Email, usr.Dob,usr.UserRole) == 1)
                {
                    TempData["Message"] = "User Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("Users");
         }
      }

      [Authorize(Roles = "admin")]
      public IActionResult Delete(string id)
      {
         string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
         if (userid.Equals(id, StringComparison.InvariantCultureIgnoreCase))
         {
            TempData["Message"] = "Own ID cannot be deleted";
            TempData["MsgType"] = "warning";
         }
         else
         {
            string delete = "DELETE FROM TravelUser WHERE UserId='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
               TempData["Message"] = "User Record Deleted";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
         }
         return RedirectToAction("Users");
      }

     

   }
}
// 20031509 Thng Yu Yang