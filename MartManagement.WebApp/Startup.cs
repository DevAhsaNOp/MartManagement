using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MartManagement.WebApp.Startup))]
namespace MartManagement.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
