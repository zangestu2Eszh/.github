using IraqWebsite.ViewModels.Services;

namespace IraqWebsite.ViewModels.ServiceSection
{
	public class ServiceSectionDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public IList<ServiceDto> Services { get; set; } = default!;
	}
}
