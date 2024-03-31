namespace IraqWebsite.Services.GoogleReCaptchaService
{
    public interface IGoogleRecaptchaService
    {
        public Task<bool> VerfiyToken(string token);
    }
}
