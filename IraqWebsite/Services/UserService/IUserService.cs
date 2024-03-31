using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.AuthManager.Response;

namespace IraqWebsite.Services.UserService
{
    public interface IUserService
    {
        public Task<List<ListUsersViewModel>> ConfirmAccount(string userId);
        public Task<List<ListUsersViewModel>> GetUsers();
        public Task DeleteUser(string userId);
        public Task<CreateUserViewModel> CreateUser(CreateUserViewModel model);
        public Task<List<IdentityRole>> GetRoles();
        public Task<IdentityRole> GetRoleByName(string roleName);
        public Task<PermissionViewModel> GetRoleClaim(string roleName);
        public Task<List<IdentityRole>> AddRole(PermissionViewModel model);
        public Task<List<IdentityRole>> EditRole(PermissionViewModel model);
        public Task<List<IdentityRole>> RemoveRole(string roleName);
        public Task<ManageUserRoleViewModel> GetUserRoles(string userId);
        public Task<IdentityRole> GetRoleByUserId(string userId);
        public Task<ManagerResponse> UpdateUserRole(string userId,string roleName);
        public Task<ApplicationUser> EditUser(CreateUserViewModel model);
        public PermissionViewModel GetAllPermission();
        public Task<List<EmailSettings>> AddEmailSettings(EmailSettings model);
        public Task<List<EmailSettings>> GetEmailSettings();
        public Task<EmailSettings> DetailsEmailSettings(Guid? id);
        public Task<List<EmailSettings>> EditEmailSettings(Guid? id ,EmailSettings model);
        public Task<List<EmailSettings>> RemoveEmailSettings(Guid? id);
        public Task<ApplicationUser> GetUserById(string userId);
        public string Error { get; set; }

        public Task<ManagerResponse> SaveUserImage(string email, IFormFile file);
        public Task<List<UserActivityLog>> SystemLog();
        public Task<List<UserActivityLog>> AuthLog();

        public Task<ManagerResponse> Filter(UserFilterViewModel model);
        public Task<string> GetCurrentUserId(ClaimsPrincipal currentUser);
        public Task<ManagerResponse> GetRecentUsers();
        public Task<bool> CustemIsInRoleAsync(ApplicationUser user, string roleName);
        public Task<bool> ForgetPassword(string email,string url);
        public Task DeleteSystemLog();
        public Task DeleteAuthLog();
    }
}
