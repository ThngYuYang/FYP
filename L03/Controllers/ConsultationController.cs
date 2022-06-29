﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using L03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L03.Controllers
{
    public class ConsultationController : Controller
    {
        public IActionResult Apply()
        {
            return View("Application");
        }

        public IActionResult Confirm()
        {
            IFormCollection form = HttpContext.Request.Form;
            string staffName = form["StaffName"].ToString().Trim();
            string email = form["Email"].ToString().Trim();
            string acadPos = form["AcadPosition"].ToString();
            string startDate = form["StartDate"].ToString().Trim();
            string endDate = form["EndDate"].ToString().Trim();
            string agree = form["Agree"].ToString();

            if (ValidUtl.CheckIfEmpty(staffName, email, acadPos, startDate, endDate, agree))
            {
                ViewData["Message"] = "Please enter or select all fields";
                return View("Application");
            }

            if (!startDate.IsDate("yyyy-MM-dd") ||
                !endDate.IsDate("yyyy-MM-dd"))
            {
                ViewData["Message"] = "Invalid Date Format";
                return View("Application");
            }

            if (startDate.ToDate("yyyy-MM-dd") > endDate.ToDate("yyyy-MM-dd"))
            {
                ViewData["Message"] = "End date should be later than Start date";
                return View("Application");
            }

            // Create Consultant object
            Consultant cst = new Consultant();
            cst.StartDate = startDate.ToDate("yyyy-MM-dd");
            cst.EndDate = endDate.ToDate("yyyy-MM-dd");
            cst.StaffName = staffName;
            cst.Email = email;
            cst.AcadPos = acadPos;

            // Calculate Renumeration
            int payment = CalcRenumeration(cst);

            // Pass renumeration to View using ViewData
            ViewData["Amount"] = payment;
            // Pass Consultant object to View as Model 
            return View("Confirmation", cst);

        }

        private const int WKDAY_PROF = 120;
        private const int WKDAY_LECT = 100;
        private const int WKDAY_ADJT = 90;

        private const int WKEND_EXTRA = 80;

        private int CalcRenumeration(Consultant cst)
        {
            int days = (cst.EndDate - cst.StartDate).Days + 1;
            int sat_sun = CalcWeekendDays(cst.StartDate, cst.EndDate);
            int amount = sat_sun * WKEND_EXTRA;

            if (cst.AcadPos.Equals("Professor"))
            {
                amount += WKDAY_PROF * days;
            }
            else if (cst.AcadPos.Equals("Lecturer"))
            {
                amount += WKDAY_LECT * days;
            }
            else if (cst.AcadPos.Equals("Adjunct"))
            {
                amount += WKDAY_ADJT * days;
            }
            return amount;
        }

        private int CalcWeekendDays(DateTime start, DateTime end)
        {
            int wk = 0;
            int days = (end - start).Days + 1;
            for (int i = 0; i < days; i++)
            {
                DateTime day = start.AddDays(i);
                if (day.DayOfWeek == DayOfWeek.Saturday ||
                    day.DayOfWeek == DayOfWeek.Sunday)
                    wk++;
            }
            return wk;
        }

    }
}
//20031509 Thng Yu Yang