
using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.AuthManager.ViewModels
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? IpPhone { get; set; }
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "عنوان البريد الالكتروني غير صالح")]
        public string Email { get; set; }
        public string? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? Company { get; set; }
    }
}
