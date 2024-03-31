namespace IraqWebsite.ViewModels.AcadmeicTraining
{
    public class EditAcadmeicTrainingDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string TitleAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public IFormFile? ImageOne { get; set; }
		public IFormFile? ImageTwo { get; set; }
		public IFormFile? ImageThree { get; set; }
	}
}
