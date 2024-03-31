namespace IraqWebsite.ViewModels.AboutUs
{
    public class AboutUsDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SubTitle { get; set; }
        public int YearsExperience { get; set; }
        public string? ImgOne { get; set; }
        public string? ImgTwo { get; set; }
        public string ImgThree { get; set; }
        public bool IsActive { get; set; } = true;
        public string? FeatureOneTitle { get; set; } = string.Empty;
        public string? FeatureTwoTitle { get; set; } = string.Empty;
        public string? FeatureThreeTitle { get; set; } = string.Empty;
        public string? FeatureFourTitle { get; set; } = string.Empty;
    }
}
