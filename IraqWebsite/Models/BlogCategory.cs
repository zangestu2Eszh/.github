namespace IraqWebsite.Models
{
	public class BlogCategory : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public IList<Blog> Blogs { get; set; } = default!;
	}
}
