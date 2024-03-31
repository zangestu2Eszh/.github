namespace IraqWebsite.Models
{
    public class AboutUs : BaseModel<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string SubTitleAr { get; set; } = string.Empty;
        public int YearsExperience { get; set; }
        public string? ImgOne { get; set; }
        public string? ImgTwo { get; set; }
        public string? ImgThree { get; set; }
        public bool IsActive { get; set; } = true;
        public string? FeatureOneTitle { get; set; } = string.Empty;
        public string? FeatureOneTitleAr { get; set; } = string.Empty;
        public string? FeatureTwoTitle { get; set; } = string.Empty;
        public string? FeatureTwoTitleAr { get; set; } = string.Empty;
        public string? FeatureThreeTitle { get; set; } = string.Empty;
        public string? FeatureThreeTitleAr { get; set; } = string.Empty;
        public string? FeatureFourTitle { get; set; } = string.Empty;
        public string? FeatureFourTitleAr { get; set; } = string.Empty;
    }
}
