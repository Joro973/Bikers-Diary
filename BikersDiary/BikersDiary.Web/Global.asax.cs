using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BikersDiary.ForumSystem.Data;
using BikersDiary.ForumSystem.Data.Migrations;
using BikersDiary.ForumSystem.Web.App_Start;

namespace BikersDiary.ForumSystem.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
