using IraqWebsite.ViewModels.Blogs;

namespace IraqWebsite.ViewModels.Managers
{
	public class ManagerDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Linkden { get; set; } = string.Empty;
		public string Position { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
	}
}
