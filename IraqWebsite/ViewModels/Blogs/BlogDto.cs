namespace IraqWebsite.ViewModels.Blogs
{
    public class BlogDto
    {
		public int Id { get; set; }
		public string Image { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public IList<TagDto> Tags { get; set; } = default!;
		public BlogCategoryDto Category { get; set; } = default!;
	}
}
