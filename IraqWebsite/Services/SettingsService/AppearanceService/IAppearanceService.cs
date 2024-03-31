using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Response;
using IraqWebsite.AuthManager.ViewModels;

namespace IraqWebsite.Services.SettingsService.AppearanceService
{
    public interface IAppearanceService
    {
        public Task<Appearance> UpdateAppearance(AppearanceViewModel model);
        public Task<Appearance> GetAppearance();
    }
}
