using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bikers_Diary.Startup))]
namespace Bikers_Diary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
