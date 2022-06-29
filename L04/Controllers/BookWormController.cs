using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace L04.Controllers
{
    public class BookWormController : Controller
    {
        const string SELECT1 = // QUESTION ONE
         @"SELECT isbn AS 'Isbn', title AS 'Title', lang AS 'Lang' FROM BwBook bb WHERE bb.Lang != 'English';";

        const string SELECT2 = // QUESTION TWO
           @"SELECT isbn AS 'Isbn', title AS 'Title', qty as 'Qty' FROM BwBook bb WHERE bb.Qty = 0;";

        const string SELECT3 = // QUESTION THREE
           @"SELECT P.PubName AS Publisher,  COUNT(DISTINCT B.Title) AS Titles FROM BwPublisher P, BwBook B WHERE P.PubID = B.PubID GROUP BY P.PubName;";

        const string SELECT4 = // QUESTION FOUR
           @"SELECT title AS 'Title' FROM BwBook GROUP BY title HAVING COUNT(lang) > 1;";

        public IActionResult Query()
        {
            return View();
        }

        public IActionResult Submit()
        {
            IFormCollection form = HttpContext.Request.Form;
            string question = form["Question"].ToString();

            string sql = "";
            if (question.Equals("1"))
            {
                sql = SELECT1;
            }
            else if (question.Equals("2"))
            {
                sql = SELECT2;
            }
            else if (question.Equals("3"))
            {
                sql = SELECT3;
            }
            else if (question.Equals("4"))
            {
                sql = SELECT4;
            }

            DataTable dt = DBUtl.GetTable(sql);
            return View("Query", dt);
        }

    }

}
//20031509 Thng Yu Yang
