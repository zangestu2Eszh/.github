using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Blogs;
using IraqWebsite.ViewModels.BlogSection;
using IraqWebsite.ViewModels.Products;
using IraqWebsite.ViewModels.Projects;
using IraqWebsite.ViewModels.ProjectSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]

	public class ProductAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var blogs = await _context.Products.ToListAsync();
			var categories = await _context.ProductCategory.ToListAsync();
			return View(new ProductAdmin
			{
				Products = blogs,
				Categories = categories
			});
		}
		public async Task<IActionResult> CreateProduct()
		{
			ViewBag.Categories = await _context.ProductCategory.ToListAsync();
			return View();
		}
		public IActionResult CreateCategory()
		{
			return View();
		}
		public async Task<IActionResult> EditProduct(int id)
		{
			var aboutUs = await _context.Products.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditProduct>(aboutUs);
			ViewBag.Categories = await _context.ProductCategory.ToListAsync();
			ViewBag.Image = aboutUs.Image;
			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditCategory(int id)
		{
			var aboutUs = await _context.ProductCategory.FindAsync(id);
			if (aboutUs == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditProductCategory>(aboutUs);

			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> CreateProduct(EditProduct model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<Product>(model);
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
		public async Task<IActionResult> CreateCategory(CreateProductCategory model)
		{
			if (ModelState.IsValid)
			{
				var category = _mapper.Map<ProductCategory>(model);
				await _context.AddAsync(category);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditProduct(EditProduct model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<Product>(model);
				var currentProject = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
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
		public async Task<IActionResult> EditCategory(EditProductCategory model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<ProductCategory>(model);
				_context.Update(project);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Products.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Products.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (_context.ProductCategory == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.ProductCategory.FindAsync(id);
			if (aboutUs != null)
			{
				_context.ProductCategory.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
