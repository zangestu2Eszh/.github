using IraqWebsite.AuthManager.Response;
using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.Data;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.AuthManager.Models;
using IraqWebsite.Helper;

namespace IraqWebsite.Services.SettingsService.AppearanceService
{
    public class AppearanceService : IAppearanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AppearanceService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Appearance> GetAppearance()
        {
            var appearance = await _context.Appearance.FirstOrDefaultAsync();
            if (appearance is not null)
            {
                return appearance;
            }
            return null;
        }

        public async Task<Appearance> UpdateAppearance(AppearanceViewModel viewModel)
        {
            try
            {
                var appearance = await _context.Appearance.FirstOrDefaultAsync();
                if (appearance is null)
                {
                    Appearance a = new Appearance
                    {
                        Id = Guid.NewGuid()
                    };
                    _context.Appearance.Add(a);
                    await _context.SaveChangesAsync();
                    return appearance;
                }

                appearance.ApplicationName = viewModel.ApplicationName;
                appearance.ColorWhite = viewModel.ColorWhite;
                appearance.ColorBlack = viewModel.ColorBlack;
                appearance.ColorMode = viewModel.ColorMode;
                appearance.MetaTitle = viewModel.MetaTitle;
                appearance.MetaDescription = viewModel.MetaDescription;

                if (viewModel.logo is not null)
                {
                    var img = await FilesHelper.ValdiateFilesAsync(viewModel.logo, _webHostEnvironment.WebRootPath);
                    if (img != "Successful Saved")
                    {
                        return null;
                    }
                    else
                    {
                        appearance.ApplicationLogo = "/Images/"+viewModel.logo.FileName;
                    }
                }

                if (viewModel.favIcon is not null)
                {
                    var img = await FilesHelper.ValdiateFilesAsync(viewModel.favIcon, _webHostEnvironment.WebRootPath);
                    if (img != "Successful Saved")
                    {
                        return null;
                    }
                    else
                    {
                        appearance.FavIcon = "/Images/" + viewModel.favIcon.FileName;
                    }
                }

                _context.Appearance.Update(appearance);
                await _context.SaveChangesAsync();

                return appearance;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
