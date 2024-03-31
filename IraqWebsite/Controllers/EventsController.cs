using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.ViewModels.Events;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.ServiceSection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<EventsController> _localizer;

        public EventsController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<EventsController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index(int categoryId)
        {
            ViewData["Services"] = _localizer["Services"];
            ViewData["RecentPosts"] = _localizer["Recent Posts"];
            ViewData["Categoriess"] = _localizer["Categories"];
            ViewData["READDETAILS"] = _localizer["READ DETAILS"];
            ViewData["Exhibitions&Events"] = _localizer["Exhibitions&Events"];




            ViewBag.Events = _mapper.Map<List<EventDto>>(await _context.Events.Include(x => x.Category).OrderByDescending(x => x.CreatedDate).Take(5).ToListAsync());
            ViewBag.Categories = _mapper.Map<List<EventCategoryDto>>(await _context.EventCategory.ToListAsync());
            
            IQueryable<IraqWebsite.Models.Event> aboutUs = _context.Events.Include(x=>x.Category).OrderByDescending(x=>x.CreatedDate);
            
            if(categoryId != 0)
            {
                aboutUs.Where(x => x.EventCategoryId == categoryId);
            }

            var events = _mapper.Map<List<EventDto>>(await aboutUs.ToListAsync());
            return View(events);
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewData["RecentServices"] = _localizer["Recents Services"];
            ViewData["MoreDetails"] = _localizer["For More Details Contact Us"];
            ViewData["ContactUs"] = _localizer["Contact Us"];
            ViewData["RecentEvents"] = _localizer["Recent Events"];


            if (id == 0) {
                return NotFound();
            }
            var aboutUs = _mapper.Map<EventDto>(await _context.Events.Include(x => x.Category).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x=>x.Id == id));
            if (aboutUs == null)
            {
                return NotFound();
            }
            ViewBag.Events = _mapper.Map<List<EventDto>>(await _context.Events.Where(x => x.Id != id).Include(x => x.Category).OrderByDescending(x => x.CreatedDate).Take(4).ToListAsync());

            return View(aboutUs);
        }
    }
}
