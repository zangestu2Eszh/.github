namespace IraqWebsite.Models
{
	public class Product  : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string Price { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public int? CategoryId { get; set; }
		public ProductCategory? Category { get; set; } = default!;
	}
}
