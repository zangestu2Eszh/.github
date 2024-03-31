namespace IraqWebsite.ViewModels.Products
{
	public class ProductAdmin
	{
		public ICollection<IraqWebsite.Models.Product> Products { get; set; } = new List<IraqWebsite.Models.Product>();
		public ICollection<IraqWebsite.Models.ProductCategory> Categories { get; set; } = new List<IraqWebsite.Models.ProductCategory>();
	}
}