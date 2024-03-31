using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.AboutUs;
using AutoMapper;
using IraqWebsite.Helper;
using Microsoft.AspNetCore.Authorization;
using static IraqWebsite.AuthManager.Statics.Permissions;
using Microsoft.Extensions.Localization;
using IraqWebsite.ViewModels.ContactUs;
using IraqWebsite.ViewModels.Statiscs;
using IraqWebsite.ViewModels.Managers;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class AboutUsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<AboutUsController> _localizer;


        public AboutUsController(ApplicationDbContext context,IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<AboutUsController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["About"] = _localizer["About Us"];
            ViewData["OurGoals"] = _localizer["Our Goals"];
            ViewData["OurMission"] = _localizer["Our Mission"];
            ViewData["OurVision"] = _localizer["Our Vision"];
            ViewData["MANAGERWORD"] = _localizer["MANAGER WORD"];
            ViewData["OrganizationalChart"] = _localizer["to see Organizational Chart"];
            ViewData["MAKEANAPPOINTMENT"] = _localizer["MAKE AN APPOINTMENT"];
            ViewData["MAKEANAPPOINTMENTT"] = _localizer["MAKE AN APPOINTMENT"];
            ViewData["MAKEANAPPOINTMENTTT"] = _localizer["MAKE AN APPOINTMENT"];
            ViewData["WeProvideBestAdviceForYourFuture"] = _localizer["We Provide Best Advice For Your Future"];
            ViewData["CALLUs"] = _localizer["CALL Us"];
            ViewData["CompanyEmail"] = _localizer["Company Email"];
            ViewData["Location"] = _localizer["Location"];
            ViewData["THEGREATCOMPANYSKILL"] = _localizer["THE GREAT COMPANY SKILL"];
            ViewData["ManagersArchives"] = _localizer["Managers Archives"];
            ViewData["OurManagers"] = _localizer["Our Managers"];
            ViewData["OURTRUSTEDCLIENTS"] = _localizer["OUR TRUSTED CLIENTS"];
            ViewData["YearsExperience"] = _localizer["Years Experience"];





            ViewBag.Clients = await _context.Clients.ToListAsync();
            ViewBag.Managers = _mapper.Map<List<ManagerDto>>(await _context.Managers.Take(8).ToListAsync());
            ViewBag.Statiscs = _mapper.Map<StatiscDto>(await _context.Statiscs.FirstOrDefaultAsync());
            ViewBag.Contact = _mapper.Map<ContactUsDto>(await _context.ContactUs.FirstOrDefaultAsync());
            ViewBag.AboutUs = _mapper.Map<AboutUsDto>(await _context.AboutUs.FirstOrDefaultAsync());
            var aboutUs = _mapper.Map<AboutUsPageDto>(await _context.AboutUsPages.FirstOrDefaultAsync());
            return View(aboutUs);
        }


    }
}
