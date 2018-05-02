#region

using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

#endregion

namespace PrimeActs.UI.Models.HtmlHelper
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString PrimeActsCheckBoxFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string id,
            string text,
            object htmlAttributes = null)
        {
            return PrimeActsCheckBoxFor(htmlHelper, expression, id, text, false, false, false, htmlAttributes);
        }

        public static MvcHtmlString PrimeActsCheckBoxFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string id,
            string text,
            bool isChecked,
            bool isAutoFocus,
            bool useInline = false,
            object htmlAttributes = null)
        {
            var sb = new StringBuilder(512);
            var htmlChecked = string.Empty;
            var htmlAutoFocus = string.Empty;

            if (isChecked)
            {
                htmlChecked = "checked='checked'";
            }
            if (isAutoFocus)
            {
                htmlAutoFocus = "autofocus='autofocus'";
            }

            // Build the CheckBox
            if (useInline)
            {
                sb.Append("<label class='checkbox-inline'>");
            }
            else
            {
                //sb.Append("<div class='checkbox'>");
                sb.Append("  <label>");
            }
            sb.AppendFormat(
                "    <input id='{0}' name='{0}' type='checkbox' value='true' {1} {2}/><input name='{0}' type='hidden' value='false' {3} />",
                id, htmlChecked, htmlAutoFocus,
                GetHtmlAttributes(htmlAttributes));
            sb.AppendFormat("    {0}", text);
            if (useInline)
            {
                sb.Append("</label>");
            }
            else
            {
                sb.Append("  </label>");
                //sb.Append("</div>");
            }

            // Return an MVC HTML String
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string GetHtmlAttributes(object htmlAttributes)
        {
            var ret = string.Empty;

            if (htmlAttributes != null)
            {
                var attributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    ret += " " + item.Key + "=" + "'" + item.Value + "'";
                }
            }

            return ret;
        }
    }
}