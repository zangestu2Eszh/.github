namespace IraqWebsite.Models
{
	public class Event : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public string Image { get; set; } = string.Empty;
		public string ImageTwo { get; set; } = string.Empty;
		public string ImageThree { get; set; } = string.Empty;
		public int EventCategoryId { get; set; }
		public EventCategory Category { get; set; }
		public int EventSectionId { get; set; }
		public EventSection EventSection { get; set; } = default!;
	}
}
