using Microsoft.AspNetCore.Identity;

namespace IraqWebsite.AuthManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IpPhone { get; set; }
        public string? Company { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string? ProfilePicture { get; set; }
        public string? Gender { get; set; }
        public string? Birthday { get; set; }
        public string? LastModification { get; set; }
        public bool IsNewUser { get; set; }
    }
}

