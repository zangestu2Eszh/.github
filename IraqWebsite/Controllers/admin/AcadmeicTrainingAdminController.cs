using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.AcadmeicTraining;
using IraqWebsite.ViewModels.Statiscs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]
	public class AcadmeicTrainingAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public AcadmeicTrainingAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.AcadmeicTraining.ToListAsync();
			return View(section);
		}
		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> Edit(int id)
		{
			var aboutUs = await _context.AcadmeicTraining.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditAcadmeicTrainingDto>(aboutUs);
			ViewBag.ImageOne = aboutUs.ImageOne;
			ViewBag.ImageTwo = aboutUs.ImageTwo;
			ViewBag.ImageThree = aboutUs.ImageThree;

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateAcadmeicTrainingDto model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<AcadmeicTraining>(model);
				
				var img = await FilesHelper.ValdiateFilesAsync(model.ImageOne, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				var img2 = await FilesHelper.ValdiateFilesAsync(model.ImageTwo, _webHostEnvironment.WebRootPath);
				if (img2 != "Successful Saved")
				{
					ModelState.AddModelError("", img2);
					return View(model);
				}
				var img3 = await FilesHelper.ValdiateFilesAsync(model.ImageThree, _webHostEnvironment.WebRootPath);
				if (img3 != "Successful Saved")
				{
					ModelState.AddModelError("", img3);
					return View(model);
				}
				await _context.AddAsync(service);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditAcadmeicTrainingDto model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<AcadmeicTraining>(model);
				var StatiscCurrent = await _context.AcadmeicTraining.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
				if(model.ImageOne != null && StatiscCurrent.ImageOne != service.ImageOne)
                {
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageOne, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
                {
					service.ImageOne = StatiscCurrent.ImageOne;
                }
				if (model.ImageTwo != null && StatiscCurrent.ImageTwo != service.ImageTwo)
				{
					var img2 = await FilesHelper.ValdiateFilesAsync(model.ImageTwo, _webHostEnvironment.WebRootPath);
					if (img2 != "Successful Saved")
					{
						ModelState.AddModelError("", img2);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.ImageTwo = StatiscCurrent.ImageTwo;
				}
				
				if (model.ImageThree != null && StatiscCurrent.ImageThree != service.ImageThree)
				{
					var img3 = await FilesHelper.ValdiateFilesAsync(model.ImageThree, _webHostEnvironment.WebRootPath);
					if (img3 != "Successful Saved")
					{
						ModelState.AddModelError("", img3);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.ImageThree = StatiscCurrent.ImageThree;
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
			if (_context.AcadmeicTraining == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.AcadmeicTraining.FindAsync(id);
			if (aboutUs != null)
			{
				_context.AcadmeicTraining.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
