namespace IraqWebsite.Models
{
	public class ProductCategory : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public IList<Product>? Products { get; set; } = new List<Product>();
	}
}
