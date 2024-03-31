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
    public class MetaKeyWordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MetaKeyWordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MetaKeyWord/Create
        [Authorize(Policy = MetaPolicy.Create)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetaKeyWord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = MetaPolicy.Create)]
        public async Task<IActionResult> Create([Bind("Id,Key")] MetaKeyWord metaKeyWord)
        {
            if (ModelState.IsValid)
            {
                metaKeyWord.Id = Guid.NewGuid();
                _context.Add(metaKeyWord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit));
            }
            return View(metaKeyWord);
        }

        // GET: MetaKeyWord/Edit/5
        [Authorize(Policy = MetaPolicy.Edit)]
        public async Task<IActionResult> Edit()
        {
            if (_context.MetaKeyWord == null)
            {
                return NotFound();
            }

            var metaKeyWord = await _context.MetaKeyWord.FirstOrDefaultAsync();
            if (metaKeyWord == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(metaKeyWord);
        }

        // POST: MetaKeyWord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = MetaPolicy.Edit)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Key")] MetaKeyWord metaKeyWord)
        {
            if (id != metaKeyWord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metaKeyWord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetaKeyWordExists(metaKeyWord.Id))
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
            return View(metaKeyWord);
        }

        private bool MetaKeyWordExists(Guid id)
        {
          return (_context.MetaKeyWord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
