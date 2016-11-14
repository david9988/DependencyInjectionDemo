using System.Web.Mvc;
using System.Web.Routing;

namespace DependencyInjectionDemo.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyConfig.RegisterDependencyResolver();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            Helpers.DependencyResolver.Current = null;
        }

    }
}
