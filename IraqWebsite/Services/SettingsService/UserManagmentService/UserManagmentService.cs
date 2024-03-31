using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Response;
using IraqWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Services.SettingsService.UserManagmentService
{
    public class UserManagmentService : IUserManagmentService
    {
        private readonly ApplicationDbContext _context;

        public UserManagmentService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<UserManagement?> GetUserManagment()
        {
            return await _context.UserManagement.FirstOrDefaultAsync();
        }

        public async Task<UserManagement> UpdateUserManagement(UserManagement model)
        {
            var userManagement = await _context.UserManagement.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (userManagement is null)
            {
                model.Id = Guid.NewGuid();
                _context.UserManagement.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            userManagement.IsAllowedToRegister = model.IsAllowedToRegister;
            userManagement.IsActiveByDeffult = model.IsActiveByDeffult;
            userManagement.UseGoogleRecaphaOnRegister = model.UseGoogleRecaphaOnRegister;
            userManagement.UseGoogleRecaphaOnLogin = model.UseGoogleRecaphaOnLogin;

            _context.UserManagement.Update(userManagement);
            await _context.SaveChangesAsync();

            return userManagement;
        }

        public async Task<ManagerResponse> GetUserLockout()
        {
            var userManagement = await _context.UserLockout.FirstOrDefaultAsync();
            if (userManagement is not null)
            {
                return ManagerResponse.Response("Send Successfully", true, new List<string>(), new List<object> { userManagement });
            }
            return ManagerResponse.Response("Invalid Value", false, new List<string> { "No Data Found" }, new List<object>());
        }

        public async Task<ManagerResponse> UpdateUserLockout(UserLockout model)
        {
            if (model is null)
            {
                return ManagerResponse.Response("Invalid Values", false, new List<string> { "No Data Found" }, new List<object> { model });
            }

            var userLockout = await _context.UserLockout.FirstOrDefaultAsync();
            if (userLockout is not null)
            {
                userLockout.AccountLocking = model.AccountLocking;
                userLockout.AccountLockingDuration = model.AccountLockingDuration;
                userLockout.LoginAttemptCount = model.LoginAttemptCount;
                _context.UserLockout.Update(userLockout);
                await _context.SaveChangesAsync();
            }
            else
            {
                model.Id = Guid.NewGuid();
                _context.UserLockout.Add(model);
                await _context.SaveChangesAsync();
            }

            return ManagerResponse.Response("Successfully Updated", true, new List<string> { "User Lockout Updated Successfully" }, new List<object> { model });
        }
    }
}
