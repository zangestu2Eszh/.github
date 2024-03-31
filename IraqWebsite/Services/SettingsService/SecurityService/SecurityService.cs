using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Response;
using IraqWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Services.SettingsService.SecurityService
{
    public class SecurityService : ISecurityService
    {
        private readonly ApplicationDbContext _context;

        public SecurityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PasswordComplexityApp> GetPasswordComplexity()
        {
            var complexity = await _context.PasswordComplexityApp.FirstOrDefaultAsync();
            if (complexity is null)
            {
                return null;
            }
            return complexity;
        }

        public async Task<ManagerResponse> UpdatePasswordComplexity(PasswordComplexityApp model)
        {
            if (model is null)
            {
                return ManagerResponse.Response("Invalid Values", false, new List<string> { "No Data Found" }, new List<object> { model });
            }

            var complexity = await _context.PasswordComplexityApp.FirstOrDefaultAsync();
            if (complexity is not null)
            {
                complexity.RequireDigit = model.RequireDigit;
                complexity.RequiredLength = model.RequiredLength;
                complexity.RequireLowercase = model.RequireLowercase;
                complexity.RequireUppercase = model.RequireUppercase;
                complexity.Require_non_alphanumeric = model.Require_non_alphanumeric;
                complexity.UseDefaultSettings = model.UseDefaultSettings;
                _context.PasswordComplexityApp.Update(complexity);
                await _context.SaveChangesAsync();
                return ManagerResponse.Response("Successfully Updated", true, new List<string> { "Password Complexity Updated Successfully" }, new List<object> { model });
            }
            else
            {
                model.Id = Guid.NewGuid();
                _context.PasswordComplexityApp.Add(model);
                await _context.SaveChangesAsync();
                return ManagerResponse.Response("Successfully Created", true, new List<string>(), new List<object> { model });
            }
        }
    }
}
