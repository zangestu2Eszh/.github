namespace IraqWebsite.ViewModels.Events
{
	public class CreateEvent
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string SubDescription { get; set; } = string.Empty;
		public string SubDescriptionAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public IFormFile Image { get; set; }
		public IFormFile ImageTwo { get; set; }
		public IFormFile ImageThree { get; set; }
		public int EventSectionId { get; set; }
		public int EventCategoryId { get; set; }
	}
}
