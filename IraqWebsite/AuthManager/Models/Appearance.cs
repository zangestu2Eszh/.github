namespace IraqWebsite.AuthManager.Models
{
    public class Appearance
    {
        public Guid Id { get; set; }
        public string? ApplicationName { get; set; }
        public string? ApplicationLogo { get; set; }
        public string? ColorWhite { get; set; }
        public string? ColorBlack { get; set; }
        public bool ColorMode { get; set; }
        public string? FavIcon { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
