namespace IraqWebsite.ViewModels.Products
{
	public class CreateProduct
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string Price { get; set; } = string.Empty;
		public IFormFile Image { get; set; }
		public int? CategoryId { get; set; }
	}
}
