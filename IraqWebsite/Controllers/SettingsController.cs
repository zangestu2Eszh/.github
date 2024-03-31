using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Statics;
using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.Helper;
using IraqWebsite.Services.SettingsService.AppearanceService;
using IraqWebsite.Services.SettingsService.SecurityService;
using IraqWebsite.Services.SettingsService.UserManagmentService;
using IraqWebsite.Services.UserService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IraqWebsite.AuthManager.Statics.Permissions;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class SettingsController : Controller
    {
        private readonly ISecurityService _isecurityService;
        private readonly IAppearanceService _iappearanceService;
        private readonly IUserManagmentService _iuserManagmentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SettingsController(ISecurityService isecurityService, IAppearanceService iappearanceService, IUserManagmentService iuserManagmentService,IUserService userService,IMapper mapper)
        {
            _isecurityService = isecurityService;
            _iappearanceService = iappearanceService;
            _iuserManagmentService = iuserManagmentService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = PsaawordComplexity.Edit)]
        public async Task<IActionResult> PasswordComplexity()
        {
            return View(await _isecurityService.GetPasswordComplexity());
        }

        [HttpPost]
        [Authorize(Policy = PsaawordComplexity.Edit)]
        public async Task<ActionResult> PasswordComplexity([FromForm] PasswordComplexityApp model)
        {
            await _isecurityService.UpdatePasswordComplexity(model);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Appearance()
        {
            var apperance = await _iappearanceService.GetAppearance();
            ViewBag.Icon = apperance.FavIcon;
            ViewBag.Logo = apperance.ApplicationLogo;
            return View(_mapper.Map<AppearanceViewModel>(apperance));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Appearance.Edit)]
        public async Task<IActionResult> Appearance([FromForm] AppearanceViewModel model)
        {
            await _iappearanceService.UpdateAppearance(model);
            return RedirectToAction(nameof(Appearance));
        }

        [HttpGet]
        [Authorize(Policy = UserManag.View)]
        public async Task<ActionResult> UserManagment()
        {
            return View(await _iuserManagmentService.GetUserManagment());
        }

        [HttpPost]
        [Authorize(Policy = UserManag.Edit)]
        public async Task<ActionResult> UserManagment([FromForm] UserManagement model)
        {
            await _iuserManagmentService.UpdateUserManagement(model);
            return RedirectToAction(nameof(UserManagment));
        }

        [HttpGet]
        [Authorize(Policy = UserLock.View)]
        public async Task<ActionResult> GetUserLockout()
        {
            return Ok(await _iuserManagmentService.GetUserLockout());
        }

        [HttpPost]
        [Authorize(Policy = UserLock.Edit)]
        public async Task<ActionResult> UpdateUserLocckoutt([FromBody] UserLockout model)
        {
            return Ok(await _iuserManagmentService.UpdateUserLockout(model));
        }

        [HttpGet]
        [Authorize(Policy = EmailSettingPolicy.Create)]
        public IActionResult AddEmailSetting()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = EmailSettingPolicy.Create)]
        public async Task<ActionResult> AddEmailSetting(EmailSettings model)
        {
            await _userService.AddEmailSettings(model);
            return RedirectToAction(nameof(EmailSettings));
        }

        [HttpGet]
        [Authorize(Policy = EmailSettingPolicy.Edit)]
        public async Task<ActionResult> EditEmailSetting(Guid? Id)
        {
            return View(await _userService.DetailsEmailSettings(Id));
        }

        [HttpPost]
        [Authorize(Policy = EmailSettingPolicy.Edit)]
        public async Task<ActionResult> EditEmailSetting(EmailSettings model)
        {
            await _userService.EditEmailSettings(model.Id, model);
            return RedirectToAction(nameof(EmailSettings));
        }

        [HttpPost]
        [Authorize(Policy = EmailSettingPolicy.Delete)]
        public async Task<ActionResult> RemoveEmailSettings(Guid? Id)
        {
            await _userService.RemoveEmailSettings(Id);
            return RedirectToAction(nameof(EmailSettings));
        }

        [HttpGet]
        [Authorize(Policy = EmailSettingPolicy.View)]
        public async Task<ActionResult> EmailSettings()
        {
            return View(await _userService.GetEmailSettings());
        }
    }
}
