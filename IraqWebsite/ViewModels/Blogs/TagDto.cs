namespace IraqWebsite.ViewModels.Blogs
{
    public class TagDto
    {
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public int BlogId { get; set; }
		public BlogDto Blog { get; set; } = default!;
	}
}
