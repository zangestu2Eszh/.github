using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.ViewModels.AcadmeicTraining;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.ServiceSection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    public class AcadmeicTrainingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AcadmeicTrainingController> _localizer;

        public AcadmeicTrainingController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<AcadmeicTrainingController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Services"] = _localizer["Services"];
			ViewData["AcedemicTraining"] = _localizer["Acedemic Training"];
            ViewData["AnyQuestions"] = _localizer["Any Question"];
            ViewData["ContactUs"] = _localizer["Contact Us"];




            var aboutUs = _mapper.Map<AcadmeicTrainingDto>(await _context.AcadmeicTraining.FirstOrDefaultAsync());
            return View(aboutUs);
        }
    }
}
