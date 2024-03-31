namespace IraqWebsite.ViewModels.Projects
{
	public class CreateProject
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string? Clinet { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string AddressAr { get; set; } = string.Empty;
		public IFormFile Image { get; set; }
		public int? CategoryId { get; set; }
		public int ProjectSectionId { get; set; }
	}
}
