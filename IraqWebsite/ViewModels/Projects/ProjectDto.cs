using IraqWebsite.ViewModels.ProjectSection;

namespace IraqWebsite.ViewModels.Projects
{
	public class ProjectDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Clinet { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public string Description { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public ProjectCategoryDto? Category { get; set; } = default!;
		public ProjectSectionDto ProjectSection { get; set; } = default!;
	}
}
