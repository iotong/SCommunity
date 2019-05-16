using ZFine.Code;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZFine.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
      
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MvcViewEngine());



        }

        public class MvcViewEngine : RazorViewEngine
        {
            public MvcViewEngine()
            {
                MasterLocationFormats = new[] { "~/Views/Shared/{0}.cshtml" };
                ViewLocationFormats = new[]
                {
            "~/Views/Admin/{1}/{0}.cshtml",
            "~/Views/{1}/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml"
        };
                PartialViewLocationFormats = ViewLocationFormats;
            }
        }
    }
}