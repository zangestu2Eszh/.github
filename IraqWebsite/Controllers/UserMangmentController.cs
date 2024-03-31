using IraqWebsite.Enums;
using IraqWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.Services.UserService;
using IraqWebsite.AuthManager.ViewModels;
using static IraqWebsite.AuthManager.Statics.Permissions;
using Microsoft.AspNetCore.Mvc.Rendering;
using IraqWebsite.Helper;
using AutoMapper;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    [Authorize]
    public class UserMangmentController : Controller
    {
        #region Feilds
        private readonly IUserService _IuserService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Constrector
        public UserMangmentController(IUserService userService,IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _IuserService = userService;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        #endregion

        #region Crud

        [Authorize(Policy = UserPolicy.View)]
        [HttpGet]
        public async Task<ActionResult> index()
        {
            var users = await _IuserService.GetUsers();
            foreach (var user in users)
            {
                var role = await _IuserService.GetRoleByUserId(user.UserId);
                ViewData["Roles"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name", role.Id);
            }
            return View(users);
        }

        [Authorize(Policy = UserPolicy.Create)]
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            if (User.Identity.Name != "software@Tm.Iq")
            {
                ViewData["Roles"] = new SelectList(_IuserService.GetRoles().Result.Where(x => x.Name != "SuperAdmin"), "Id", "Name");
            }
            else
            {
                ViewData["Roles"] = new SelectList(await _IuserService.GetRoles(), "Id", "Name");
            }
            return View();
        }

        [Authorize(Policy = UserPolicy.Create)]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var x = await _IuserService.CreateUser(model);
                if (!string.IsNullOrEmpty(_IuserService.Error))
                {
                    @TempData["Message"] = _IuserService.Error;
                    ViewData["Roles"] = new SelectList(_IuserService.GetRoles().Result.Where(x => x.Name != "SuperAdmin"), "Id", "Name");
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View (model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmAccount(string UserId)
        {
            return Ok(await _IuserService.ConfirmAccount(UserId));
        }

        [Authorize(Policy = UserPolicy.View)]
        [HttpGet]
        public async Task<ActionResult> GetRecentUsers()
        {
            return View(await _IuserService.GetRecentUsers());
        }

        [Authorize(Policy = UserPolicy.Delete)]
        [HttpGet]
        public async Task<ActionResult> DeleteUser(string userId, bool DeleteUser)
        {
            if (DeleteUser == true)
            {
                await _IuserService.DeleteUser(userId);
            }
            return RedirectToAction(nameof(index));
        }

        [Authorize(Policy = UserPolicy.Edit)]
        [HttpGet]
        public async Task<IActionResult> EditUser(string UserId)
        {
            var user = await _IuserService.GetUserById(UserId);
            RigsterViewModel rigsterVm = new RigsterViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Company = user.Company,
                Phone = user.PhoneNumber,
                IpPhone = user.IpPhone,
                Birthday = user.Birthday,
                Gender = user.Gender,
            };
            CreateUserViewModel userVm = new CreateUserViewModel
            {
                RigsterViewModel = rigsterVm,
                Active = user.IsActive,
            };
            return View(userVm);
        }

        [Authorize(Policy = UserPolicy.Edit)]
        [HttpPost]
        public async Task<IActionResult> EditUser(CreateUserViewModel model)
        {
            await _IuserService.EditUser(model);
            if (!string.IsNullOrEmpty(_IuserService.Error))
            {
                @TempData["Message"] = _IuserService.Error;
                return View(model);
            }
            return RedirectToAction(nameof(index));
        }

        [Authorize(Policy = RolePolicy.Edit)]
        [HttpPost]
        public async Task<ActionResult> UpdateUserRole(string userId,string roleName)
        {
            await _IuserService.UpdateUserRole(userId,roleName);
            return RedirectToAction(nameof(index));
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<ActionResult> Filter(UserFilterViewModel viewModel)
        //{
        //    return Ok(await _IuserService.Filter(viewModel));
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<ActionResult> SaveUserImage(IFormFile file)
        //{
        //    return Ok(await _IuserService.SaveUserImage(User.Claims.ToList()[3].ToString().Replace("Email: ", ""), file));
        //}

        [Authorize(Policy = LogPolicy.View)]
        [HttpGet]
        public async Task<IActionResult> SystemLog()
        {
            return View(await _IuserService.SystemLog());
        }

        [Authorize(Policy = LogPolicy.Delete)]
        [HttpGet]
        public async Task<IActionResult> DeleteSystemLog()
        {
            await _IuserService.DeleteSystemLog();
            return RedirectToAction(nameof(SystemLog));
        }
        
        [Authorize(Policy = LogPolicy.View)]
        [HttpGet]
        public async Task<IActionResult> AuthLog()
        {
            return View(await _IuserService.AuthLog());
        }

        [Authorize(Policy = LogPolicy.Delete)]
        [HttpGet]
        public async Task<IActionResult> DeleteAuthLog()
        {
            await _IuserService.DeleteAuthLog();
            return RedirectToAction(nameof(AuthLog));
        }

        #endregion
    }
}
