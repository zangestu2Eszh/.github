using IraqWebsite.Models;

namespace IraqWebsite.Models
{
	public class AcadmeicTraining : BaseModel<int>
	{
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public string ImageOne { get; set; } = string.Empty;
		public string ImageTwo { get; set; } = string.Empty;
		public string ImageThree { get; set; } = string.Empty;
	}
}
