namespace IraqWebsite.ViewModels.Products
{
	public class ProductCategoryDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
	}
}
