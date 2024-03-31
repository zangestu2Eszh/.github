using IraqWebsite.AuthManager.Models;
using IraqWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.AuthManager.Seeds
{
    public class DefaultsSettings
    {
        private static readonly Appearance _appearance = new()
        {
            Id = Guid.NewGuid(),
            ApplicationName = "Tm",
            ApplicationLogo = "/Images/Tm.png",
            ColorWhite = "#fff",
            ColorBlack = "#000000",
            ColorMode = false,
            FavIcon = "/Images/Tm.ico",
            MetaTitle = "Auib Fm",
            MetaDescription = "AUIB FM is a non for proﬁt radio station that is directed to the students of the American Univer- sity of Iraq-Baghdad. AUIB FM is an English speaking radio station that is broadcasting from Baghdad on the frequency 101.5 every day for 24 hours."
        };

        private static readonly UserLockout _userLockout = new()
        {
            Id = Guid.NewGuid(),
            AccountLocking = true,
            AccountLockingDuration = 300,
            LoginAttemptCount = 4
        };

        private static readonly UserManagement _userManagment = new()
        {
            Id = Guid.NewGuid(),
            IsAllowedToRegister = false,
            IsActiveByDeffult = false,
            UseGoogleRecaphaOnRegister = false,
            UseGoogleRecaphaOnLogin = false
        };

        public static async Task SeedSettings(ApplicationDbContext context)
        {
            if (!await context.Appearance.AnyAsync())
            {
                context.Appearance.Add(_appearance);
                await context.SaveChangesAsync();
            }

            if (!await context.UserLockout.AnyAsync())
            {
                context.UserLockout.Add(_userLockout);
                await context.SaveChangesAsync();
            }

            if (!await context.UserManagement.AnyAsync())
            {
                context.UserManagement.Add(_userManagment);
                await context.SaveChangesAsync();
            }
        }
    }
}
