namespace IraqWebsite.Models
{
	public class ProjectCategory : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public IList<ProjectModel>? Projects { get; set; } = new List<ProjectModel>();
	}
}
