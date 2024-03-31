namespace IraqWebsite.Models
{
    public class Clinet :BaseModel<int>
    {
        public string? Link { get; set; }
        public string? Img { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
