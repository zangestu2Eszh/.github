using IraqWebsite.Areas.Identity.Pages.Account;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace IraqWebsite.Helper
{
    public class AuthLog : IPageFilter
    {
        private readonly ApplicationDbContext _context;

        public AuthLog(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            var x = context.ModelState.Values;

            if (context.HandlerMethod?.MethodInfo.Name == "OnPostAsync" && context.HandlerInstance is PageModel pageModel && pageModel is LoginModel loginModel)
            {
                var result = loginModel.Result;
                var email = loginModel.Input.Email ?? "Unkown";
                UserActivityLog audit = new UserActivityLog()
                {
                    Id = Guid.NewGuid(),
                    UserName = email + " Has Made " + loginModel.ErrorMessage,
                    IPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                    Browser = GetUserBrowser(context.HttpContext.Request.Headers["User-Agent"].ToString()),
                    Platform = RuntimeInformation.OSDescription,
                    AreaAccessed = "/Identity/Account/Login",
                    Timestamp = DateTime.Now,
                    Status = false
                };

                if (result == true)
                {
                    audit.Status = true;
                    audit.UserName = loginModel.Input.Email;
                }
                _context.UserActivityLog.Add(audit);
                _context.SaveChanges();
            }
        }

        public string GetUserBrowser(string request)
        {
            if (request.Contains("Edg"))
                return "Microsoft Edge";

            if (request.Contains("Firefox"))
                return "Mozila Firefox";

            if (request.Contains("Opera"))
                return "Opera";

            if (request.Contains("Brave"))
                return "Brave";

            if (request.Contains("(iPhone; CPU iPhone"))
                return "Safari";

            if (request.Contains("Chrome"))
                return "Google Chrome";

            return request;
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new NotImplementedException();
        }

        public static bool status()
        {
            return true;
        }
    }
}
