namespace IraqWebsite.ViewModels.Partner
{
    public class EditPartnerVm
    {
        public Guid Id { get; set; }
        public string? Link { get; set; }
        public IFormFile? Img { get; set; }
        public bool IsActive { get; set; }
    }
}
