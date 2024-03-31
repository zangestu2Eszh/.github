namespace IraqWebsite.ViewModels.Blogs
{
    public class EditBlog
    {
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public IList<CreateTagDto>? Tags { get; set; } = default!;
		public int BlogCategoryId { get; set; }
		public IFormFile? Image { get; set; }
	}
}
