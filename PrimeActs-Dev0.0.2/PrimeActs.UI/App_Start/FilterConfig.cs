#region

using System.Web.Mvc;

#endregion

namespace PrimeActs.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PrimeActsHandleErrorAttribute());
        }
    }
}