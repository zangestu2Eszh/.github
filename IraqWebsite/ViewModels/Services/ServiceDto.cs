using IraqWebsite.ViewModels.ServiceSection;

namespace IraqWebsite.ViewModels.Services
{
    public class ServiceDto
    {
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public string ImageTwo { get; set; } = string.Empty;
		public string ImageThree { get; set; } = string.Empty;
		public string Icon { get; set; } = string.Empty;
		public string IconWhite { get; set; } = string.Empty;
		public ServiceSectionDto ServiceSection { get; set; } = default!;
		public bool ServiceFeature { get; set; } = true;
		public string? ServiceFeatureOne { get; set; }
		public string? ServiceFeatureTwo { get; set; }
		public string? ServiceFeatureThree { get; set; }
		public string? ServiceFeatureFour { get; set; }
	}
}
