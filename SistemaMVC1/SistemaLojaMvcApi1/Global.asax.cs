using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SistemaLojaMvcApi1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Uma forma de migração automática
            //Sempre que ele iniciar o projeto ele inicia uma nova versão esquecendo a antiga.
            //Desta forma o banco sempre é atualizado.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.SistemaLojaMvcApi1Context, Migrations.Configuration>());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
