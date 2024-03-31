namespace IraqWebsite.ViewModels.ProjectSection
{
	public class ProjectAdmin
	{
		public ICollection<IraqWebsite.Models.ProjectSection> Sections { get; set; } = new List<IraqWebsite.Models.ProjectSection>();
		public ICollection<IraqWebsite.Models.ProjectModel> Projects { get; set; } = new List<IraqWebsite.Models.ProjectModel>();
		public ICollection<IraqWebsite.Models.ProjectCategory> Categories { get; set; } = new List<IraqWebsite.Models.ProjectCategory>();
	}
}
