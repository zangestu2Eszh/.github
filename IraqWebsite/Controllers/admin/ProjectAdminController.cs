using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Blogs;
using IraqWebsite.ViewModels.BlogSection;
using IraqWebsite.ViewModels.Projects;
using IraqWebsite.ViewModels.ProjectSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class ProjectAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProjectAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.ProjectSection.ToListAsync();
			var blogs = await _context.Projects.ToListAsync();
			var categories = await _context.ProjectCategory.ToListAsync();
			return View(new ProjectAdmin
			{
				Sections = section,
				Projects = blogs,
				Categories = categories
			});
		}
		public IActionResult CreateSection()
		{
			return View();
		}
		public async Task<IActionResult> CreateProject()
		{
			ViewBag.Categories = await _context.ProjectCategory.ToListAsync();
			return View();
		}
		public IActionResult CreateCategory()
		{
			return View();
		}
		public async Task<IActionResult> EditSection(int id)
		{
			var aboutUs = await _context.ProjectSection.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditProjectSection>(aboutUs);

			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditProject(int id)
		{
			var aboutUs = await _context.Projects.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditProject>(aboutUs);
			ViewBag.Categories = await _context.ProjectCategory.ToListAsync();
			ViewBag.Image = aboutUs.Image;
			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditCategory(int id)
		{
			var aboutUs = await _context.ProjectSection.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditProjectSection>(aboutUs);

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> CreateProject(CreateProject model)
		{
			if (ModelState.IsValid)
			{
				var section = await _context.ProjectSection.FirstOrDefaultAsync();
				model.ProjectSectionId = section.Id;
				var project = _mapper.Map<ProjectModel>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return RedirectToAction(nameof(CreateProject));
				}
				await _context.AddAsync(project);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(CreateProject));
		}
		[HttpPost]
		public async Task<IActionResult> CreateSection(CreateProjectSection model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<ProjectSection>(model);
				await _context.AddAsync(project);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateProjectCategory model)
		{
			if (ModelState.IsValid)
			{
				var category = _mapper.Map<ProjectCategory>(model);
				await _context.AddAsync(category);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditProject(EditProject model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<ProjectModel>(model);
				var currentProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
				project.ProjectSectionId = currentProject.ProjectSectionId;
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
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(EditProject));
		}
		[HttpPost]
		public async Task<IActionResult> EditSection(EditProjectSection model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<ProjectSection>(model);
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditCategory(EditProjectCategory model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<ProjectCategory>(model);
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Projects == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Projects.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Projects.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (_context.ProjectCategory == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.ProjectCategory.FindAsync(id);
			if (aboutUs != null)
			{
				_context.ProjectCategory.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
