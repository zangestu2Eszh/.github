using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<BlogsController> _localizer;

        public BlogsController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<BlogsController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index(int? categoryId)
        {
            ViewData["ReadMore"] = _localizer["Read More"];
            ViewData["Blogs"] = _localizer["Blogs"];
            ViewData["Cateogries"] = _localizer["Cateogries"];
            IQueryable<Blog> blogs = _context.Blogs.OrderByDescending(x => x.CreatedDate).Include(x => x.Category);
            if(categoryId != null)
            {
                blogs.Where(x => x.BlogCategoryId == categoryId);
            }
            
            var aboutUs = _mapper.Map<List<BlogDto>>(await blogs.ToListAsync());
            ViewBag.Categories = _mapper.Map<List<BlogCategoryDto>>(await _context.BlogCategory.ToListAsync());
            return View(aboutUs);
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Cateogries"] = _localizer["Cateogries"];
            ViewData["LatestBlogs"] = _localizer["Latest Blogs"];

            if (id == 0) {
                return NotFound();
            }
            var aboutUs = _mapper.Map<BlogDto>(await _context.Blogs.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id == id));
            if (aboutUs == null)
            {
                return NotFound();
            }
            ViewBag.Blogs = _mapper.Map<List<BlogDto>>(await _context.Blogs.Where(x=>x.Id != id).Include(x=>x.Category).Take(8).ToListAsync());
            ViewBag.Categories = _mapper.Map<List<BlogCategoryDto>>(await _context.BlogCategory.ToListAsync());
            return View(aboutUs);
        }
    }
}
