using IraqWebsite.ViewModels.Events;
using IraqWebsite.ViewModels.ProjectSection;

namespace IraqWebsite.ViewModels.EventSection
{
	public class EventAdmin
	{
		public ICollection<IraqWebsite.Models.EventSection> Sections { get; set; } = new List<IraqWebsite.Models.EventSection>();
		public ICollection<IraqWebsite.Models.Event> Events { get; set; } = new List<IraqWebsite.Models.Event>();
		public ICollection<IraqWebsite.Models.EventCategory> Categories { get; set; } = new List<IraqWebsite.Models.EventCategory>();
	}
}
