namespace IraqWebsite.Models
{
	public class Structure :  BaseModel<int>
	{
		public string SectionImage { get; set; } = string.Empty;
		public string StructureImage { get; set; } = string.Empty;
	}
}
