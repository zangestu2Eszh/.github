using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.Models
{
    public class Subscribers
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime? JoinedDate { get; set; }
    }
}
