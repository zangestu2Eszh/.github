namespace IraqWebsite.ViewModels.AboutUs
{
	public class AboutUsAdmin
	{
		public ICollection<IraqWebsite.Models.AboutUs> Sections { get; set; } = new List<IraqWebsite.Models.AboutUs>();
		public ICollection<IraqWebsite.Models.AboutUsPage> AboutUsPages { get; set; } = new List<IraqWebsite.Models.AboutUsPage>();
	}
}
