using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Blogs;
using IraqWebsite.ViewModels.BlogSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	[ServiceFilter(typeof(ActivitiesLog))]
	[Authorize]
	public class BlogAdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public BlogAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			var section = await _context.BlogSection.ToListAsync();
			var blogs = await _context.Blogs.ToListAsync();
			var categories = await _context.BlogCategory.ToListAsync();
			return View(new BlogAdmin
			{
				Sections = section,
				Blogs = blogs,
				Categories = categories
			});
		}
		public IActionResult CreateSection()
		{
			return View();
		}
		public async Task<IActionResult> CreateBlog()
		{
			ViewBag.Categories = await _context.BlogCategory.ToListAsync();
			return View();
		}
		public IActionResult CreateCategory()
		{
			return View();
		}
		public async Task<IActionResult> EditSection(int id)
		{
			var blogSection = await _context.BlogSection.FindAsync(id);
			if (blogSection == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditBlogSection>(blogSection);
			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditBlog(int id)
		{
			var blog = await _context.Blogs.FindAsync(id);
			if (blog == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditBlog>(blog);
			ViewBag.Categories = await _context.BlogCategory.ToListAsync();
			ViewBag.Image = blog.Image;
			return View(aboutUsVm);
		}
		public async Task<IActionResult> EditCategory(int id)
		{
			var blog = await _context.BlogCategory.FindAsync(id);
			if (blog == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var aboutUsVm = _mapper.Map<EditBlogCategoryDto>(blog);
			return View(aboutUsVm);
		}
		[HttpPost]
		public async Task<IActionResult> CreateBlog(CreateBlog model)
		{
			if (ModelState.IsValid)
			{
				var section = await _context.BlogSection.FirstOrDefaultAsync();
				model.BlogSectionId = section.Id;	
				var blog = _mapper.Map<Blog>(model);
				var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
				if (img != "Successful Saved")
				{
					ModelState.AddModelError("", img);
					return View(model);
				}
				await _context.AddAsync(blog);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateSection(CreateBlogSection model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<BlogSection>(model);
				await _context.AddAsync(blog);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateBlogCategoryDto model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<BlogCategory>(model);
				await _context.AddAsync(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditBlog(EditBlog model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<Blog>(model);
				var currentBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == model.Id);
				if (model.Image != null && currentBlog?.Image != blog.Image)
				{
					var img = await FilesHelper.ValdiateFilesAsync(model.Image, _webHostEnvironment.WebRootPath);
					if (img != "Successful Saved")
					{
						ModelState.AddModelError("", img);
						return RedirectToAction(nameof(EditBlog));
					}
				}
                else
                {
					blog.Image = currentBlog.Image;
                }
				blog.BlogSectionId = currentBlog.BlogSectionId; 
				_context.Update(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(EditBlog));
		}
		[HttpPost]
		public async Task<IActionResult> EditSection(EditBlogSection model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<BlogSection>(model);
				_context.Update(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditCategory(EditBlogCategoryDto model)
		{
			if (ModelState.IsValid)
			{
				var blog = _mapper.Map<BlogCategory>(model);
				_context.Update(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Blogs == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.Blogs.FindAsync(id);
			if (aboutUs != null)
			{
				_context.Blogs.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		//[Authorize(Policy = AboutPolicy.Delete)]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (_context.BlogCategory == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
			}
			var aboutUs = await _context.BlogCategory.FindAsync(id);
			if (aboutUs != null)
			{
				_context.BlogCategory.Remove(aboutUs);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
