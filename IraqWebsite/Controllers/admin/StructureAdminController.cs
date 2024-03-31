using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Client;
using IraqWebsite.ViewModels.Structure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class StructureAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public StructureAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateStructure model)
		{
			if (ModelState.IsValid)
			{
				var structure = _mapper.Map<Structure>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.SectionImage, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				var img2 = await FilesHelper.ValdiateFilesAsync(model.StructureImage, _webHostEnvironment.WebRootPath);
				if (img2 != "Successful Saved")
				{
					ModelState.AddModelError("", img2);
					return View(model);
				}
				await _context.AddAsync(structure);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Edit));
			}
			return View(model);
		}
		public async Task<IActionResult> Edit()
		{
			var aboutUs = await _context.Structrue.FirstOrDefaultAsync();
			if(aboutUs == null)
			{
				return RedirectToAction(nameof(Create));
			}
			var aboutUsVm = _mapper.Map<CreateStructure>(aboutUs);
			ViewBag.SectionImage = aboutUs.SectionImage;
			ViewBag.StructureImage = aboutUs.StructureImage;

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(CreateStructure model)
		{
			if (ModelState.IsValid)
			{
				var service = _mapper.Map<Structure>(model);
				var StatiscCurrent = await _context.Structrue.AsNoTracking().FirstOrDefaultAsync();
				if (model.SectionImage != null && StatiscCurrent.SectionImage != service.SectionImage)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.SectionImage, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.SectionImage = StatiscCurrent.SectionImage;
				}
				if (model.StructureImage != null && StatiscCurrent.StructureImage != service.StructureImage)
				{
					var img2 = await FilesHelper.ValdiateFilesAsync(model.StructureImage, _webHostEnvironment.WebRootPath);
					if (img2 != "Successful Saved")
					{
						ModelState.AddModelError("", img2);
						return RedirectToAction(nameof(Edit));
					}
				}
				else
				{
					service.StructureImage = StatiscCurrent.StructureImage;
				}

				_context.Structrue.Update(service);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Edit));
			}
			return RedirectToAction(nameof(Edit));
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Clients == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Clients.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Clients.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
