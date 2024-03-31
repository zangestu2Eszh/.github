namespace IraqWebsite.AuthManager.ViewModels
{
    public class UserFilterViewModel
    {
        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? PhoneNumber { get; set; } = "";
        public string? IpPhone { get; set; } = "";
        public string? Company { get; set; } = "";
        public string? RoleName { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
