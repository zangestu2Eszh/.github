using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.ServiceSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class ServiceAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ServiceAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.ServiceSection.ToListAsync();
			var services = await _context.Services.ToListAsync();
			return View(new ServiceAdmin
			{
				Sections = section,
				Services = services,
			});
		}
		public async Task<IActionResult> CreateSection()
		{
			return View();
		}
		public async Task<IActionResult> CreateService()
		{
			return View();
		}
		public async Task<IActionResult> EditSection(int id)
		{
			var aboutUs = await _context.ServiceSection.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditServiceSection>(aboutUs);
			
			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditService(int id)
		{
			var aboutUs = await _context.Services.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditService>(aboutUs);

			ViewBag.Image = aboutUs.Image;
			ViewBag.ImageTwo = aboutUs.ImageTwo;
			ViewBag.ImageThree = aboutUs.ImageThree;
			ViewBag.Icon= aboutUs.Icon;
			ViewBag.IconWhite = aboutUs.IconWhite;

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> CreateService(CreateService model)
		{
			if (ModelState.IsValid)
			{
				var section = await _context.ServiceSection.FirstOrDefaultAsync();
				model.ServiceSectionId = section.Id;	
				var service = _mapper.Map<Service>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				await _context.AddAsync(service);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateSection(CreateServiceSection model)
		{
			if (ModelState.IsValid)
			{
				var serviceSection = _mapper.Map<ServiceSection>(model);
				await _context.AddAsync(serviceSection);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditService(EditService model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<Service>(model);
				var currentService = await _context.Services.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == model.Id);
				service.ServiceSectionId = currentService.ServiceSectionId;
				if(model.Image != null && service.Image != currentService.Image) 
                {
					var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditService));
					}
				}
                else
                {
					service.Image = currentService.Image;
                }
				if (model.ImageTwo != null && service.ImageTwo != currentService.ImageTwo)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageTwo, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditService));
					}
				}
				else
				{
					service.ImageTwo = currentService.ImageTwo;
				}
				if (model.ImageThree != null && service.ImageThree != currentService.ImageThree)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageThree, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditService));
					}
				}
				else
				{
					service.ImageThree = currentService.ImageThree;
				}
				if (model.Icon != null && service.Icon != currentService.Icon)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.Icon, _webHostEnvironment.WebRootPath);

					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditService));
					}
				}
				else
				{
					service.Icon = currentService.Icon;
				}
				if (model.IconWhite != null && service.IconWhite != currentService.IconWhite)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.IconWhite, _webHostEnvironment.WebRootPath);

					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditService));
					}
				}
				else
				{
					service.IconWhite = currentService.IconWhite;
				}
				_context.Update(service);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(EditService));
		}
		[HttpPost]
		public async Task<IActionResult> EditSection(EditServiceSection model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<ServiceSection>(model);
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Services == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Services.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Services.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
