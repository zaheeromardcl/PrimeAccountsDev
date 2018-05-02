#region

using System.Web.Mvc;
using System.Web.Routing;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers
{
    public abstract class PrimeActsController : Controller
    {
        public string controllerName;
        public SubNavModel subNavModel;

        public ActionResult SubNav()
        {
            subNavModel = new SubNavModel(controllerName);
            return PartialView("_SubNav", subNavModel);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            controllerName = requestContext.RouteData.Values["controller"].ToString();
        }
    }
}