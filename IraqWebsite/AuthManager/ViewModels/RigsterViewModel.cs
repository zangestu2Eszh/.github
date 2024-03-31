using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.AuthManager.ViewModels
{
    public class RigsterViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone")]
        public string? Phone { get; set; } = string.Empty;

        [Display(Name = "IpPhone")]
        public string? IpPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Required")]
        [Compare("Password", ErrorMessage = "Must Equal to Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Company { get; set; }
        public string? Birthday { get; set; }
        public string? Gender { get; set; }
    }
}
