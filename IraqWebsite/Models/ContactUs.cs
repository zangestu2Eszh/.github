namespace IraqWebsite.Models
{
    public class ContactUs:BaseModel<Guid>
    {
        public string? Title { get; set; }
        public string? TitleAr { get; set; }
        public string? Img { get; set; }
        public string? Slogan { get; set; }
        public string? SloganAr { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? LocationAr { get; set; }
        public string? Linkden { get; set; }
        public string? Facebook { get; set; }
        public string? Instgram { get; set; }
    }
}
