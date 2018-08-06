using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaLoja.Models;
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

namespace SistemaLoja
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.SistemaLojaContext, Migrations.Configuration>());
            
            //Referencia para conexao default
            ApplicationDbContext db = new ApplicationDbContext();
            CriarRoles(db);
            CriarSuperUsuario(db);
            AddPermissoesSuperUSuario(db);
            db.Dispose();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermissoesSuperUSuario(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("felipe@gmail.com");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if(!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");

                userManager.AddToRole(user.Id, "Create");

                userManager.AddToRole(user.Id, "Edit");
                
                userManager.AddToRole(user.Id, "Delete");
            }
        }

        private void CriarSuperUsuario(ApplicationDbContext db)
        {
            //Atribui uma conexao que serve para Criar um novo usuario
            //Utiliza a conexao Default
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("felipe@gmail.com");

            //Verifica se existe
            if(user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "felipe@gmail.com", 
                    Email = "felipe@gmail.com",

                };
                //Cria usuario
                userManager.Create(user, "Felipe0123*");
            }

        }



        // Como o banco é dinamico, sempre que acessar este metodo 
        // O sistema verifica se existe, caso exista ele não cria um novo.
        private void CriarRoles(ApplicationDbContext db)
        {
            //Atribui uma conexao que serve para gerar os metodos do CRUD para variavel
            //Utilizara a conexao Default
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if(!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
        }
    }
}
