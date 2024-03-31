using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.ViewModels.ContactUs
{
    public class SendMessageVm
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "FullName Feild Is Required")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email Feild Is Required")]
        [EmailAddress(ErrorMessage = "Valid Email Is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Message Feild Is Required")]
        public string? Message { get; set; }
        [Required(ErrorMessage = "Google Recaptcha Vervication Is Required")]
        public string? Token { get; set; }
    }
}
