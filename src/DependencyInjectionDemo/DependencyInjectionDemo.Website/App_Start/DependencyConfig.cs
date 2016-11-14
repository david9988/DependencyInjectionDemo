using DependencyInjectionDemo.Website.Helpers;

namespace DependencyInjectionDemo.Website
{
    public static class DependencyConfig
    {
        public static void RegisterDependencyResolver()
        {
            System.Web.Mvc.DependencyResolver.SetResolver(DependencyResolver.Current);
        }
    }
}