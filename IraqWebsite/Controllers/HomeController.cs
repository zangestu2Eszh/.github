using IraqWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IraqWebsite.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using IraqWebsite.ViewModels.Slider;
using IraqWebsite.ViewModels.AboutUs;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.Statiscs;
using IraqWebsite.ViewModels.ProjectSection;
using IraqWebsite.ViewModels.ServiceSection;
using IraqWebsite.ViewModels.Projects;
using IraqWebsite.ViewModels.Blogs;
using IraqWebsite.ViewModels.EventSection;
using IraqWebsite.ViewModels.Events;
using IraqWebsite.ViewModels.ContactUs;
using IraqWebsite.ViewModels.Managers;
using IraqWebsite.ViewModels.Products;

namespace IraqWebsite.Controllers
{
	public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext context, IMapper mapper,IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            //ViewData["OurProgram"] = _localizer["Our Programs"];
            //ViewData["SignUpTitle"] = _localizer["Sign Up for Newsletter"];
            //ViewData["SignUpDesc"] = _localizer["We’ll be with you on every walk of life on how to identify new opportunities.ion"];
            //ViewData["Subscribe"] = _localizer["Subscribe"];

            ViewData["ViewAllProjects"] = _localizer["View All Projects"];
            ViewData["ReadDetails"] = _localizer["Read Details"];
            ViewData["VIEWSERVICES"] = _localizer["VIEW ALL SERVICES"];
            ViewData["LEARNMORE"] = _localizer["LEARN MORE"];
            ViewData["YearsExperience"] = _localizer["Years Experience"];
            ViewData["ReadMore"] = _localizer["Read More"];
            ViewData["AboutUss"] = _localizer["About Us"];
            ViewData["bookanappointment"] = _localizer["BOOK AN APPOINTMENT"];
            ViewData["bookanappointmentt"] = _localizer["BOOK AN APPOINTMENT"];
            ViewData["bookanappointmenttt"] = _localizer["BOOK AN APPOINTMENT"];
            ViewData["WEARE"] = _localizer["WE ARE HERE TO ANSWER YOUR QUESTIONS"];
            ViewData["WeProvideBest"] = _localizer["We Provide Best Advice For Your Future"];
            ViewData["CallAvailable"] = _localizer["Call Available"];
            ViewData["EmailUs"] = _localizer["Email Us"];
            ViewData["OfficeLocation"] = _localizer["Office Location"];
            ViewData["productss"] = _localizer["Products"];
            ViewData["Latestproductss"] = _localizer["Latest Products"];
            ViewData["NeedAConsultation"] = _localizer["Need A Consultation"];
            ViewData["ContactUs"] = _localizer["Contact Us"];
            ViewData["MeetOurManagers"] = _localizer["Meet Our Managers"];
            ViewData["ManagersArchives"] = _localizer["Managers Archives"];
            ViewData["OurTrustedClients"] = _localizer["Our Trusted Clients"];
            ViewData["UR"] = _localizer["UR Products"];









            ViewBag.Sliders = _mapper.Map<List<SliderDto>>(await _context.Slider.OrderByDescending(x => x.CreatedDate).ToListAsync());
            ViewBag.AboutUs = _mapper.Map<AboutUsDto>(await _context.AboutUs.FirstOrDefaultAsync());
            ViewBag.ServiceSection = _mapper.Map<ServiceSectionDto>(await _context.ServiceSection.FirstOrDefaultAsync());
            ViewBag.EventSection = _mapper.Map<EventSectionDto>(await _context.EventSection.FirstOrDefaultAsync());
            ViewBag.Services = _mapper.Map<List<ServiceDto>>(await _context.Services.Take(8).ToListAsync());
            ViewBag.Managers = _mapper.Map<List<ManagerDto>>(await _context.Managers.Take(8).ToListAsync());
            ViewBag.Events = _mapper.Map<List<EventDto>>(await _context.Events.Take(8).Include(x => x.Category).OrderByDescending(x=>x.CreatedDate).ToListAsync());
            ViewBag.Statiscs = _mapper.Map<StatiscDto>(await _context.Statiscs.FirstOrDefaultAsync());
            ViewBag.ProjectSection = _mapper.Map<ProjectSectionDto>(await _context.ProjectSection.FirstOrDefaultAsync());
            ViewBag.Projects = _mapper.Map<List<ProjectDto>>(await _context.Projects.Include(x=>x.Category).OrderByDescending(x => x.CreatedDate).Take(8).ToListAsync());
            ViewBag.Products = _mapper.Map<List<ProductDto>>(await _context.Products.Include(x=>x.Category).OrderByDescending(x => x.CreatedDate).ToListAsync());
            ViewBag.BlogSection = _mapper.Map<BlogSection>(await _context.BlogSection.FirstAsync());
            ViewBag.Blogs = _mapper.Map<List<BlogDto>>(await _context.Blogs.Include(x=>x.Category).OrderByDescending(x=>x.CreatedDate).Take(8).ToListAsync());
            ViewBag.Clients = await _context.Clients.ToListAsync();
            ViewBag.Contact = _mapper.Map<ContactUsDto>(await _context.ContactUs.FirstOrDefaultAsync());

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult GetLanguage()
        {
            var language = Thread.CurrentThread.CurrentUICulture.Name;

            return Ok(language);
        }
        public async Task<IActionResult> Unsubscribe([EmailAddress] string email)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (email == null)
                return NotFound();
            
            var subscriber = await _context.Subscribers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (subscriber == null)
                return NotFound();

            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}