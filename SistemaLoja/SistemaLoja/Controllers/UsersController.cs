using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaLoja.Models;
using SistemaLoja.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaLoja.Controllers
{
    [Authorize(Users ="hugovasconcelosf@hotmail.com")]
    public class UsersController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();
            foreach(var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Nome = user.UserName,
                    UserId = user.Id
                };

                usersView.Add(userView);
            }
            return View(usersView);
        }


        public ActionResult Delete(string userId, string roleId)
        {
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId)){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.Users.ToList().Find(u => u.Id == userId); 

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleId); 

            if(userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            var users = userManager.Users.ToList();



          
          
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }




            var userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);

        }

        public ActionResult AddRole(string userId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userId);
            var userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id
                
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Selecione uma Permissão!]" });
            list = list.OrderBy(c => c.Name).ToList();
            ViewBag.RoleId = new SelectList(list, "Id", "Name");

            return View(userView);
        }

        [HttpPost]
        public ActionResult AddRole(string userId, FormCollection form)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userId);
            var userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id

            };


            var roleId = Request["RoleId"];
            if (string.IsNullOrEmpty(roleId))
            {
                
                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Selecione uma Permissão!]" });
                list = list.OrderBy(c => c.Name).ToList();
                ViewBag.RoleId = new SelectList(list, "Id", "Name");

                ViewBag.Error = "Você precisa selecionar uma permissão!!";
                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleId);
            if (!userManager.IsInRole(userId, role.Name)) {
                userManager.AddToRole(userId, role.Name);
            }

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }




            userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            

            return View("Roles", userView);
        }

        public ActionResult Roles(string userId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            

            var user = users.Find(u => u.Id == userId);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles){
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }
            

           

            var userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }



   

}