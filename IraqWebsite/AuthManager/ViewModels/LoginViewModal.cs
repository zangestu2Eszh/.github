using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.AuthManager.ViewModels
{
    public class LoginViewModal
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرجاء عدم ترك الحقل فارغ")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
