using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static IraqWebsite.AuthManager.Statics.Permissions;

namespace IraqWebsite.Controllers
{
    [Authorize]

    public class RoleManagerController : Controller
    {
        #region Feilds
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _IuserService;
        #endregion

        #region Constrector
        public RoleManagerController(RoleManager<IdentityRole> roleManager,IUserService iuserService)
        {
            _roleManager = roleManager;
            _IuserService = iuserService;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _IuserService.GetRoles());
        }
        #endregion

        #region Crud Opreation
        [HttpGet]
        public IActionResult Create()
        {
            return View(_IuserService.GetAllPermission());
        }

        [Authorize(Policy = RolePolicy.Create)]
        [HttpPost]
        public async Task<ActionResult> Create(PermissionViewModel model)
        {
            await _IuserService.AddRole(model);
            return RedirectToAction("Index");
        }

        [Authorize(Policy = RolePolicy.Edit)]
        [HttpGet]
        public async Task<IActionResult> Edit(string Name)
        {
            return View(await _IuserService.GetRoleClaim(Name));
        }

        [Authorize(Policy = RolePolicy.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(PermissionViewModel model)
        {
            await _IuserService.EditRole(model);
            return RedirectToAction("Index");
        }

        [Authorize(Policy = RolePolicy.Delete)]
        [HttpGet]
        public async Task<ActionResult> Delete(string Name)
        {
            return View(await _IuserService.GetRoleByName(Name));
        }

        [Authorize(Policy = RolePolicy.Delete)]
        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
            await _IuserService.RemoveRole(model.Name);
            if (!string.IsNullOrEmpty(_IuserService.Error))
            {
                @TempData["Message"] = _IuserService.Error;
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
