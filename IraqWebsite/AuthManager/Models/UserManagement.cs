namespace IraqWebsite.AuthManager.Models
{
    public class UserManagement
    {
        public Guid Id { get; set; }
        public bool IsAllowedToRegister { get; set; }
        public bool IsActiveByDeffult { get; set; }
        public bool UseGoogleRecaphaOnLogin { get; set; }
        public bool UseGoogleRecaphaOnRegister { get; set; }
    }
}
