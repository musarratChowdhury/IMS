using System.Web.Security;
using IMS.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IMS.WEB.Startup))]
namespace IMS.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SeedAdminUserAndRole();
        }
        
        private void SeedAdminUserAndRole()
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser, long>(new UserStoreLongPk(context));
                var roleManager = new RoleManager<RoleLongPk, long>(new RoleStoreLongPk(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    var role  = new RoleLongPk("Admin");
                    roleManager.Create(role);
                }
                // Create the admin user if it doesn't exist
                var adminEmail = "admin@ims.com"; // Change to the desired admin email
                var adminUser = userManager.FindByEmail(adminEmail);
                if (adminUser == null)
                // Create the "Admin" role if it doesn't exist
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                    };
                    userManager.Create(adminUser, "admin123");
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }
    }
}
