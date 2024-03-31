namespace IraqWebsite.Models
{
	public class ServiceSection : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public string SubTitleAr { get; set; } = string.Empty;
		public IList<Service> Services { get; set; } = default!;
	}
}
