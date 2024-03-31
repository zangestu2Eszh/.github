namespace IraqWebsite.ViewModels.Statiscs
{
	public class EditStatisc
	{
		public int Id { get; set; }
		public IFormFile? Image { get; set; }
		public IFormFile? StatiscOneIcon { get; set; }
		public string StatiscOneTitle { get; set; } = string.Empty;
		public string StatiscOneTitleAr { get; set; } = string.Empty;
		public string StatiscOneNumber { get; set; } = string.Empty;
		public IFormFile? StatiscTwoIcon { get; set; }
		public string StatiscTwoTitle { get; set; } = string.Empty;
		public string StatiscTwoTitleAr { get; set; } = string.Empty;
		public string StatiscTwoNumber { get; set; } = string.Empty;
		public IFormFile? StatiscThreeIcon { get; set; }
		public string StatiscThreeTitle { get; set; } = string.Empty;
		public string StatiscThreeTitleAr { get; set; } = string.Empty;
		public string StatiscThreeNumber { get; set; } = string.Empty;
		public IFormFile? StatiscFourIcon { get; set; }
		public string StatiscFourTitle { get; set; } = string.Empty;
		public string StatiscFourTitleAr { get; set; } = string.Empty;
		public string StatiscFourNumber { get; set; } = string.Empty;
	}
}
