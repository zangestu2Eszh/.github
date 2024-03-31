namespace IraqWebsite.Models
{
	public class Blog : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public IList<Tag>? Tags { get; set; } = default!;
		public int BlogCategoryId { get; set; }
		public BlogCategory Category { get; set; } = default!;
		public int BlogSectionId { get; set; }
		public BlogSection BlogSection { get; set; } = default!;
	}
}
