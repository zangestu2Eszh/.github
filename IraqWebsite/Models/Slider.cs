namespace IraqWebsite.Models
{
    public class Slider: BaseModel<Guid>
    {
        public string? Img { get; set; }
        public string? Title { get; set; }
        public string? TitleAr { get; set; }
        public string? SubTitle { get; set; }
        public string? SubTitleAr { get; set; }
        public bool ShowBtn { get; set; }
        public string? ButtonLink { get; set; }
    }
}
