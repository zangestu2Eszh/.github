namespace IraqWebsite.Models
{
	public class Appointment : BaseModel<int>
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public string Message { get; set; } = string.Empty;
	}
}
