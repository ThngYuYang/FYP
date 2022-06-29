using Microsoft.AspNetCore.Mvc;
using L15.Models;
using System.Collections.Generic;
using System.Data;

namespace L15.Controllers
{
    public class RarityController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            DataTable dt = DBUtl.GetTable("SELECT * FROM Rarity");
            return View(dt.Rows);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            string select = @"SELECT * FROM Rarity WHERE Id={0}";
            List<Rarity> lstRarity = DBUtl.GetList<Rarity>(select, id);
            if (lstRarity.Count == 1)
            {
                Rarity rv = lstRarity[0];
                return View(rv);
            }
            else
            {
                TempData["Message"] = "Rarity record not found";
                TempData["MsgType"] = "warning";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(Rarity r)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "danger";
                return View("Update", r);
            }
            else
            {
                string update = @"UPDATE Rarity  
                                SET RarityValue = '{1}' 
                                WHERE Id={0}";
                int res = DBUtl.ExecSQL(update,
                                        r.Id,
                                        r.RarityValue
                                        );
                if (res == 1)
                {
                    TempData["Message"] = "Rarity record updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("Index");
            }
        }
    }
}
