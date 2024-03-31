using IraqWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Helper
{
    public class HeadViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public HeadViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult>  InvokeAsync()
        {
            ViewData["Appearance"] = await _context.Appearance.FirstOrDefaultAsync();
            string metaKeyWord = await _context.MetaKeyWord.Select(x=>x.Key).FirstOrDefaultAsync() ?? "AUIB FM";
            ViewData["MetaKeyWord"] = metaKeyWord.Replace(" ",",");
            return View();
        }
    }
}
