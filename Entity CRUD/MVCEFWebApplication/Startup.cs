using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCEFWebApplication.Startup))]
namespace MVCEFWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
