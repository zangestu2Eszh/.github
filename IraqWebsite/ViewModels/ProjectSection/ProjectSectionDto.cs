using IraqWebsite.ViewModels.Projects;

namespace IraqWebsite.ViewModels.ProjectSection
{
	public class ProjectSectionDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public IList<ProjectDto> Projects { get; set; } = default!;
	}
}
