using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.Models
{
    public class CustemerReview
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "FullName Feild Is Required")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email Feild Is Required")]
        [EmailAddress(ErrorMessage = "Valid Email Is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Message Feild Is Required")]
        public string? Message { get; set; }
        public DateTime? SentDate { get; set; } = DateTime.Now;
    }
}
