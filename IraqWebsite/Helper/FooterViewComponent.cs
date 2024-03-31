using IraqWebsite.Data;
using IraqWebsite.ViewModels.ContactUs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using IraqWebsite.Models;
using NuGet.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IraqWebsite.Helper
{
	public class FooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<FooterViewComponent> _localizer;
        private readonly IMapper _mapper;
        public FooterViewComponent(ApplicationDbContext context, IStringLocalizer<FooterViewComponent> localizer,IMapper mapper)
		{
			_context = context;
			_localizer = localizer;
            _mapper = mapper;
		}

		public IViewComponentResult Invoke()
        {
            // Fetch the footer data from your data source or any other logic
            var footerData = GetFooterDataFromDataSource();

            return View(footerData);
        }

        private async Task<string> GetFooterDataFromDataSource()
        {
            ViewData["Home"] = _localizer["Home"];
            ViewData["About"] = _localizer["About Us"];
            ViewData["Services"] = _localizer["Service&Activities"];
            ViewData["Projectss"] = _localizer["Projects"];
            ViewData["JoinNewsLetter"] = _localizer["JOIN OUR NEWSLETTER."];
            ViewData["MainPage"] = _localizer["Main Page"];
            ViewData["Contactt"] = _localizer["Contact Us"];
            ViewData["Contact"] = _localizer["Connect With Us"];
            ViewData["CopyRight"] = _localizer["All Right Reserved"];
            ViewData["EnteryourEmail"] = _localizer["Enter your Email"];
            ViewData["Subscribe"] = _localizer["Subscribe"];
            ViewData["SubscribeDesc"] = _localizer["Subscribe Our Newsletter For Getting Quick Updates"];
            ViewData["Question"] = _localizer["WE ARE HERE TO ANSWER YOUR QUESTIONS 24/7"];
            ViewData["Consult"] = _localizer["NEED A CONSULTATION?"];
            ViewData["Pages"] = _localizer["Pages"];
            ViewData["ConnectWithUs"] = _localizer["Connect With Us"];
            ViewData["RecentlyEvents"] = _localizer["Recently Events"];
            ViewData["AcedemicTraining"] = _localizer["Acedemic Training"];
            ViewData["Departments"] = _localizer["Departments"];
            ViewData["GeneralComp"] = _localizer["General Comp. for"];
            ViewData["ElectronicSystems"] = _localizer["Electronic Systems"];
            ViewData["Copyright"] = _localizer["Copyright  2024 GENERAL COMPANY FOR ELECTRONIC SYSTEM"];


            ViewData["SiteKey"] =  _context.GoogleRecaptcha.Select(x=>x.SiteKey).FirstOrDefault();
            ViewBag.Events = await _context.Events.OrderByDescending(x=>x.CreatedDate).Take(4).ToListAsync();
            
            var contactUs = _mapper.Map<ContactUsDto>( _context.ContactUs.AsNoTracking().FirstOrDefault());
            if (contactUs != null)
            {
                string footerData = $"Title: {contactUs.Title}, Location: {contactUs.Location}, Phone: {contactUs.Phone}";
                ViewData["FooterTitle"] = contactUs.Title;
                ViewData["FooterEmail"] = contactUs.Email;
                ViewData["FooterPhone"] = contactUs.Phone;
                ViewData["FooterLocation"] = contactUs.Location;
                ViewData["FooterInstgram"] = contactUs.Instgram;
                ViewData["FooterFacebook"] = contactUs.Facebook;
                ViewData["FooterLinkden"] = contactUs.Linkden;
                ViewData["Slogan"] = contactUs.Slogan;
                return footerData;
            }
            return "Footer data not found";
        }
    }

}
