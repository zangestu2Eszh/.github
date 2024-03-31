using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.BlogSection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
	public class BlogController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IStringLocalizer<BlogController> _localizer;
		public BlogController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<BlogController> localizer)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
			_localizer = localizer;
		}
		public async Task<IActionResult> Index()
		{
			return View();
		}
		public IActionResult CreateBlogSection()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateBlogSection(CreateBlogSection model)
		{
			if (ModelState.IsValid)
			{
				var blogSection = _mapper.Map<BlogSection>(model);
				_context.Add(blogSection);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

	}
}
