using Epione.Data;
using Epione.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Epione.Web.Startup))]
namespace Epione.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            EpioneContext context = new EpioneContext();
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManger = new UserManager<User>(new UserStore<User>(context));
            if (!roleManger.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManger.Create(role);
                var user = new User();
                user.UserName ="SlimHamdi";
                user.Email = "Slim.Hamdi@esprit.tn";
                string pwd = "07471917Slim";
                var newuser = userManger.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManger.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManger.RoleExists("Patient"))
            {
                var role = new IdentityRole();
                role.Name = "Patient";
                roleManger.Create(role);

            }
            if (!roleManger.RoleExists("Doctor"))
            {
                var role = new IdentityRole();
                role.Name = "Doctor";
                roleManger.Create(role);

            }
        }
    }
}
