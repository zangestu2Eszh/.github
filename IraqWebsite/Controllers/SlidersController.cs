using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Slider;
using AutoMapper;
using IraqWebsite.Helper;
using Microsoft.AspNetCore.Authorization;
using static IraqWebsite.AuthManager.Statics.Permissions;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class SlidersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SlidersController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Policy = SliderPolicy.View)]
        // GET: Sliders
        public async Task<IActionResult> Index()
        {
              return _context.Slider != null ? 
                          View(await _context.Slider.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
        }

        [Authorize(Policy = SliderPolicy.Create)]
        // GET: Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = SliderPolicy.Create)]
        public async Task<IActionResult> Create(CreateSliderVm sliderVm)
        {
            if (ModelState.IsValid)
            {
                var slider = _mapper.Map<Slider>(sliderVm);
                var img = await FilesHelper.ValdiateFilesAsync(sliderVm.Img, _webHostEnvironment.WebRootPath);
                if (img != "Successful Saved")
                {
                    ModelState.AddModelError("", img);
                    return View(sliderVm);
                }
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sliderVm);
        }

        // GET: Sliders/Edit/5
        [Authorize(Policy = SliderPolicy.Edit)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            var sliderVm = _mapper.Map<EditSliderVm>(slider);
            ViewBag.Img = slider.Img;
            if (sliderVm == null)
            {
                return NotFound();
            }
            return View(sliderVm);
        }

        // POST: Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = SliderPolicy.Edit)]
        public async Task<IActionResult> Edit(EditSliderVm sliderVm)
        {
            if (sliderVm == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var slider = _mapper.Map<Slider>(sliderVm);
                    var sliderDto = await _context.Slider.Where(x => x.Id == slider.Id).Select(x => new { x.Img, x.CreatedDate }).FirstOrDefaultAsync();
                    if (sliderVm.Img is not null && slider.Img != sliderDto.Img)
                    {
                        var img = await FilesHelper.ValdiateFilesAsync(sliderVm.Img, _webHostEnvironment.WebRootPath);
                        if (img != "Successful Saved")
                        {
                            ModelState.AddModelError("", img);
                            return View(sliderVm);
                        }
                    }
                    else
                    {
                        slider.Img = sliderDto.Img;
                    }
                    slider.CreatedDate = sliderDto.CreatedDate;

                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(sliderVm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sliderVm);
        }

        // POST: Sliders/Delete/5
        [HttpPost]
        [Authorize(Policy = SliderPolicy.Delete)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Slider == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
            }
            var slider = await _context.Slider.FindAsync(id);
            if (slider != null)
            {
                _context.Slider.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(Guid id)
        {
          return (_context.Slider?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
