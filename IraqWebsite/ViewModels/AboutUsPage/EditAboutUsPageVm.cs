namespace IraqWebsite.ViewModels.AboutUs
{
    public class EditAboutUsPageVm
    {
		public int Id { get; set; }
		public IFormFile? ImageSectionOne { get; set; }
		public IFormFile? ImageOurVision { get; set; }
		public IFormFile? ImageOurGoal { get; set; }
		public IFormFile? ImageOurMission { get; set; }
		public string DescriptionOurMission { get; set; } = string.Empty;
		public string DescriptionArOurMission { get; set; } = string.Empty;
		public string DescriptionOurGoal { get; set; } = string.Empty;
		public string DescriptionArOurGoal { get; set; } = string.Empty;
		public string DescriptionOurVision { get; set; } = string.Empty;
		public string DescriptionArOurVision { get; set; } = string.Empty;
		public string ManagerWordTitle { get; set; } = string.Empty;
		public string ManagerWordTitleAr { get; set; } = string.Empty;
		public string ManagerWordDescription { get; set; } = string.Empty;
		public string ManagerWordDescriptionAr { get; set; } = string.Empty;
		public IFormFile? ImageManagerWord { get; set; }
		public string CompanySkillTitle { get; set; } = string.Empty;
		public string CompanySkillTitleAr { get; set; } = string.Empty;
		public string CompanySkillDescription { get; set; } = string.Empty;
		public string CompanySkillDescriptionAr { get; set; } = string.Empty;
		public IFormFile? CompanySkillImageOne { get; set; }
		public IFormFile? CompanySkillImageTwo { get; set; }
		public IFormFile? CompanySkillImageThree { get; set; }
	}
}
