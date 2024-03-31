using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.AboutUs;
using IraqWebsite.ViewModels.Blogs;
using IraqWebsite.ViewModels.BlogSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]
	public class AboutUsAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public AboutUsAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.AboutUs.ToListAsync();
			var page = await _context.AboutUsPages.ToListAsync();
			return View(new AboutUsAdmin
			{
				Sections = section,
				AboutUsPages = page
			});
		}
		public IActionResult CreateSection()
		{
			return View();
		}
		public IActionResult CreatePage()
		{
			return View();
		}
		public async Task<IActionResult> EditSection(Guid id)
		{
			var aboutUs = await _context.AboutUs.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditAboutUsVm>(aboutUs);
			ViewBag.ImgOne = aboutUs.ImgOne;
			ViewBag.ImgTwo = aboutUs.ImgTwo;
			ViewBag.ImgThree = aboutUs.ImgThree;

			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditPage(int id)
		{
			var aboutUs = await _context.AboutUsPages.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditAboutUsPageVm>(aboutUs);
			ViewBag.ImageSectionOne = aboutUs.ImageSectionOne;
			ViewBag.ImageOurVision = aboutUs.ImageOurVision;
			ViewBag.ImageOurGoal = aboutUs.ImageOurGoal;
			ViewBag.ImageOurMission = aboutUs.ImageOurMission;
			ViewBag.CompanySkillImageOne = aboutUs.CompanySkillImageOne;
			ViewBag.CompanySkillImageTwo = aboutUs.CompanySkillImageTwo;
			ViewBag.CompanySkillImageThree = aboutUs.CompanySkillImageThree;
			ViewBag.ImageManagerWord = aboutUs.ImageManagerWord;
			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> CreatePage(CreateAboutUsPageVm model)
		{
			if (ModelState.IsValid)
			{
				var aboutUs = _mapper.Map<AboutUsPage>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.ImageSectionOne, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				var img2 = await FilesHelper.ValdiateFilesAsync(model.ImageOurVision, _webHostEnvironment.WebRootPath);
				if (img2 != "Successful Saved")
				{
					ModelState.AddModelError("", img2);
					return View(model);
				}
				var img3 = await FilesHelper.ValdiateFilesAsync(model.ImageOurGoal, _webHostEnvironment.WebRootPath);
				if (img3 != "Successful Saved")
				{
					ModelState.AddModelError("", img3);
					return View(model);
				}
				var img4 = await FilesHelper.ValdiateFilesAsync(model.ImageOurMission, _webHostEnvironment.WebRootPath);
				if (img4 != "Successful Saved")
				{
					ModelState.AddModelError("", img4);
					return View(model);
				}
				var img5 = await FilesHelper.ValdiateFilesAsync(model.CompanySkillImageOne, _webHostEnvironment.WebRootPath);
				if (img5 != "Successful Saved")
				{
					ModelState.AddModelError("", img5);
					return View(model);
				}
				var img6 = await FilesHelper.ValdiateFilesAsync(model.CompanySkillImageTwo, _webHostEnvironment.WebRootPath);
				if (img6 != "Successful Saved")
				{
					ModelState.AddModelError("", img6);
					return View(model);
				}
				var img7 = await FilesHelper.ValdiateFilesAsync(model.CompanySkillImageThree, _webHostEnvironment.WebRootPath);
				if (img7 != "Successful Saved")
				{
					ModelState.AddModelError("", img7);
					return View(model);
				}
				var img8 = await FilesHelper.ValdiateFilesAsync(model.ImageManagerWord, _webHostEnvironment.WebRootPath);
				if (img8 != "Successful Saved")
				{
					ModelState.AddModelError("", img8);
					return View(model);
				}
				
				await _context.AddAsync(aboutUs);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateSection(CreateAboutUsVm model)
		{
			if (ModelState.IsValid)
			{
				var aboutUs = _mapper.Map<AboutUs>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.ImgOne, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				var img2 = await FilesHelper.ValdiateFilesAsync(model.ImgTwo, _webHostEnvironment.WebRootPath);
				if (img2 != "Successful Saved")
				{
					ModelState.AddModelError("", img2);
					return View(model);
				}
				var img3 = await FilesHelper.ValdiateFilesAsync(model.ImgThree ,_webHostEnvironment.WebRootPath);
				if (img3 != "Successful Saved")
				{
					ModelState.AddModelError("", img3);
					return View(model);
				}
				await _context.AddAsync(aboutUs);
				
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditPage(EditAboutUsPageVm model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<AboutUsPage>(model);
				var LastaboutUsSection = await _context.AboutUsPages.AsNoTracking().FirstAsync(x => x.Id == model.Id);
				
				
				if (model.ImageSectionOne != null && LastaboutUsSection?.ImageSectionOne != blog.ImageSectionOne)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageSectionOne, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else {
					blog.ImageSectionOne = LastaboutUsSection.ImageSectionOne;
				}
				if (model.ImageOurVision != null && LastaboutUsSection?.ImageOurVision != blog.ImageOurVision)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageOurVision, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.ImageOurVision = LastaboutUsSection.ImageOurVision;
				}
				if (model.ImageOurGoal != null && LastaboutUsSection?.ImageOurGoal != blog.ImageOurGoal)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageOurGoal, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.ImageOurGoal = LastaboutUsSection.ImageOurGoal;
				}
				if (model.ImageOurMission != null && LastaboutUsSection?.ImageOurMission != blog.ImageOurMission)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageOurMission, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.ImageOurMission = LastaboutUsSection.ImageOurMission;
				}
				if (model.ImageOurMission != null && LastaboutUsSection?.ImageOurMission != blog.ImageOurMission)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageOurMission, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.ImageOurMission = LastaboutUsSection.ImageOurMission;
				}
				if (model.ImageManagerWord != null && LastaboutUsSection?.ImageManagerWord != blog.ImageManagerWord)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImageManagerWord, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.ImageManagerWord = LastaboutUsSection.ImageManagerWord;
				}
				if (model.CompanySkillImageOne != null && LastaboutUsSection?.CompanySkillImageOne != blog.CompanySkillImageOne)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.CompanySkillImageOne, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.CompanySkillImageOne = LastaboutUsSection.CompanySkillImageOne;
				}
				if (model.CompanySkillImageTwo != null && LastaboutUsSection?.CompanySkillImageTwo != blog.CompanySkillImageTwo)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.CompanySkillImageTwo, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.CompanySkillImageTwo = LastaboutUsSection.CompanySkillImageTwo;
				}
				if (model.CompanySkillImageThree != null && LastaboutUsSection?.CompanySkillImageThree != blog.CompanySkillImageThree)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.CompanySkillImageThree, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.CompanySkillImageThree = LastaboutUsSection.CompanySkillImageThree;
				}
				_context.Update(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditSection(EditAboutUsVm model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<AboutUs>(model);
				var LastaboutUsSection = await _context.AboutUs.AsNoTracking().FirstAsync(x => x.Id == model.Id) ;
				if(model.ImgOne != null && LastaboutUsSection?.ImgOne != blog.ImgOne)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.ImgOne, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return View(model);
					}
				}
				else
				{
					blog.ImgOne = LastaboutUsSection?.ImgOne;
				}
				if (model.ImgTwo != null && LastaboutUsSection?.ImgTwo != blog.ImgTwo)
				{
					var img2 = await FilesHelper.ValdiateFilesAsync(model.ImgTwo, _webHostEnvironment.WebRootPath);
					if (img2 != "Successful Saved")
					{
						ModelState.AddModelError("", img2);
						return View(model);
					}
				}
				else
				{
					blog.ImgTwo = LastaboutUsSection?.ImgTwo;
				}
				if (model.ImgThree != null && LastaboutUsSection?.ImgThree != blog.ImgThree)
				{
					var img3 = await FilesHelper.ValdiateFilesAsync(model.ImgThree, _webHostEnvironment.WebRootPath);
					if (img3 != "Successful Saved")
					{
						ModelState.AddModelError("", img3);
						return View(model);
					}
				}
				else
				{
					blog.ImgThree = LastaboutUsSection?.ImgThree;
				}
				_context.AboutUs.Update(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
	}
}
