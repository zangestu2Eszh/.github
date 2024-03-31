// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IraqWebsite.AuthManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Data;
using IraqWebsite.Services.GoogleReCaptchaService;
using IraqWebsite.Helper;

namespace IraqWebsite.Areas.Identity.Pages.Account
{
    [ServiceFilter(typeof(AuthLog))]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginModel> _logger;
        private readonly IGoogleRecaptchaService _captchaService;
        private readonly ActivitiesLog _activitiesLog;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager
            ,IGoogleRecaptchaService captchaService, ActivitiesLog activitiesLog)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _captchaService = captchaService;
            _activitiesLog = activitiesLog;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }
        public string SiteKey { get; set; }
        public bool Result { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            public string Token { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");
            

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            SiteKey = await _context.GoogleRecaptcha.Select(x => x.SiteKey).FirstOrDefaultAsync();

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var captchaResult = await _captchaService.VerfiyToken(Input.Token);
            if (!captchaResult)
            {
                ErrorMessage = "Google ReCaptcha Is Faild";
                return RedirectToPage("./Login", returnUrl);
            }

            returnUrl ??= Url.Content("~/");
            var adminUrl = Url.Content("~/admin");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _context.Users.Where(x => x.Email == Input.Email).FirstOrDefaultAsync();

                if(user == null)
                {
                    ErrorMessage = "Account Not Found";
                    return RedirectToPage("./Login", returnUrl);
                }
                    
                if (user.IsActive == false)
                {
                    ErrorMessage = "Admin not accepted your account yet.";
                    return RedirectToPage("./Login", returnUrl);
                }

                var userLockout = await _context.UserLockout.Where(x => x.AccountLocking == true).FirstOrDefaultAsync();

                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: userLockout.AccountLocking);


                if (result.Succeeded)
                {
                    Result = true;
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(adminUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", returnUrl);
                }

                if (result.IsNotAllowed)
                {
                    _logger.LogWarning("User account not activated.");
                    ErrorMessage = "account not activated.";
                    return RedirectToPage("./Login", returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    if (userLockout is not null && user is not null)
                    {
                        if (userLockout.LoginAttemptCount == user.AccessFailedCount)
                        {
                            user.LockoutEnd = DateTime.Now.AddSeconds(userLockout.AccountLockingDuration);
                            await _userManager.SetLockoutEnabledAsync(user, true);
                        }
                    }

                    ErrorMessage = "Invalid login attempt.";
                    return RedirectToPage("./Login", returnUrl);
                }
            }
            ErrorMessage = "Please Enter Valid Email and Password";
            return RedirectToPage("./Login", returnUrl);
        }
    }
}
