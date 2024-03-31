namespace IraqWebsite.Models
{
	public class AboutUsPage : BaseModel<int>
	{
		public string ImageSectionOne { get; set; } = string.Empty;
		public string ImageOurVision { get; set; } = string.Empty;
		public string ImageOurGoal { get; set; } = string.Empty;
		public string ImageOurMission { get; set; } = string.Empty;
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
		public string ImageManagerWord { get; set; } = string.Empty;
		public string CompanySkillTitle { get; set; } = string.Empty;
		public string CompanySkillTitleAr { get; set; } = string.Empty;
		public string CompanySkillDescription { get; set; } = string.Empty;
		public string CompanySkillDescriptionAr { get; set; } = string.Empty;
		public string CompanySkillImageOne { get; set; } = string.Empty;
		public string CompanySkillImageTwo { get; set; } = string.Empty;
		public string CompanySkillImageThree { get; set; } = string.Empty;
	}
}
