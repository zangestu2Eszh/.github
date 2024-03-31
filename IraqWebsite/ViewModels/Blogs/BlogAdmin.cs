namespace IraqWebsite.ViewModels.Blogs
{
	public class BlogAdmin
	{
		public ICollection<IraqWebsite.Models.BlogSection> Sections { get; set; } = new List<IraqWebsite.Models.BlogSection>();
		public ICollection<IraqWebsite.Models.Blog> Blogs { get; set; } = new List<IraqWebsite.Models.Blog>();
		public ICollection<IraqWebsite.Models.BlogCategory> Categories { get; set; } = new List<IraqWebsite.Models.BlogCategory>();
	}
}
