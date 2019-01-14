using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UdlaOverflow.Startup))]
namespace UdlaOverflow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
