namespace IraqWebsite.ViewModels.Projects
{
	public class ProjectCategoryDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public IList<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
	}
}
