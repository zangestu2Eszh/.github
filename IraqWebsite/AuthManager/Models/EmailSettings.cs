using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.AuthManager.Models
{
    public class EmailSettings
    {
        public Guid Id { get; set; }
        [EmailAddress(ErrorMessage = "Invail Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string SmtpClient { get; set; }
        public int Port { get; set; }
        public bool EnableSSl { get; set; }
        public string LastModification { get; set; }
        public bool IsActive { get; set; }
    }
}
