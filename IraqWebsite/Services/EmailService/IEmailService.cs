namespace IraqWebsite.Services.EmailService
{
    public interface IEmailService
    {
        public Task CustemSendEmailAsync(string email, string subject, string htmlMessage, string link, string cardTitle);
    }
}
