namespace IraqWebsite.ViewModels.Services
{
	public class ServiceAdmin
	{
		public ICollection<IraqWebsite.Models.ServiceSection> Sections { get; set; } = new List<IraqWebsite.Models.ServiceSection>();
		public ICollection<IraqWebsite.Models.Service> Services { get; set; } = new List<IraqWebsite.Models.Service>();
	}
}
