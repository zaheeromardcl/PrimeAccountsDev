using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrimeActs.UI.Controllers
{
    public class JournalController : Controller
    {
        // GET: Journal
        public ActionResult JournalEntry()
        {
            return View();
        }
    }
}