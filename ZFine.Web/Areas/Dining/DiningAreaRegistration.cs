using System.Web.Mvc;

namespace ZFine.Web.Areas.Dining
{
    public class DiningAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Dining";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Dining_default",
                "Dining/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
