using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Data;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.ContactUs;
using AutoMapper;
using IraqWebsite.Helper;
using Microsoft.AspNetCore.Authorization;
using static IraqWebsite.AuthManager.Statics.Permissions;
using IraqWebsite.Services.GoogleReCaptchaService;
using System.Net.Mail;
using IraqWebsite.Services.EmailService;
using IraqWebsite.Services.UserService;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;

namespace IraqWebsite.Controllers
{
    [ServiceFilter(typeof(ActivitiesLog))]
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGoogleRecaptchaService _captchaService;
        private readonly IEmailService _email;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ContactUsController> _localizer;

        public ContactUsController(ApplicationDbContext context, IMapper mapper, IGoogleRecaptchaService captchaService, IEmailService email, IUserService userService, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ContactUsController> localizer)
        {
            _context = context;
            _mapper = mapper;
            _captchaService = captchaService;
            _email = email;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        // GET: ContactUs
        [Authorize(Policy = ContactPolicy.View)]
        public async Task<IActionResult> Index()
        {
              return _context.ContactUs != null ? 
                          View(await _context.ContactUs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
        }

        [Authorize(Policy = ContactPolicy.View)]
        public async Task<IActionResult> Messages()
        {
              return _context.CustemerReview != null ? 
                          View(await _context.CustemerReview.OrderByDescending(x=>x.SentDate).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
        }

        public async Task<IActionResult> Info()
        {
            ViewData["EMAILUS"] = _localizer["EMAIL US"];
            ViewData["CALLUS"] = _localizer["CALL US"];
            ViewData["OFFICELOCATION"] = _localizer["OFFICE LOCATION"];
            ViewData["CONTACTUS"] = _localizer["CONTACT US"];
            ViewData["Email"] = _localizer["Email"];
            ViewData["Name"] = _localizer["Name"];
            ViewData["Message"] = _localizer["Message"];
            ViewData["Submit"] = _localizer["Submit"];
            ViewData["LeaveaMessage"] = _localizer["Leave a Message"];
            ViewData["We’reReadyToHelpYou"] = _localizer["We’re Ready To Help You"];
            ViewData["Subject"] = _localizer["Subject"];




            var result = _mapper.Map<ContactUsDto>(await _context.ContactUs.FirstOrDefaultAsync());
            return View(result);
        }

        // GET: ContactUs/Create
        [Authorize(Policy = ContactPolicy.Create)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ContactPolicy.Create)]
        public async Task<IActionResult> Create(CreateContactUsVm contactUsVm)
        {
            if (ModelState.IsValid)
            {
                var contactUs = _mapper.Map<ContactUs>(contactUsVm);
                var img = await FilesHelper.ValdiateFilesAsync(contactUsVm.Img, _webHostEnvironment.WebRootPath);
                if (img != "Successful Saved")
                {
                    ModelState.AddModelError("", img);
                    return View(contactUsVm);
                }
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        // GET: ContactUs/Edit/5
        [Authorize(Policy = ContactPolicy.Edit)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (_context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs.FirstOrDefaultAsync();
            if (contactUs == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(contactUs);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ContactPolicy.Edit)]
        public async Task<IActionResult> Edit(Guid id, ContactUs contactUs, IFormFile? file)
        {
            if (id != contactUs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(file != null)
                    {
                        var img = await FilesHelper.ValdiateFilesAsync(file, _webHostEnvironment.WebRootPath);
                        if (img != "Successful Saved")
                        {
                            ModelState.AddModelError("", img);
                            return View(contactUs);
                        }
                        contactUs.Img = "/Images/" + file.FileName;
                    }
                    contactUs.UpdateDate = DateTime.Now;
                    _context.Update(contactUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsExists(contactUs.Id))
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
            return RedirectToAction(nameof(Edit));
        }

        [HttpPost]
        public async Task<IActionResult> Review([FromForm] SendMessageVm messageVm)
        {
            try
            {
                
                if (messageVm is not null && messageVm.Email is not null && messageVm.Message is not null && messageVm.FullName is not null && messageVm.Token is not null)
                {
                    if (!ValidateUsingRegex(messageVm.Email))
                    {
                        return BadRequest("Invalid Email");
                    }
                    var captchaResult = await _captchaService.VerfiyToken(messageVm.Token);
                    if (!captchaResult)
                    {
                        return BadRequest("Google ReCaptcha Required Refresh The Page");
                    }
                    var review = _mapper.Map<CustemerReview>(messageVm);
                    await _context.CustemerReview.AddAsync(review);
                    await _context.SaveChangesAsync();

                    var users = await _context.Users.ToListAsync();
                    foreach (var user in users)
                    {
                        if (await _userService.CustemIsInRoleAsync(user, "Admin"))
                        {
                            await _email.CustemSendEmailAsync(user.Email, "AUIB FM Contact Form", "Name: " + messageVm.FullName + $"<br><br> Message: {messageVm.Message}", null, messageVm.Email);
                        }
                    }
                    return RedirectToAction("Info");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return BadRequest("Invalid Sumbit");
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ContactPolicy.Delete)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ContactUs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
            }
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs != null)
            {
                _context.ContactUs.Remove(contactUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(Guid id)
        {
          return (_context.ContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public bool ValidateUsingRegex(string emailAddress)
        {
            if (emailAddress == null)
                return false;
            var pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }
    }
}
