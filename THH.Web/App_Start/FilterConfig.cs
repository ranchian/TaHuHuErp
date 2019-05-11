using System.Web;
using System.Web.Mvc;

namespace THH.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute() { View = "~/Views/Shared/ErrorPage.cshtml" });
        }
    }
}
