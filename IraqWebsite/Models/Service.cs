namespace IraqWebsite.Models
{
	public class Service : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public string ImageTwo { get; set; } = string.Empty;
		public string ImageThree { get; set; } = string.Empty;
		public string Icon { get; set; } = string.Empty;
		public string IconWhite { get; set; } = string.Empty;
		public int ServiceSectionId { get; set; }
		public ServiceSection ServiceSection { get; set; } = default!;
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
