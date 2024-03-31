using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DepartmentsController> _localizer;

        public DepartmentsController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<DepartmentsController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Departments"] = _localizer["Departments"];
            ViewData["ToSeeTheStructure"] = _localizer["To see the structure of the company"];
            ViewData["ViewAllStructure"] = _localizer["View All Structure"];
            
            ViewBag.StructrueSection = await _context.Structrue.Select(x => x.SectionImage).FirstOrDefaultAsync();

            var aboutUs = _mapper.Map<List<DepartmentDto>>(await _context.Departments.ToListAsync());
            
            return View(aboutUs);
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Department"] = _localizer["Department"];
            ViewData["ToSeeTheStructure"] = _localizer["To see the structure of the company"];
            ViewData["ViewAllStructure"] = _localizer["View All Structure"];

            ViewBag.StructrueSection = await _context.Structrue.Select(x => x.SectionImage).FirstOrDefaultAsync();


            if (id == 0) {
                return NotFound();
            }
            var aboutUs = _mapper.Map<DepartmentDto>(await _context.Departments.FindAsync(id));
            if (aboutUs == null)
            {
                return NotFound();
            }
            return View(aboutUs);
        }
        public async Task<IActionResult> Structure()
        {
            ViewData["Structure"] = _localizer["Structure"];
            ViewData["Organizational"] = _localizer["Organizational Chart"];
            ViewBag.StructrueSection = await _context.Structrue.Select(x => x.StructureImage).FirstOrDefaultAsync();

            return View();
        }
    }
}
