using IraqWebsite.AuthManager.Models;
using IraqWebsite.AuthManager.Permission;
using IraqWebsite.AuthManager.Seeds;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Services.EmailService;
using IraqWebsite.Services.SettingsService.AppearanceService;
using IraqWebsite.Services.SettingsService.SecurityService;
using IraqWebsite.Services.SettingsService.UserManagmentService;
using IraqWebsite.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Services.AttachmentService;
using IraqWebsite.Services.GoogleReCaptchaService;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ISecurityService, SecurityService>();
builder.Services.AddTransient<IAppearanceService, AppearanceService>();
builder.Services.AddTransient<IUserManagmentService, UserManagmentService>();
builder.Services.AddTransient<IAttachmentService, AttachmentService>();
builder.Services.AddTransient<IGoogleRecaptchaService, GoogleRecaptchaService>();
builder.Services.AddTransient<ActivitiesLog>();
builder.Services.AddTransient<AuthLog>();
builder.Services.AddTransient<IViewLocalizer,ViewLocalizer>();
builder.Services.AddHttpContextAccessor();

//Role and Permission Configuration
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
});

builder.Services.ConfigureApplicationCookie(options => options.Cookie.Name = "IraqWebsite");

builder.Services.AddAuthorization(options =>
{
    string[] models = {
        "Dashboard", "User", "User Manag", "User Lock", "Role",
        "Email Setting", "Psaaword Complexity","Appearance",
        "User Manag","User Lock","Slider","Schedule Section","Schedule Sub Section","Subscribers",
        "About","Portfolio","Event","News","Partner","Contact","FmLink","Log","Recaptcha","Meta"
    };

    string[] claimsModel = { "Create", "Edit", "Delete", "View" };

    for (int i = 0; i < models.Length; i++)
    {
        for (int j = 0; j < claimsModel.Length; j++)
        {
            options.AddPolicy(models[i] + " " + claimsModel[j], policy => policy.RequireClaim("Permission", models[i] + " " + claimsModel[j]));
        }
    }
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<FooterViewComponent>();
builder.Services.AddTransient<HeaderViewComponent>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-IQ"),
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("app");

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultUsers.SeedSuperAdminUsersAsync(userManager, roleManager);
        await DefaultsSettings.SeedSettings(context);

        logger.LogInformation("Seeding Finished");
        logger.LogInformation("Application Starting");
    }
    catch (Exception e)
    {
        logger.LogWarning(e, "An error occurred seeding the DB");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
