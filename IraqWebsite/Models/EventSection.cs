namespace IraqWebsite.Models
{
	public class EventSection : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public string SubTitleAr { get; set; } = string.Empty;
		public IList<Event> Events { get; set; } = default!;
	}
}
