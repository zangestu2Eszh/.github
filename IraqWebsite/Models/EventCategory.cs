namespace IraqWebsite.Models
{
	public class EventCategory : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public IList<Event>? Events { get; set; } = new List<Event>();
	}
}
