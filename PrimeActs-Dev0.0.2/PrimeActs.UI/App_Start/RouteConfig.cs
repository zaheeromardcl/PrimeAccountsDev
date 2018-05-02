#region

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace PrimeActs.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Account", action = "Login", id = UrlParameter.Optional});


            routes.MapRoute("IsActive", "{controller}/{action}/{IsActive}",
                new {controller = "Division", action = "Index", IsActive = UrlParameter.Optional});


            routes.MapRoute("Id", "{controller}/{action}/{Id}",
               new { controller = "Ticket", action = "Details", Id = UrlParameter.Optional });

            routes.MapRoute("DetailsTab", "{controller}/{action}/{tabId}/{id}", new { tabId = UrlParameter.Optional  });
            routes.IgnoreRoute("elmah.axd");
        }
    }
}