using IraqWebsite.ViewModels.ProjectSection;

namespace IraqWebsite.ViewModels.Products
{
	public class ProductDto
	{
		public string Title { get; set; } = string.Empty;
		public string Price { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public ProductCategoryDto Category { get; set; }
	}
}
