#region

using System.Web.Mvc;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult NotAuthorized()
        {
            return PartialView("_NotAuthorized");
        }
    }
}