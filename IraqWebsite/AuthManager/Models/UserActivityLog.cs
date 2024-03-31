namespace IraqWebsite.AuthManager.Models
{
    public class UserActivityLog
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string AreaAccessed { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Status { get; set; }
    }
}
