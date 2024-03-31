using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.ServiceSection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ServicesController> _localizer;

        public ServicesController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ServicesController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Services"] = _localizer["Services"];
            ViewData["Read"] = _localizer["Read More"];


            var aboutUs = _mapper.Map<List<ServiceDto>>(await _context.Services.ToListAsync());
            ViewBag.ServiceSection = _mapper.Map<ServiceSectionDto>(await _context.ServiceSection.FirstOrDefaultAsync());
            return View(aboutUs);
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewData["RecentServices"] = _localizer["Recents Services"];
            ViewData["Services"] = _localizer["Services"];
            ViewData["Read"] = _localizer["Read More"];
            ViewData["MoreDetails"] = _localizer["For More Details Contact Us"];
            ViewData["ContactUs"] = _localizer["Contact Us"];
            ViewData["AllServices"] = _localizer["All Services"];
            ViewData["Needhelp"] = _localizer["Need help"];
            ViewData["ServiceFeatures"] = _localizer["Service Features"];



            if (id == 0) {
                return NotFound();
            }
            var aboutUs = _mapper.Map<ServiceDto>(await _context.Services.FindAsync(id));
            if (aboutUs == null)
            {
                return NotFound();
            }
            ViewBag.Services = _mapper.Map<List<ServiceDto>>(await _context.Services.Where(x=>x.Id != id).Take(4).ToListAsync());
            var contact = _context.ContactUs.First();
            ViewBag.Phone = contact.Phone;
            ViewBag.Email = contact.Email;
            return View(aboutUs);
        }
    }
}
