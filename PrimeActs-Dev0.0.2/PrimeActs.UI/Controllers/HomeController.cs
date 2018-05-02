#region

using System.Web.Mvc;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class HomeController : PrimeActsController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalesHome()
        {
            return View();
        }

        public ActionResult TradingHome()
        {
            return View();
        }

        public ActionResult PurchaseHome()
        {
            return View();
        }

        public ActionResult NominalHome()
        {
            return View();
        }

        public ActionResult SettingsHome()
        {
            return View();
        }

        public ActionResult NewHome()
        {
            return View();
        }

        public ActionResult Consignment()
        {
            return View();
        }

        public ActionResult Layout()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogoutConfirmation()
        {
            return View();
        }
    }
}