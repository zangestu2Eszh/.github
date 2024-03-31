using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.Services.GoogleReCaptchaService;
using System.Text.RegularExpressions;
using IraqWebsite.Helper;
using Microsoft.AspNetCore.Authorization;
using static IraqWebsite.AuthManager.Statics.Permissions;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class SubscribersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGoogleRecaptchaService _captchaService;

        public SubscribersController(ApplicationDbContext context, IGoogleRecaptchaService captchaService)
        {
            _context = context;
            _captchaService = captchaService;
        }

        // GET: Subscribers
        [Authorize(Policy = SubscribersPolicy.View)]
        public async Task<IActionResult> Index()
        {
              return _context.Subscribers != null ? 
                          View(await _context.Subscribers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Subscribers'  is null.");
        }

        // GET: Subscribers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscribers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string Email,string token)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                if (!ValidateUsingRegex(Email))
                {
                    return BadRequest("Invalid Email");
                }

                var captchaResult = await _captchaService.VerfiyToken(token);
                if (!captchaResult)
                {
                    return BadRequest("Google ReCaptcha Require Refresh The Page");
                }

                if (await _context.Subscribers.Where(x => x.Email == Email).AnyAsync())
                {
                    return BadRequest("You are Alrady Subsecribed");
                }

                Subscribers subscriber = new Subscribers
                {
                    Id = Guid.NewGuid(),
                    Email = Email,
                    JoinedDate = DateTime.Now,
                };
                _context.Add(subscriber);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return BadRequest("Enter Valid Email!");
        }

        private bool ValidateUsingRegex(string emailAddress)
        {
            if (emailAddress == null)
                return false;
            var pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        // POST: Subscribers/Delete/5
        [Authorize(Policy = SubscribersPolicy.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_context.Subscribers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Subscribers'  is null.");
            }
            var subscribers = await _context.Subscribers.FindAsync(id);
            if (subscribers != null)
            {
                _context.Subscribers.Remove(subscribers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscribersExists(Guid id)
        {
          return (_context.Subscribers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
