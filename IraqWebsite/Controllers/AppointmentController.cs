using IraqWebsite.Data;
using IraqWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Controllers
{
	public class AppointmentController : Controller
	{
		private readonly ApplicationDbContext _context;
		public AppointmentController(ApplicationDbContext context)
		{
			_context = context;
		}
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var appointments = await _context.Appointments.OrderByDescending(x => x.CreatedDate).ToListAsync();
			return View(appointments);
		}
		[HttpPost]
		public async Task<IActionResult> Appointment(Appointment model)
		{
			if(ModelState.IsValid)
			{
				await _context.Appointments.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}
			return BadRequest();
		}
	}
}
