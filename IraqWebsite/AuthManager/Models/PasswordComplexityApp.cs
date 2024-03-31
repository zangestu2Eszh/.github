namespace IraqWebsite.AuthManager.Models
{
    public class PasswordComplexityApp
    {
        public Guid Id { get; set; }
        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool UseDefaultSettings { get; set; }
        public bool Require_non_alphanumeric { get; set; }
    }
}
