using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.AuthManager.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "الرجاء عدم ترك الحقل فارغ")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "الرجاء عدم ترك الحقل فارغ")]
        public string ConfirmPassword { get; set; }
    }
}
