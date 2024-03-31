using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Seeds;
using IraqWebsite.AuthManager.Statics;
using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using static IraqWebsite.AuthManager.Statics.Permissions;
using Microsoft.AspNetCore.Mvc;
using IraqWebsite.AuthManager.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using IraqWebsite.Services.EmailService;

namespace IraqWebsite.Services.UserService
{
    public class UserService : IUserService
    {
        #region Feilds

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public string? Error { get; set; }

        #endregion

        #region Constructor

        public UserService(
            SignInManager<ApplicationUser> signInManager,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userStore = userStore;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _roleManager = roleManager;
            _context = context;
        }

        #endregion

        #region UserManagment

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<List<ListUsersViewModel>?> ConfirmAccount(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user is null)
                {
                    return null;
                }

                if (user.IsActive is true)
                {
                    user.IsActive = false;
                    user.EmailConfirmed = user.IsActive;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    user.IsActive = true;
                    user.EmailConfirmed = user.IsActive;
                    await _userManager.UpdateAsync(user);
                }
                return await GetUsers();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ListUsersViewModel>> GetUsers()
        {
            try
            {
                List<ListUsersViewModel> listUsersViewModels = new List<ListUsersViewModel>();

                var UsersWithOutPassword = await _userManager.Users.Where(x => x.Email.ToLower() != "software@tm.iq").Where(x => x.IsNewUser == false).Select(c =>
                           new
                           {
                               c.Id,
                               c.FirstName,
                               c.LastName,
                               c.IpPhone,
                               c.Company,
                               c.IsActive,
                               c.CreatedDate,
                               c.ProfilePicture,
                               c.Email,
                               c.PhoneNumber,
                               c.PhoneNumberConfirmed,
                               c.Gender,
                               c.Birthday,
                               c.LastModification
                           }).ToListAsync();

                foreach (var userWithOutPassword in UsersWithOutPassword)
                {
                    var user = await _userManager.FindByEmailAsync(userWithOutPassword.Email);
                    var userRoles = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                    var RoleClaims = await GetRoleClaim(userRoles);

                    listUsersViewModels.Add(new ListUsersViewModel
                    {
                        UserId = userWithOutPassword.Id,
                        FirstName = userWithOutPassword.FirstName,
                        LastName = userWithOutPassword.LastName,
                        IsActive = userWithOutPassword.IsActive,
                        CreatedDate = userWithOutPassword.CreatedDate,
                        ProfilePicture = userWithOutPassword.ProfilePicture,
                        Email = userWithOutPassword.Email,
                        Roles = userRoles,
                        RoleClaims = RoleClaims
                    });
                }

                return listUsersViewModels;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        // User deletion succeeded
                        return;
                    }
                    else
                    {
                        // User deletion failed
                        throw new Exception("Failed to delete user.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CreateUserViewModel?> CreateUser(CreateUserViewModel model)
        {
            try
            {
                var applicationUser = await _userManager.FindByEmailAsync(model.RigsterViewModel.Email);
                if (applicationUser is not null)
                {
                    Error = "الحساب قيد الاستخدام";
                    return null;
                }
                var user = Activator.CreateInstance<ApplicationUser>();

                user.FirstName = model.RigsterViewModel.FirstName;
                user.LastName = model.RigsterViewModel.LastName;
                user.CreatedDate = DateTime.Now;
                user.ProfilePicture = "Images/DeffultUser.png";
                user.Email = model.RigsterViewModel.Email;
                user.UserName = model.RigsterViewModel.Email;
                user.EmailConfirmed = model.Active;
                user.IsActive = model.Active;
                user.IpPhone = model.RigsterViewModel.IpPhone;
                user.PhoneNumber = model.RigsterViewModel.Phone;
                user.Company = model.RigsterViewModel.Company;
                user.Birthday = model.RigsterViewModel.Birthday;
                user.Gender = model.RigsterViewModel.Gender;

                var roles = GetRoles().Result.Where(x => x.Id == model.Role.ToString()).FirstOrDefault();
                if (roles is null)
                {
                    Error = "الرجاء اختيار الدور";
                    return model;
                }

                var result = await _userManager.CreateAsync(user, model.RigsterViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, roles.Name);
                    user.EmailConfirmed = true;
                    return model;
                }
                Error = "حدث خطأ, تأكد من الرمز السري";
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ManagerResponse> UpdateUserRole(string userId, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            //if (role is null || role.Name == "SuperAdmin")
            //{
            //    Error = "The Role Not Founded";
            //    return null;
            //}
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null || user.Email.ToLower() == "software@tm.iq")
            {
                Error = "The User Not Founded";
                return null;
            }
            string? userRole = GetUserRoles(userId).Result.UserRoles.Where(x => x.Selected == true)?.Select(x => x.RoleName).FirstOrDefault()?.ToString();
            var resultRole = await _userManager.RemoveFromRoleAsync(user, userRole);
            resultRole = await _userManager.AddToRoleAsync(user, role.Name);

            return null;
        }

        public async Task<ApplicationUser?> EditUser(CreateUserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.RigsterViewModel.Email);
                if (user is null || user.Email.ToLower() == "software@tm.iq")
                {
                    Error = "الحساب غير موجود";
                    return null;
                }

                user.FirstName = model.RigsterViewModel.FirstName;
                user.LastName = model.RigsterViewModel.LastName;
                user.PhoneNumber = model.RigsterViewModel.Phone;
                user.IpPhone = model.RigsterViewModel.IpPhone;
                user.Gender = model.RigsterViewModel.Gender;
                user.Company = model.RigsterViewModel.Company;
                user.Birthday = model.RigsterViewModel.Birthday;
                user.LastModification = DateTime.Now.ToString();

                if (user.Email != model.RigsterViewModel.Email)
                {
                    var users = await _userManager.Users.Where(x => x.Email == model.RigsterViewModel.Email).ToListAsync();

                    if (users.Count > 0)
                    {
                        Error = "الحساب موجود";
                        return null;
                    }
                }
                user.Email = model.RigsterViewModel.Email;

                if (!string.IsNullOrEmpty(model.RigsterViewModel.Password) && !string.IsNullOrEmpty(model.RigsterViewModel.ConfirmPassword))
                {
                    if (model.RigsterViewModel.Password != model.RigsterViewModel.ConfirmPassword)
                    {
                        Error = "الرمز غير مطابق";
                        return null;
                    }

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resultPass = await _userManager.ResetPasswordAsync(user, token, model.RigsterViewModel.Password);

                    if (resultPass.Errors.Any())
                    {
                        Error = "Failld Update Password";
                        return null;
                    }
                }


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return null;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EmailSettings>?> AddEmailSettings(EmailSettings model)
        {
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.SmtpClient))
            {
                var settings = await _context.EmailSettings.Where(x => x.IsActive == true).FirstOrDefaultAsync();
                if (settings is not null && model.IsActive == true)
                {
                    settings.IsActive = false;
                }

                var checkEmail = await _context.EmailSettings.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
                if (checkEmail is not null)
                {
                    Error = "الحساب موجود مسبقاّ";
                    return null;
                }

                byte[] encData_byte = new byte[model.Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(model.Password);
                string encodedData = Convert.ToBase64String(encData_byte);


                model.Password = encodedData;
                model.Id = Guid.NewGuid();
                model.LastModification = DateTime.Now.ToString();
                _context.EmailSettings.Add(model);
                await _context.SaveChangesAsync();
                return await GetEmailSettings();
            }
            Error = "حدث خطأ تأكد من الادخال";
            return null;
        }

        public async Task<List<EmailSettings>?> GetEmailSettings()
        {
            return await _context.EmailSettings.ToListAsync();
        }

        public async Task<EmailSettings?> DetailsEmailSettings(Guid? id)
        {
            try
            {
                if (id is not null)
                {
                    var settings = await _context.EmailSettings.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (settings is not null)
                    {
                        return settings;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EmailSettings>?> EditEmailSettings(Guid? id, EmailSettings model)
        {
            try
            {
                if (id != model.Id)
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.SmtpClient))
                {
                    var checkActive = await _context.EmailSettings.Where(x => x.IsActive == true).FirstOrDefaultAsync();
                    if (checkActive is not null && model.IsActive == true && model.Id != checkActive.Id)
                    {
                        checkActive.IsActive = false;
                    }

                    var settings = await _context.EmailSettings.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (settings is null)
                    {
                        return null;
                    }

                    settings.LastModification = DateTime.Now.ToString();
                    settings.Email = model.Email;
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        settings.Password = model.Password;
                    }
                    settings.SmtpClient = model.SmtpClient;
                    settings.Port = model.Port;
                    settings.IsActive = model.IsActive;
                    settings.EnableSSl = model.EnableSSl;

                    _context.EmailSettings.Update(settings);
                    await _context.SaveChangesAsync();

                    return await GetEmailSettings();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EmailSettings>?> RemoveEmailSettings(Guid? id)
        {
            var setting = await _context.EmailSettings.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (setting is not null)
            {
                if (setting.IsActive == true)
                {
                    Error = "لايمكن حذف حساب مفعل";
                    return null;
                }
                _context.EmailSettings.Remove(setting);
                await _context.SaveChangesAsync();
                return await GetEmailSettings();
            }
            return null;
        }

        public async Task<ManagerResponse> SaveUserImage(string email, IFormFile file)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user is null)
                {
                    return ManagerResponse.Response("Invalid Value", false, new List<string> { "Account Not Found" }, new List<object> { });
                }

                if (file != null)
                {
                    string imgext = Path.GetExtension(file.FileName);
                    if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg")
                    {
                        if (file.Length > 0)
                        {
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\", file.FileName);

                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await file.CopyToAsync(stream);
                            }
                            user.ProfilePicture = Path.Combine("/Images/" + file.FileName);
                            await _userManager.UpdateAsync(user);

                            return ManagerResponse.Response("Image Updated Successfully", true, new List<string> { }, new List<object> { });
                        }
                    }
                    return ManagerResponse.Response("حدث خطأ", false, new List<string> { "Worng Data Type" }, new List<object> { });
                }
                return ManagerResponse.Response("حدث خطأ", false, new List<string> { "Choose Image First" }, new List<object> { });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<UserActivityLog>> SystemLog()
        {
            return await _context.UserActivityLog
            .Where(x => !x.AreaAccessed.Contains("Login"))
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
        }

        public async Task<List<UserActivityLog>> AuthLog()
        {
            return await _context.UserActivityLog
           .Where(x => x.AreaAccessed.Contains("Login"))
           .OrderByDescending(x => x.Timestamp)
           .ToListAsync();
        }

        public async Task DeleteSystemLog()
        {
            var log = await _context.UserActivityLog.Where(x => !x.AreaAccessed.Contains("Login")).ToListAsync();
            _context.RemoveRange(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthLog()
        {
            var log = await _context.UserActivityLog.Where(x => x.AreaAccessed.Contains("Login")).ToListAsync();
            _context.RemoveRange(log);
            await _context.SaveChangesAsync();
        }


        #endregion

        #region RoleAndPermissionManagment

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<PermissionViewModel?> GetRoleClaim(string roleName)
        {
            try
            {
                if (roleName is null)
                {
                    return null;
                }

                var role = await _roleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    return null;
                }


                var model = new PermissionViewModel();
                var allPermissions = new List<RoleClaimViewModel>();

                allPermissions.GetPermission(typeof(Permissions.Dashboard), role.Id);
                allPermissions.GetPermission(typeof(Permissions.UserPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.RolePolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.EmailSettingPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.PsaawordComplexity), role.Id);
                allPermissions.GetPermission(typeof(Permissions.Appearance), role.Id);
                allPermissions.GetPermission(typeof(Permissions.UserManag), role.Id);
                allPermissions.GetPermission(typeof(Permissions.UserLock), role.Id);
                allPermissions.GetPermission(typeof(Permissions.SliderPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.ScheduleSectionPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.ScheduleSubSectionPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.AboutPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.PortfolioPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.EventPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.NewsPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.PartnerPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.ContactPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.FmLinkPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.LogPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.MetaPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.RecaptchaPolicy), role.Id);
                allPermissions.GetPermission(typeof(Permissions.SubscribersPolicy), role.Id);

                model.RoleId = role.Id;

                var claims = await _roleManager.GetClaimsAsync(role);

                var allClaimValues = allPermissions.Select(a => a.Value).ToList();
                var roleClaimValues = claims.Select(a => a.Value).ToList();
                var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();

                foreach (var permission in allPermissions)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;
                    }
                }

                model.RoleClaims = allPermissions;
                model.RoleName = roleName;
                return model;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IdentityRole> GetRoleByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Error = "User Not Founded";
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (role == null)
            {
                Error = "role Not Founded";
                return null;
            }
            var roleDto = await _roleManager.FindByNameAsync(role);
            return roleDto;
        }

        public async Task<List<IdentityRole>?> EditRole(PermissionViewModel model)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role is null)
                {
                    return null;
                }
                var claims = await _roleManager.GetClaimsAsync(role);

                foreach (var claim in claims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();

                foreach (var claim in selectedClaims)
                {
                    await _roleManager.AddPermissionClaim(role, claim.Value);
                }

                if (role.Name != model.RoleName)
                {
                    var roles = await _roleManager.Roles.Where(x => x.Name == model.RoleName).ToListAsync();

                    if (roles.Count > 0)
                    {
                        return null;
                    }
                }
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return null;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<IdentityRole>?> AddRole(PermissionViewModel model)
        {
            try
            {
                if (model.RoleName is not null)
                {
                    var role = await _roleManager.FindByNameAsync(model.RoleName);
                    if (role is not null)
                    {
                        return null;
                    }
                    model.RoleId = "";
                    model.RoleId = Guid.NewGuid().ToString();
                    await _roleManager.CreateAsync(new IdentityRole(model.RoleName.Trim()));


                    var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();

                    foreach (var claim in selectedClaims)
                    {
                        await _roleManager.AddPermissionClaim(await _roleManager.FindByNameAsync(model.RoleName), claim.Value);
                    }

                    return await GetRoles();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityRole?> GetRoleByName(string roleName)
        {
            try
            {
                if (roleName is not null)
                {
                    return await _roleManager.Roles.Where(x => x.Name == roleName).FirstOrDefaultAsync();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<IdentityRole>?> RemoveRole(string roleName)
        {
            try
            {
                var role = await GetRoleByName(roleName);

                if (role is null)
                {
                    Error = "لا يمكن اجراء عملية";
                    return null;
                }

                var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

                if (usersOfRole.Any())
                {
                    Error = "لا يمكن اجراء عملية الحذف على دور قيد الاستخدام";
                    return null;
                }

                if (role.Name != "SuperAdmin" && role.Name != "User")
                {
                    await _roleManager.DeleteAsync(role);
                    return await GetRoles();
                }

                Error = "لا يمكن اجراء عملية الحذف على دور قيد الاستخدام";
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ManageUserRoleViewModel> GetUserRoles(string userId)
        {
            try
            {
                var userRolesVM = new List<UserRolesViewModel>();
                var user = await _userManager.FindByIdAsync(userId);

                foreach (var role in _roleManager.Roles)
                {
                    var userRoleVM = new UserRolesViewModel()
                    {
                        RoleName = role.Name,
                    };

                    if (await _userManager.IsInRoleAsync(user, userRoleVM.RoleName))
                    {
                        userRoleVM.Selected = true;
                    }
                    else
                    {
                        userRoleVM.Selected = false;
                    }

                    userRolesVM.Add(userRoleVM);
                }

                var model = new ManageUserRoleViewModel()
                {
                    UserId = user.Id,
                    UserRoles = userRolesVM
                };
                return model;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public PermissionViewModel GetAllPermission()
        {
            return new PermissionViewModel { RoleId = null, RoleClaims = AllClaims() };
        }

        //this function built with custome need of if condition in filter service thats need false only if entered non exsist role or not joined with user
        public async Task<bool> CustemIsInRoleAsync(ApplicationUser user, string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                return await _userManager.IsInRoleAsync(user, roleName.ToUpper());
            }
            return true;
        }


        public async Task<ManagerResponse> Filter(UserFilterViewModel viewModel)
        {
            List<UserFilterViewModel> userFilters = new();

            foreach (var user in await _context.Users.ToListAsync())
            {
                string firestName = user.FirstName ?? "";
                string lastName = user.LastName ?? "";
                string email = user.Email ?? "";
                string phoneNumber = user.PhoneNumber ?? "";
                string ipPhone = user.IpPhone ?? "";
                string company = user.Company ?? "";

                string roleId = await _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).FirstOrDefaultAsync() ?? "";
                string roleName = await _roleManager.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync() ?? "";

                if (firestName.Contains(viewModel.FirstName ?? "") && lastName.Contains(viewModel.LastName ?? "")
                    && email.Contains(viewModel.Email ?? "") && phoneNumber.Contains(viewModel.PhoneNumber ?? "")
                    && ipPhone.Contains(viewModel.IpPhone ?? "") && company.Contains(viewModel.Company ?? "")
                    && user.IsActive == viewModel.IsActive && await CustemIsInRoleAsync(user, viewModel.RoleName ?? ""))
                {
                    userFilters.Add(new UserFilterViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        IpPhone = user.IpPhone,
                        Company = user.Company,
                        IsActive = user.IsActive,
                        RoleName = roleName,
                    });
                }
            }

            if (userFilters.Any())
            {
                return ManagerResponse.Response("Successfully Found", true, new List<string>(), new List<object> { userFilters });
            }
            return ManagerResponse.Response("Not Found", false, new List<string>(), new List<object>());
        }

        public async Task<string?> GetCurrentUserId(ClaimsPrincipal currentUser)
        {
            try
            {
                if (currentUser.Claims.Any())
                {
                    var userId = currentUser.Claims.ToList()[1].Value.ToString();
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return null;
                    }
                    return user.Id;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ManagerResponse> GetRecentUsers()
        {
            try
            {
                var applicationUsers = new List<object>();

                var UsersWithOutPassword = await _userManager.Users.Where(x => x.IsNewUser == true).Select(c =>
                           new
                           {
                               c.Id,
                               c.FirstName,
                               c.LastName,
                               c.IpPhone,
                               c.Company,
                               c.IsActive,
                               c.CreatedDate,
                               c.ProfilePicture,
                               c.Email,
                               c.PhoneNumber,
                               c.PhoneNumberConfirmed,
                               c.Gender,
                               c.Birthday,
                               c.LastModification,
                               c.IsNewUser
                           }).ToListAsync();

                foreach (var userWithOutPassword in UsersWithOutPassword)
                {
                    var user = await _userManager.FindByEmailAsync(userWithOutPassword.Email);
                    var userRoles = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                    var RoleClaims = await GetRoleClaim(userRoles);


                    applicationUsers.Add(new List<object>
                    {
                        userWithOutPassword,
                        userRoles ,
                        RoleClaims
                    });
                }

                return ManagerResponse.Response("Successfully Sent", true, new List<string>(), applicationUsers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public async Task<bool> ForgetPassword(string email, string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return false;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                //string url = $"http://10.10.14.200:9010/Account/ResetPassword?email={email}&token={validToken}";

                await _emailService.CustemSendEmailAsync(
                    email,
                    "Reset Password",
                    $"Follow the instructions to reset your password",
                    url,
                    "Reset Password");

                //await _iemailSender.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                //    $"<p>To reset your password <a href='{url}'>Click here</a></p>");

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
