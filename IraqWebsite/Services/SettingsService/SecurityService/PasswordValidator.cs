using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Services.SettingsService.SecurityService
{
    public class PasswordValidator : AbstractValidator<PasswordViewModel>
    {
        private readonly ApplicationDbContext _context;

        public PasswordValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Password).Password(_context);
        }
    }

    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, ApplicationDbContext context)
        {
            var complexity = context.PasswordComplexityApp.FirstOrDefaultAsync().Result;
            if (!context.PasswordComplexityApp.Any() || complexity.UseDefaultSettings is true)
            {
                var deffultsOptions = ruleBuilder
                          .NotEmpty()
                          .NotNull()
                          .MinimumLength(8)
                          .MaximumLength(16)
                          .Matches("[a-z]").WithMessage("At least one Lowercase character")
                          .Matches("[A-Z]").WithMessage("At least one Uppercase character")
                          .Matches("[0-9]").WithMessage("At least one digit")
                          .Matches("(?=.*?[#?!@$%^&*-])").WithMessage("At least one special character");

                return deffultsOptions;
            }


            var options = ruleBuilder.NotEmpty().NotNull().MinimumLength(complexity.RequiredLength).MaximumLength(16);

            if (complexity.RequireLowercase == true)
            {
                options.Matches("[a-z]").WithMessage("At least one Lowercase character");
            }

            if (complexity.RequireUppercase == true)
            {
                options.Matches("[A-Z]").WithMessage("At least one Uppercase character");
            }

            if (complexity.RequireLowercase == true)
            {
                options.Matches("[0-9]").WithMessage("At least one digit");
            }

            if (complexity.Require_non_alphanumeric == true)
            {
                options.Matches("(?=.*?[#?!@$%^&*-])").WithMessage("At least one special character");
            }
            return options;
        }
    }
}
