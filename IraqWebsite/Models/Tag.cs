namespace IraqWebsite.Models
{
	public class Tag : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public int BlogId { get; set; }
		public Blog Blog { get; set; } = default!;
	}
}
