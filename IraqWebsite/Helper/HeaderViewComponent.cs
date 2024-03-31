using IraqWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using AutoMapper;
using IraqWebsite.ViewModels.ContactUs;

namespace IraqWebsite.Helper
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<HeaderViewComponent> _localizer;
        private readonly IMapper _mapper;
        public HeaderViewComponent(ApplicationDbContext context,IStringLocalizer<HeaderViewComponent> localizer,IMapper mapper)
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            // Fetch the footer data from your data source or any other logic
            var footerData = GetHeaderDataFromDataSource();

            return View(footerData);
        }

        private async Task<string> GetHeaderDataFromDataSource()
        {
            var contactUs = _mapper.Map<ContactUsDto>( _context.ContactUs.AsNoTracking().FirstOrDefault());

            ViewData["Phone"] = contactUs.Phone;
            ViewData["FooterInstgram"] = contactUs.Instgram;
            ViewData["FooterFacebook"] = contactUs.Facebook;
            ViewData["FooterEmail"] = contactUs.Email;
            ViewData["Home"] = _localizer["Home"];
            ViewData["About"] = _localizer["About Us"];
            ViewData["Services"] = _localizer["Service&Activities"];
            ViewData["Projects"] = _localizer["Projects"];
            ViewData["Events"] = _localizer["Exhibitions&Events"];
            ViewData["AcedemicTraining"] = _localizer["Acedemic Training"];
            ViewData["Departments"] = _localizer["Departments"];
            ViewData["Contact"] = _localizer["Contact Us"];
            ViewData["GeneralComp"] = _localizer["General Comp. for"];
            ViewData["ElectronicSystems"] = _localizer["Electronic Systems"];
            ViewData["UR"] = _localizer["UR"];

            return "Header Data";
        }
    }

}
