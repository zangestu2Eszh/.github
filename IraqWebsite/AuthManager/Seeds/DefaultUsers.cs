using Microsoft.AspNetCore.Identity;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Statics;

namespace IraqWebsite.AuthManager.Seeds
{
    public static class DefaultUsers
    {
        private const string Default_Password = "QW!@qw12";

        private static ApplicationUser SuperAdminUser = new ApplicationUser()
        {
            FirstName = "Admin",
            LastName = "App",
            UserName = "software@Tm.Iq",
            Email = "software@Tm.Iq",
            IsActive = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            ProfilePicture = "/Images/Admin.png"
        };

        public static async Task SeedSuperAdminUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = await userManager.FindByEmailAsync(SuperAdminUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(SuperAdminUser, Default_Password);

                await userManager.AddToRoleAsync(SuperAdminUser, Roles.SuperAdmin.ToString());
            }

            await roleManager.SeedClaimsForSuperAdmin();
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var superAdminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await roleManager.AddPermissionClaim(superAdminRole, "Role");
            await roleManager.AddPermissionClaim(superAdminRole, "User Manag");
            await roleManager.AddPermissionClaim(superAdminRole, "User Lock");
            await roleManager.AddPermissionClaim(superAdminRole, "Dashboard");
            await roleManager.AddPermissionClaim(superAdminRole, "User");
            await roleManager.AddPermissionClaim(superAdminRole, "Email Setting");
            await roleManager.AddPermissionClaim(superAdminRole, "Psaaword Complexity");
            await roleManager.AddPermissionClaim(superAdminRole, "Appearance");
            await roleManager.AddPermissionClaim(superAdminRole, "Slider");
            await roleManager.AddPermissionClaim(superAdminRole, "Schedule Section");
            await roleManager.AddPermissionClaim(superAdminRole, "Schedule Sub Section");
            await roleManager.AddPermissionClaim(superAdminRole, "About");
            await roleManager.AddPermissionClaim(superAdminRole, "Portfolio");
            await roleManager.AddPermissionClaim(superAdminRole, "Event");
            await roleManager.AddPermissionClaim(superAdminRole, "News");
            await roleManager.AddPermissionClaim(superAdminRole, "Partner");
            await roleManager.AddPermissionClaim(superAdminRole, "Contact");
            await roleManager.AddPermissionClaim(superAdminRole, "FmLink");
            await roleManager.AddPermissionClaim(superAdminRole, "Log");
            await roleManager.AddPermissionClaim(superAdminRole, "Meta");
            await roleManager.AddPermissionClaim(superAdminRole, "Recaptcha");
            await roleManager.AddPermissionClaim(superAdminRole, "Subscribers");
        }

        private async static Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissions(module);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", permission));
                }

            }
        }
    }
}
