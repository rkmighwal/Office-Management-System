using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OMS.Startup))]
namespace OMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
