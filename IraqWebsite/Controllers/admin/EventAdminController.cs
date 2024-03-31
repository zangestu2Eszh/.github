using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Events;
using IraqWebsite.ViewModels.EventSection;
using IraqWebsite.ViewModels.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class EventAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public EventAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.EventSection.ToListAsync();
			var blogs = await _context.Events.ToListAsync();
			var categories = await _context.EventCategory.ToListAsync();
			return View(new EventAdmin
			{
				Sections = section,
				Events = blogs,
				Categories = categories
			});
		}
		public IActionResult CreateSection()
		{
			return View();
		}
		public async Task<IActionResult> CreateEvent()
		{
			ViewBag.Categories = await _context.EventCategory.ToListAsync();
			return View();
		}
		public IActionResult CreateCategory()
		{
			return View();
		}
		public async Task<IActionResult> EditSection(int id)
		{
			var aboutUs = await _context.EventSection.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditEventSection>(aboutUs);

			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditEvent(int id)
		{
			var aboutUs = await _context.Events.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditEvent>(aboutUs);
			ViewBag.Categories = await _context.EventCategory.ToListAsync();
			ViewBag.Image = aboutUs.Image;
			ViewBag.ImageTwo = aboutUs.ImageTwo;
			ViewBag.ImageThree = aboutUs.ImageThree;
			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditCategory(int id)
		{
			var aboutUs = await _context.EventCategory.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditEventCategory>(aboutUs);

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> CreateEvent(CreateEvent model)
		{
			if (ModelState.IsValid)
			{
				var section = await _context.EventSection.FirstOrDefaultAsync();
				model.EventSectionId = section.Id;
				var project = _mapper.Map<Event>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return RedirectToAction(nameof(CreateProject));
				}
				var img2 = await FilesHelper.ValdiateFilesAsync(model.ImageTwo, _webHostEnvironment.WebRootPath);
				if (img2 != "Successful Saved")
				{
					ModelState.AddModelError("", img2);
					return RedirectToAction(nameof(CreateProject));
				}
				var img3 = await FilesHelper.ValdiateFilesAsync(model.ImageThree, _webHostEnvironment.WebRootPath);
				if (img3 != "Successful Saved")
				{
					ModelState.AddModelError("", img3);
					return RedirectToAction(nameof(CreateProject));
				}
				await _context.AddAsync(project);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(CreateProject));
		}
		[HttpPost]
		public async Task<IActionResult> CreateSection(CreateEventSection model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<EventSection>(model);
				await _context.AddAsync(project);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateEventCategory model)
		{
			if (ModelState.IsValid)
			{
				var category = _mapper.Map<EventCategory>(model);
				await _context.AddAsync(category);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditEvent(EditEvent model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<Event>(model);
				var currentProject = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
				project.EventSectionId = currentProject.EventSectionId;
				if (model.Image != null && currentProject?.Image != project.Image)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditProject));
					}
				}
				else
				{
					project.Image = currentProject.Image;
				}
				if (model.ImageTwo != null && currentProject?.ImageTwo != project.ImageTwo)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageTwo, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditProject));
					}
				}
				else
				{
					project.ImageTwo = currentProject.ImageTwo;
				}
				if (model.ImageThree != null && currentProject?.ImageThree != project.ImageThree)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageThree, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditProject));
					}
				}
				else
				{
					project.ImageThree = currentProject.ImageThree;
				}
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(EditProject));
		}
		[HttpPost]
		public async Task<IActionResult> EditSection(EditEventSection model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<EventSection>(model);
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditCategory(EditEventCategory model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<EventCategory>(model);
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Events == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Events.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Events.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (_context.EventCategory == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.EventCategory.FindAsync(id);
			if (aboutUs != null)
			{
				_context.EventCategory.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
