
using System.ComponentModel.DataAnnotations;

namespace IraqWebsite.AuthManager.ViewModels
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<RoleClaimViewModel> RoleClaims { get; set; }
    }

    public class RoleClaimViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}
