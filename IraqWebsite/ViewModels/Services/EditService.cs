namespace IraqWebsite.ViewModels.Services
{
    public class EditService
    {
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public IFormFile? Image { get; set; }
		public IFormFile? ImageTwo { get; set; }
		public IFormFile? ImageThree { get; set; }
		public IFormFile? Icon { get; set; }
		public IFormFile? IconWhite { get; set; }
		public int ServiceSectionId { get; set; }
		public bool ServiceFeature { get; set; } = true;
		public string? ServiceFeatureOne { get; set; }
		public string? ServiceFeatureOneAr { get; set; }
		public string? ServiceFeatureTwo { get; set; }
		public string? ServiceFeatureTwoAr { get; set; }
		public string? ServiceFeatureThree { get; set; }
		public string? ServiceFeatureThreeAr { get; set; }
		public string? ServiceFeatureFour { get; set; }
		public string? ServiceFeatureFourAr { get; set; }
	}
}
