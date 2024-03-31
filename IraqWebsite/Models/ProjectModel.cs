namespace IraqWebsite.Models
{
	public class ProjectModel  : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string? Clinet { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string AddressAr { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public int? CategoryId { get; set; }
		public ProjectCategory? Category { get; set; } = default!;
		public int ProjectSectionId { get; set; }
		public ProjectSection ProjectSection { get; set; } = default!;
	}
}
