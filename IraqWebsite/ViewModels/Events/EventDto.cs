
namespace IraqWebsite.ViewModels.Events
{
	public class EventDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public string Image { get; set; } = string.Empty;
		public string ImageTwo { get; set; } = string.Empty;
		public string ImageThree { get; set; } = string.Empty;
		public EventCategoryDto Category { get; set; }
	}
}
