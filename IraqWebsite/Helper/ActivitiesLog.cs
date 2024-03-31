using IraqWebsite.AuthManager.Models;
using IraqWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace IraqWebsite.Helper
{
    public class ActivitiesLog : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesLog(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                string userName = "";
                if(context.HttpContext.User.Claims.ToList().Count >= 2)
                {
                    userName = context.HttpContext.User.Claims.ToList()[2].Value;
                }
                else
                {
                    userName = "Visitor";
                }

                UserActivityLog audit = new UserActivityLog()
                {
                    Id = Guid.NewGuid(),
                    UserName = userName,
                    IPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                    Browser = GetUserBrowser(context.HttpContext.Request.Headers["User-Agent"].ToString()),
                    Platform = RuntimeInformation.OSDescription,
                    AreaAccessed = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName + "/" + ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName,
                    Timestamp = DateTime.Now
                };

                var result = context.Result;
                if (result is JsonResult json)
                {
                    var val = JsonConvert.SerializeObject(json.Value);
                    if (val.Contains("\"IsSuccess\":true"))
                    {
                        audit.Status = true;
                    }
                    else
                    {
                        audit.Status = false;
                    }
                }
                if (result is OkObjectResult objectResult)
                {
                    var val = JsonConvert.SerializeObject(objectResult.Value);
                    if (val.Contains("\"IsSuccess\":true"))
                    {
                        audit.Status = true;
                    }
                    else
                    {
                        audit.Status = false;
                    }
                }
                _context.UserActivityLog.Add(audit);
                _context.SaveChanges();
                base.OnActionExecuted(context);
            }
            catch (Exception)
            {
                throw;
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
    }
}
