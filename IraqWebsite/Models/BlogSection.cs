namespace IraqWebsite.Models
{
	public class BlogSection : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public string SubTitleAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public ICollection<Blog>? Blogs { get; set; } = new List<Blog>();
	}
}
