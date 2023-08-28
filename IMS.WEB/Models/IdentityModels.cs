using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IMS.WEB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<long, UserLoginLongPk, UserRoleLongPk, UserClaimLongPk>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, 
                DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UserRoleLongPk : IdentityUserRole<long>
    {
        
    }

    public class UserClaimLongPk : IdentityUserClaim<long>
    {
        
    }
    
    public class UserLoginLongPk : IdentityUserLogin<long>
    {        
    }
    
    public class RoleLongPk : IdentityRole<long, UserRoleLongPk>
    {
        public RoleLongPk() {}

        public RoleLongPk(string name)
        {
            Name = name;
        }
    }
    
    
    public class UserStoreLongPk : UserStore<ApplicationUser, RoleLongPk, long, 
        UserLoginLongPk, UserRoleLongPk, UserClaimLongPk>
    {
        public UserStoreLongPk(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreLongPk : RoleStore<RoleLongPk, long, UserRoleLongPk>
    {
        public RoleStoreLongPk(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RoleLongPk, long, 
            UserLoginLongPk, UserRoleLongPk, UserClaimLongPk>
        {
            public ApplicationDbContext()
                : base("DefaultConnection")
            {
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
        }
}