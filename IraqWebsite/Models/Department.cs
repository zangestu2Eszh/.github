using IraqWebsite.Models;

namespace IraqWebsite.Models
{
	public class Department : BaseModel<int>
	{
		public string Name { get; set; } = string.Empty;
		public string NameAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public string ImageOne { get; set; } = string.Empty;
		public string ImageTwo { get; set; } = string.Empty;
	}
}
