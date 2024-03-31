using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Projects;
using IraqWebsite.ViewModels.ProjectSection;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.ServiceSection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProjectsController> _localizer;

        public ProjectsController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ProjectsController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index(int year)
        {
            ViewData["RecentProjects"] = _localizer["Recent Projects"];
            ViewData["Projects"] = _localizer["Projects"];
            ViewData["ProjectsDate"] = _localizer["Projects Date"];



            IQueryable<ProjectModel> aboutUs = _context.Projects.OrderByDescending(x=>x.CreatedDate).Include(x=>x.Category);
            if (year != 0)
            {
                aboutUs.Where(x => x.Date.Year == year);
            }

            var projects = _mapper.Map<List<ProjectDto>>(await aboutUs.ToListAsync());
            ViewBag.ProjectSection = _mapper.Map<ProjectSectionDto>(await _context.ProjectSection.FirstOrDefaultAsync());
            return View(projects);
        }
        public async Task<IActionResult> Details(int id)
        {

            ViewData["ProjectDetails"] = _localizer["Project Details"];
            ViewData["Client"] = _localizer["Client"];
            ViewData["Category"] = _localizer["Category"];
            ViewData["Date"] = _localizer["Date"];
            ViewData["Checkout"] = _localizer["Check out more works"];
            ViewData["Oursimilarwork"] = _localizer["Our similar work"];
            ViewData["Address"] = _localizer["Address"];
            ViewData["AnyQuestions"] = _localizer["Any Questions"];
            ViewData["ContactUs"] = _localizer["Contact Us"];



            if (id == 0) {
                return NotFound();
            }
            var aboutUs = _mapper.Map<ProjectDto>(await _context.Projects.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id == id));
            if (aboutUs == null)
            {
                return NotFound();
            }
            ViewBag.Projects = _mapper.Map<List<ProjectDto>>(await _context.Projects.Where(x=>x.Id != id).Include(x=>x.Category).Take(4).ToListAsync());
            return View(aboutUs);
        }
    }
}
