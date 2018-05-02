#region

using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PrimeActs.Domain.ViewModels;

#endregion

public static class HtmlHelperExtensions
{
    public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        return new HtmlString(JsonConvert.SerializeObject(model, settings));
    }

    public static MvcHtmlString BuildKnockoutSortableLink(this HtmlHelper htmlHelper,
        string fieldName, string actionName, string controllerName, string sortField)
    {
        //var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        return new MvcHtmlString(string.Format(
            "<a href=\"{0}\" class = \"NavigationLinks\" data-bind=\"click: Paging().sortEntitiesBy\"" +
            " data-sort-field=\"{1}\">{2} " +
            "<span data-bind=\"css: Paging().buildSortIcon('{1}')\"></span></a>",
            controllerName + "/" + actionName,
            sortField,
            fieldName));
    }

    public static MvcHtmlString BuildKnockoutSortableLinkNewStyle(this HtmlHelper htmlHelper,
    string fieldName, string actionName, string controllerName, string sortField, string sortType = "")
    {
        return new MvcHtmlString(string.Format(
            "<a href=\"{0}\" class = \"panel-title\" data-bind=\"click: Paging().sortEntitiesBy\"" +
            " data-sort-field=\"{1}\" data-sort-type=\"{3}\">{2} " +
            "<span class='glyphicon glyphicon-resize-vertical'></span></a>",
            controllerName + "/" + actionName,
            sortField,
            fieldName, sortType));
    }

    public static MvcHtmlString BuildKnockoutNextPreviousLinks(this HtmlHelper htmlHelper, string actionName,
        string controllerName, string whichPaging)
    {
        return new MvcHtmlString(string.Format(
            "<nav><ul class=\"pagination\">" +
            "        <li data-bind=\"css: Paging().buildPreviousClass\">" +
            "           <a href=\"{0}\" class=\"pagingnav\" data-bind=\"click: Paging().previousPage\"><<</a></li>" +
            "       <li class=\"active\"><a href=\"#\" data-bind=\"text: Paging().queryOptions.currentPage\"></a></li><li> <a href=\"#\" data-bind=\"text: Paging().queryOptions.totalPages\"></a></li>" +
            "        <li data-bind=\"css: Paging().buildNextClass\">" +
            "           <a href=\"{0}\" class=\"pagingnav\" data-bind=\"click: Paging().nextPage\">>></a></li>" +
            "</ul></nav>",
            controllerName + "/" + actionName
            ));
    }

    public static MvcHtmlString BuildKnockoutNextPreviousLinks(this HtmlHelper htmlHelper, string actionName,
        string controllerName)
    {
        //var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        return new MvcHtmlString(string.Format(
            "<nav><ul class=\"pagination\">" +
            "        <li data-bind=\"css: Paging().buildPreviousClass\">" +
            "           <a href=\"{0}\" class=\"pagingnav\" data-bind=\"click: Paging().previousPage\"><<</a></li>" +
            "       <li class=\"active\"><a href=\"#\" data-bind=\"text: Paging().queryOptions.currentPage\"></a></li><li> <a href=\"#\" data-bind=\"text: Paging().queryOptions.totalPages\"></a></li>" +
            "        <li data-bind=\"css: Paging().buildNextClass\">" +
            "           <a href=\"{0}\" class=\"pagingnav\" data-bind=\"click: Paging().nextPage\">>></a></li>" +
            "</ul></nav>",
            controllerName + "/" + actionName
            ));
    }

    public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper,
        string fieldName, string actionName, string controllerName, string sortField, QueryOptions queryOptions)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        var isCurrentSortField = queryOptions.SortField == sortField;

        return new MvcHtmlString(string.Format("<a href=\"{0}\">{1} {2}</a>",
            urlHelper.Action(actionName, controllerName,
                new
                {
                    SortField = sortField,
                    SortOrder = (isCurrentSortField
                                 && queryOptions.SortOrder == SortOrder.ASC.ToString())
                        ? SortOrder.DESC
                        : SortOrder.ASC
                }),
            fieldName,
            BuildSortIcon(isCurrentSortField, queryOptions)));
    }

    private static string BuildSortIcon(bool isCurrentSortField, QueryOptions queryOptions)
    {
        var sortIcon = "sort";

        if (isCurrentSortField)
        {
            sortIcon += "-by-alphabet";
            if (queryOptions.SortOrder == SortOrder.DESC.ToString())
                sortIcon += "-alt";
        }

        return string.Format("<span class=\"{0} {1}{2}\"></span>",
            "glyphicon", "glyphicon-", sortIcon);
    }

    public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlHelper, QueryOptions queryOptions,
        string actionName, string controllerName)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        return new MvcHtmlString(string.Format(
            "<nav>" +
            "    <ul class=\"pager\">" +
            "        <li class=\"previous {0}\">{1}</li>" +
            "        <li class=\"next {2}\">{3}</li>" +
            "    </ul>" +
            "</nav>",
            IsPreviousDisabled(queryOptions),
            BuildPreviousLink(urlHelper, queryOptions, actionName, controllerName),
            IsNextDisabled(queryOptions),
            BuildNextLink(urlHelper, queryOptions, actionName, controllerName)
            ));
    }

    private static string IsPreviousDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == 1)
            ? "disabled"
            : string.Empty;
    }

    private static string IsNextDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == queryOptions.TotalPages)
            ? "disabled"
            : string.Empty;
    }

    private static string BuildPreviousLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName,
        string controllerName)
    {
        return string.Format(
            "<a href=\"{0}\"><span aria-hidden=\"true\">&larr;</span> Previous</a>",
            urlHelper.Action(actionName, controllerName, new
            {
                queryOptions.SortOrder,
                queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage - 1,
                queryOptions.PageSize
            }));
    }

    private static string BuildNextLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName,
        string controllerName)
    {
        return string.Format(
            "<a href=\"{0}\">Next <span aria-hidden=\"true\">&rarr;</span></a>",
            urlHelper.Action(actionName, controllerName, new
            {
                queryOptions.SortOrder,
                queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage + 1,
                queryOptions.PageSize
            }));
    }
}