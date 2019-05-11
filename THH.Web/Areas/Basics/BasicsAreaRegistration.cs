using System.Web.Mvc;

namespace THH.Web.Areas.Basics
{
    public class BasicsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Basics";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Basics_default",
                "Basics/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "THH.Web.Areas.Basics.Controllers" }
            );
        }
    }
}