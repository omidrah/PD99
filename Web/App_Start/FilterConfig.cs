using System.Web;
using System.Web.Mvc;
using Web.Providers;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ParsianAuthorize());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
