using IraqWebsite.ViewModels.EventSection;
using IraqWebsite.ViewModels.Events;

namespace IraqWebsite.ViewModels.EventSection
{
	public class EventSectionDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public IList<EventDto> Events { get; set; } = default!;
	}
}
