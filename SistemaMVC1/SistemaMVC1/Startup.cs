using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaMVC1.Startup))]
namespace SistemaMVC1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
