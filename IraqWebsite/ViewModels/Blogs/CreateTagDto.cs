namespace IraqWebsite.ViewModels.Blogs
{
	public class CreateTagDto
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public int? BlogId { get; set; }
	}
}
