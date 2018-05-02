using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace PrimeActs.UI
{
    public class PrimeActsHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            if (filterContext.Exception is AuthenticationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Redirect("~/");
            }

            base.OnException(filterContext);
        }
    }
}