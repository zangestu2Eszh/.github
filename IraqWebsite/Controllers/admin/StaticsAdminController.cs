using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Statiscs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class StaticsAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public StaticsAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.Statiscs.ToListAsync();
			return View(section);
		}
		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> Edit(int id)
		{
			var aboutUs = await _context.Statiscs.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditStatisc>(aboutUs);
			ViewBag.Image = aboutUs.Image;
			ViewBag.StatiscOneIcon = aboutUs.StatiscOneIcon;
			ViewBag.StatiscTwoIcon = aboutUs.StatiscTwoIcon;
			ViewBag.StatiscThreeIcon = aboutUs.StatiscThreeIcon;
			ViewBag.StatiscFourIcon = aboutUs.StatiscFourIcon;

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateStatisc model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<Statisc>(model);
				
				var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				var img2 = await FilesHelper.ValdiateFilesAsync(model.StatiscOneIcon, _webHostEnvironment.WebRootPath);
				if (img2 != "Successful Saved")
				{
					ModelState.AddModelError("", img2);
					return View(model);
				}
				var img3 = await FilesHelper.ValdiateFilesAsync(model.StatiscTwoIcon, _webHostEnvironment.WebRootPath);
				if (img3 != "Successful Saved")
				{
					ModelState.AddModelError("", img3);
					return View(model);
				}
				var img4 = await FilesHelper.ValdiateFilesAsync(model.StatiscThreeIcon, _webHostEnvironment.WebRootPath);
				if (img4 != "Successful Saved")
				{
					ModelState.AddModelError("", img4);
					return View(model);
				}
				var img5 = await FilesHelper.ValdiateFilesAsync(model.StatiscFourIcon, _webHostEnvironment.WebRootPath);
				if (img5 != "Successful Saved")
				{
					ModelState.AddModelError("", img4);
					return View(model);
				}
				await _context.AddAsync(service);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditStatisc model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<Statisc>(model);
				var StatiscCurrent = await _context.Statiscs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
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
				if (model.StatiscOneIcon != null && StatiscCurrent.StatiscOneIcon != service.StatiscOneIcon)
				{
					var img2 = await FilesHelper.ValdiateFilesAsync(model.StatiscOneIcon, _webHostEnvironment.WebRootPath);
					if (img2 != "Successful Saved")
					{
						ModelState.AddModelError("", img2);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.StatiscOneIcon = StatiscCurrent.StatiscOneIcon;
				}
				
				if (model.StatiscTwoIcon != null && StatiscCurrent.StatiscTwoIcon != service.StatiscTwoIcon)
				{
					var img3 = await FilesHelper.ValdiateFilesAsync(model.StatiscTwoIcon, _webHostEnvironment.WebRootPath);
					if (img3 != "Successful Saved")
					{
						ModelState.AddModelError("", img3);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.StatiscTwoIcon = StatiscCurrent.StatiscTwoIcon;
				}
				
				if (model.StatiscThreeIcon != null && StatiscCurrent.StatiscThreeIcon != service.StatiscThreeIcon)
				{
					var img3 = await FilesHelper.ValdiateFilesAsync(model.StatiscThreeIcon, _webHostEnvironment.WebRootPath);
					if (img3 != "Successful Saved")
					{
						ModelState.AddModelError("", img3);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.StatiscThreeIcon = StatiscCurrent.StatiscThreeIcon;
				}

				if (model.StatiscFourIcon != null && StatiscCurrent.StatiscFourIcon != service.StatiscFourIcon)
				{
					var img4 = await FilesHelper.ValdiateFilesAsync(model.StatiscFourIcon, _webHostEnvironment.WebRootPath);
					if (img4 != "Successful Saved")
					{
						ModelState.AddModelError("", img4);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.StatiscFourIcon = StatiscCurrent.StatiscFourIcon;
				}
				_context.Update(service);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Edit));
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		//[Authorize(Policy = AboutPolicy.Delete)]
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
