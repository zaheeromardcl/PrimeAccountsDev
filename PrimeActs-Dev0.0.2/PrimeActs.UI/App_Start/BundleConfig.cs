#region

using System.Web.Optimization;

#endregion

namespace PrimeActs
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery-ui-1.11.4.js"));


            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/knockout-file-bind.js",
                "~/Scripts/ko.dateBindings.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css/app").Include("~/Content/css/app.css"));
            bundles.Add(new StyleBundle("~/Content/css/NEWCSS").Include("~/Content/css/NEWCSS.css"));
            bundles.Add(new StyleBundle("~/Content/css/jquerycss").Include("~/Content/css/jquery-ui.min.css"));
            bundles.Add(new StyleBundle("~/Content/css/PagedList").Include("~/Content/css/PagedList.css"));
            bundles.Add(new StyleBundle("~/Content/css/PrimeActsStyles").Include("~/Content/css/PrimeActsStyles.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}