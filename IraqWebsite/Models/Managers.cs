namespace IraqWebsite.Models
{
	public class Managers : BaseModel<int>
	{
		public string Name { get; set; } = string.Empty;
		public string NameAr { get; set; } = string.Empty;
		public string Linkden { get; set; } = string.Empty;
		public string Position { get; set; } = string.Empty;
		public string PositionAr { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
	}
}
