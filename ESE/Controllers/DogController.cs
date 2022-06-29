using Microsoft.AspNetCore.Mvc;
using L15.Models;
using System.Collections.Generic;
using System.Data;

namespace L15.Controllers
{
    public class DogController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string select = @"SELECT d.Id as DogId, d.MintNumber, d.MintDate,
                                     d.Weapon, d.ArmorColor, 
                                     d.ForSale, d.Price, 
                                     r.Id as RarityId, r.RarityValue, 
                                     d.Companion
                              FROM Dog d, Rarity r
                              WHERE d.RarityId = r.Id";
            DataTable dt = DBUtl.GetTable(select);
            return View("Index", dt.Rows);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            string select = @"SELECT * FROM Dog WHERE Id={0}";
            List<Dog> lstDog = DBUtl.GetList<Dog>(select, id);
            if (lstDog.Count == 1)
            {
                PopulateViewData(id);
                Dog dog = lstDog[0];
                return View(dog);
            }
            else
            {
                TempData["Message"] = "Dog record not found";
                TempData["MsgType"] = "warning";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(Dog sd)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "danger";
                PopulateViewData(sd.Id);
                return View("Update", sd);
            }
            else
            {
                string update = @"UPDATE Dog  
                                SET Weapon = '{1}', 
                                    ArmorColor = '{2}',
                                    ForSale = {3},  
                                    Price = {4},
                                    Accessory = '{5}',
                                    CallSign = '{6}'
                                WHERE Id={0}";
                int res = DBUtl.ExecSQL(update,
                                        sd.Id,
                                        sd.Weapon,
                                        sd.ArmorColor,
                                        sd.ForSale.Equals(true) ? 1 : 0,
                                        sd.Price,
                                        sd.Accessory,
                                        sd.CallSign
                                        );
                if (res == 1)
                {
                    TempData["Message"] = "Samurai Dog record updated";
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

        private void PopulateViewData(int id)
        {
            // Create list of all available weapons
            string[] weaponsList = new[] { "Katana", "Katana (Dual)", "Sai", "Sia (Dual)",
                                   "Kunai", "Kunai (Dual)", "Kama", "Kama (Dual)"};
            DataTable Weapons = new DataTable();
            Weapons.Columns.Add("weapon", typeof(string));
            foreach (var row in weaponsList)
            {
                Weapons.Rows.Add(row);
            }
            ViewData["Weapons"] = Weapons.Rows;

            // Create list of all available armor colors
            string[] armorColorsList = new[] { "Amethyst", "Emerald", "Rust", "Amber",
                                   "Slate", "Gold"};
            DataTable ArmorColors = new DataTable();
            ArmorColors.Columns.Add("armorcolor", typeof(string));
            foreach (var row in armorColorsList)
            {
                ArmorColors.Rows.Add(row);
            }
            ViewData["ArmorColors"] = ArmorColors.Rows;

            // Get discription of the rarity
            string select = @"SELECT r.RarityValue
                              FROM   Dog d, Rarity r
                              WHERE  d.RarityId = r.Id
                              AND    d.Id = {0}";
            DataTable dt = DBUtl.GetTable(select, id);
            if (dt.Rows.Count > 0)
            {
                ViewData["Rarity"] = dt.Rows[0]["RarityValue"];
            }

        }
    }
}
