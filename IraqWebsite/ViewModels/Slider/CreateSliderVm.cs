namespace IraqWebsite.ViewModels.Slider
{
    public class CreateSliderVm
    {
        public IFormFile? Img { get; set; }
        public string? Title { get; set; }
        public string? TitleAr { get; set; }
        public string? SubTitle { get; set; }
        public string? SubTitleAr { get; set; }
        public bool ShowBtn { get; set; }
        public string? ButtonLink { get; set; }
    }
}