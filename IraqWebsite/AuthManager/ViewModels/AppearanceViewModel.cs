namespace IraqWebsite.AuthManager.ViewModels
{
    public class AppearanceViewModel
    {
        public string? ApplicationName { get; set; }
        public string? ColorWhite { get; set; }
        public string? ColorBlack { get; set; }
        public bool ColorMode { get; set; }
        public IFormFile? logo { get; set; }
        public IFormFile? favIcon { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
