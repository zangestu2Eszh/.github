using IraqWebsite.Data;
using IraqWebsite.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static IraqWebsite.AuthManager.Statics.Permissions;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class Admin : Controller
    {
        private readonly ApplicationDbContext _context;
        public Admin(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = Dashboard.View)]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
