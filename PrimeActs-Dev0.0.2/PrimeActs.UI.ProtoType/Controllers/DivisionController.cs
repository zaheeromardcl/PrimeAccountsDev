using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrimeActs.UI.ProtoType.Controllers
{
    public class DivisionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult DynamicIndex()
        {
            ViewBag.Title = "Dynamic Page";

            return View();
        }
    }
}
