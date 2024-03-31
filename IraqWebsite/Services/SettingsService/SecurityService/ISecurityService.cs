using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Response;

namespace IraqWebsite.Services.SettingsService.SecurityService
{
    public interface ISecurityService
    {
        public Task<ManagerResponse> UpdatePasswordComplexity(PasswordComplexityApp model);
        public Task<PasswordComplexityApp> GetPasswordComplexity();
    }
}
