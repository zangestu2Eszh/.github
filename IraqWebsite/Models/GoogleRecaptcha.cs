namespace IraqWebsite.Models
{
    public class GoogleRecaptcha
    {
        public Guid Id { get; set; }
        public string? SiteKey { get; set; }
        public string? SecretKey { get; set; }
        public bool IsActive { get; set; }
    }
}
