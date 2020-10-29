using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~"+Links.Scripts.bootstrap_js.ToString(),
                      "~"+Links.Scripts.jquery_validate_min_js,
                      "~"+Links.Scripts.jquery_validate_unobtrusive_min_js
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~" + Links.Content.bootstrap_css,
                      "~" + Links.Content.Site_Dashboard_css                      
                      ));

            bundles.Add(new StyleBundle("~/Content/BackEndcss").Include(
                  "~"+Links.Content.bootstrap_css,
                  "~"+Links.Areas.Admin.Content.font_awesome_css,
                  "~"+Links.Areas.Admin.Content.Back_Site_css,
                  "~"+Links.Areas.Admin.Content.kendo._2015_1_408.kendo_common_min_css,
                  "~"+Links.Areas.Admin.Content.kendo._2015_1_408.kendo_mobile_all_min_css,
                  "~"+Links.Areas.Admin.Content.kendo._2015_1_408.kendo_blueopal_min_css,
                  "~"+Links.Areas.Admin.Content.kendo._2015_1_408.kendo_dataviz_default_min_css,
                  "~"+Links.Areas.Admin.Content.kendo._2015_1_408.kendo_rtl_min_css
            ));
            bundles.Add(new ScriptBundle("~/Js/BackJs").Include(    
                "~"+Links.Scripts.kendo._2015_1_408.jquery_min_js,
                "~"+Links.Scripts.kendo._2015_1_408.jszip_min_js,
                "~"+Links.Scripts.kendo._2015_1_408.kendo_all_min_js,
                "~"+Links.Scripts.kendo._2015_1_408.kendo_aspnetmvc_min_js,
                "~"+Links.Scripts.KendoPersianDate.JalaliDate_js,
                "~"+Links.Scripts.KendoPersianDate.kendo_web_js,
                "~"+Links.Scripts.KendoPersianDate.fa_IR_js
                      ));
            bundles.Add(new ScriptBundle("~/Js/FooterBackJs").Include(
               "~"+Links.Scripts.jquery_noty_packaged_min_js,
               "~"+Links.Scripts.deleteNoty_js
                      ));

            
        }
    }
}
