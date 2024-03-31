using Microsoft.AspNetCore.Identity;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Statics;

namespace IraqWebsite.AuthManager.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            }

            if (await roleManager.FindByNameAsync(Roles.User.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }
        }
    }
}
