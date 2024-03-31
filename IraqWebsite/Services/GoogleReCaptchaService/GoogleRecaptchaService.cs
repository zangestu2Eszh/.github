using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using IraqWebsite.Helper;
using IraqWebsite.ViewModels;
using IraqWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace IraqWebsite.Services.GoogleReCaptchaService
{
    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        //private readonly IOptionsMonitor<GoogleReCaptchaConfig> _config;
        private readonly ApplicationDbContext _context;
        public GoogleRecaptchaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> VerfiyToken(string token)
        {
            try
            {
                var captcha = await _context.GoogleRecaptcha.FirstOrDefaultAsync();

                string secretKey = "";

                if (captcha is not null)
                {
                    secretKey = captcha.SecretKey ?? "";
                    if (!captcha.IsActive)
                        return true;
                }
                else
                    return true;
                    

                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    var responseString = await httpResult.Content.ReadAsStringAsync();
                    var googleResult = JsonConvert.DeserializeObject<GoogleRecaptchaVm>(responseString);
                    if (googleResult.challenge_ts.AddMinutes(1) < DateTime.UtcNow)
                    {
                        return false;
                    }
                    return googleResult.success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
