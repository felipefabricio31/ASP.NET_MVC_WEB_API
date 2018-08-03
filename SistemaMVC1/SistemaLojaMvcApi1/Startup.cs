using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaLojaMvcApi1.Startup))]
namespace SistemaLojaMvcApi1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
