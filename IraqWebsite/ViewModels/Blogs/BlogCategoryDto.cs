namespace IraqWebsite.ViewModels.Blogs
{
    public class BlogCategoryDto
    {
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public IList<BlogDto> Blogs { get; set; } = default!;
	}
}
