using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PodCastApp.Startup))]
namespace PodCastApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
