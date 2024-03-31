using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Response;

namespace IraqWebsite.Services.SettingsService.UserManagmentService
{
    public interface IUserManagmentService
    {
        public Task<UserManagement> UpdateUserManagement(UserManagement model);
        public Task<UserManagement> GetUserManagment();

        public Task<ManagerResponse> GetUserLockout();
        public Task<ManagerResponse> UpdateUserLockout(UserLockout model);
    }
}
