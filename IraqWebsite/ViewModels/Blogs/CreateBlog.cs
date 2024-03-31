namespace IraqWebsite.ViewModels.Blogs
{
    public class CreateBlog
    {
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public IList<CreateTagDto>? Tags { get; set; } = default!;
		public IFormFile Image { get; set; }
		public int BlogCategoryId { get; set; }
		public int BlogSectionId { get; set; }

	}
}
