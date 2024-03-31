using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.Helper;
using Microsoft.AspNetCore.Authorization;
using static IraqWebsite.AuthManager.Statics.Permissions;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class GoogleRecaptchaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoogleRecaptchaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = RecaptchaPolicy.Create)]
        // GET: GoogleRecaptcha/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GoogleRecaptcha/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = RecaptchaPolicy.Create)]
        public async Task<IActionResult> Create([Bind("Id,SiteKey,SecretKey,IsActive")] GoogleRecaptcha googleRecaptcha)
        {
            if (ModelState.IsValid)
            {
                googleRecaptcha.Id = Guid.NewGuid();
                _context.Add(googleRecaptcha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit));
            }
            return View(googleRecaptcha);
        }

        // GET: GoogleRecaptcha/Edit/5
        [Authorize(Policy = RecaptchaPolicy.Edit)]
        public async Task<IActionResult> Edit()
        {
            if (_context.GoogleRecaptcha == null)
            {
                return NotFound();
            }

            var googleRecaptcha = await _context.GoogleRecaptcha.FirstOrDefaultAsync();
            if (googleRecaptcha == null)
            {
                return RedirectToAction(nameof(Create));
            }
            var captcha = new GoogleRecaptcha
            {
                Id = googleRecaptcha.Id,
                SiteKey = googleRecaptcha.SiteKey,
            };
            return View(captcha);
        }

        // POST: GoogleRecaptcha/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = RecaptchaPolicy.Edit)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SiteKey,SecretKey,IsActive")] GoogleRecaptcha googleRecaptcha)
        {
            if (id != googleRecaptcha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dto = await _context.GoogleRecaptcha.FindAsync(id);
                    if(dto == null)
                        return NotFound();
                    dto.SiteKey = googleRecaptcha.SiteKey;
                    if(googleRecaptcha.SecretKey is not null)
                        dto.SecretKey = googleRecaptcha.SecretKey;
                    dto.IsActive = googleRecaptcha.IsActive;
                    _context.Update(dto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoogleRecaptchaExists(googleRecaptcha.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(googleRecaptcha);
        }

        private bool GoogleRecaptchaExists(Guid id)
        {
          return (_context.GoogleRecaptcha?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
