namespace IraqWebsite.Models
{
	public class ProjectSection : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public string SubTitleAr { get; set; } = string.Empty;
		public IList<ProjectModel>? Projects { get; set; } = default!;
	}
}
