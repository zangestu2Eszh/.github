namespace IraqWebsite.AuthManager.ViewModels
{
    public class ListUsersViewModel
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Roles { get; set; }
        public PermissionViewModel? RoleClaims { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
