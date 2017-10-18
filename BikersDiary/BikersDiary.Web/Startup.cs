using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikersDiary.ForumSystem.Web.Startup))]
namespace BikersDiary.ForumSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
