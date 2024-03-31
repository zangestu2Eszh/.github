namespace IraqWebsite.ViewModels.Events
{
	public class EventCategoryDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public IList<EventDto> Events { get; set; } = new List<EventDto>();
	}
}
