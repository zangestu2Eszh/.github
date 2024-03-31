using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Managers;
using IraqWebsite.ViewModels.Statiscs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class ManagersAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ManagersAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.Managers.ToListAsync();
			return View(section);
		}
		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> Edit(int id)
		{
			var aboutUs = await _context.Managers.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditManager>(aboutUs);
			ViewBag.Image = aboutUs.Image;

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateManager model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<Managers>(model);
				
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
		public async Task<IActionResult> Edit(EditManager model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<Managers>(model);
				var StatiscCurrent = await _context.Managers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
				if(model.Image != null && StatiscCurrent.Image != service.Image)
                {
					var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
                {
					service.Image = StatiscCurrent.Image;
                }
				_context.Update(service);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Edit));
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Managers == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Managers.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Managers.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
