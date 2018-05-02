using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrimeActs.UI.Controllers
{
    public class StockboardAdminController : PrimeActsController
    {
        
        // GET: List Stockboards available filter by dept, date, salesperson
        public ActionResult Stockboards()
        {
            return View();
        }

        
        // GET: StockboardAdmin - create stockboards using drag/drop of Produce Groups By Group by clause
        // Create limited by number of stockboards for that user and type of license
        //By Group by clause only if they have create permissions
        public ActionResult Create()
        {
            //edit produce groups displayed
            return View();
        }

        // GET: StockboardAdmin - create/edit stockboards using drag/drop of Produce Groups 
        //By Group by clause only if they have edit permissions
        public ActionResult Edit()
        {
            //edit produce groups displayed
            return View();
        }

        //By Group by clause only if they have edit permissions
        public ActionResult Delete()
        {
            //edit produce groups displayed
            return View();
        }
    }
}