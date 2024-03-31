using IraqWebsite.ViewModels.Blogs;

namespace IraqWebsite.ViewModels.BlogSection
{
	public class BlogSectionDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public ICollection<BlogDto> Blogs { get; set; } = new List<BlogDto>();
	}
}
