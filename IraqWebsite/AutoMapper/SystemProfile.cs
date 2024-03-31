using AutoMapper;
using IraqWebsite.AuthManager.ViewModels;
using IraqWebsite.Models;
using IraqWebsite.ViewModels.Slider;
using IraqWebsite.ViewModels.AboutUs;
using IraqWebsite.ViewModels.Partner;
using IraqWebsite.ViewModels.ContactUs;
using IraqWebsite.Helper;
using IraqWebsite.ViewModels.Client;
using IraqWebsite.ViewModels.BlogSection;
using IraqWebsite.ViewModels.Blogs;
using IraqWebsite.ViewModels.Projects;
using IraqWebsite.ViewModels.ProjectSection;
using IraqWebsite.ViewModels.ServiceSection;
using IraqWebsite.ViewModels.Services;
using IraqWebsite.ViewModels.Statiscs;
using IraqWebsite.ViewModels.Products;
using IraqWebsite.ViewModels.AcadmeicTraining;
using IraqWebsite.ViewModels.Department;
using IraqWebsite.ViewModels.EventSection;
using IraqWebsite.ViewModels.Events;
using IraqWebsite.ViewModels.Managers;
using IraqWebsite.ViewModels.Structure;

namespace IraqWebsite.AutoMapper
{
    public class SystemProfile : Profile
    {
        public SystemProfile()
        { 
            #region Slider

            CreateMap<Slider, SliderDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubTitleAr) : src.SubTitle));
			//Model To ViewModel

			CreateMap<Slider, EditSliderVm>()
				.ForMember(dest => dest.Img, m => m.Ignore());


            //// ViewModel To Model 
            CreateMap<CreateSliderVm, Slider>()
                .ForMember(dest => dest.Img, m => m.MapFrom(src => "/Images/"+ src.Img.FileName))
                .ForMember(dest => dest.Id, m => m.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, m => m.MapFrom(src => DateTime.Now));

            CreateMap<EditSliderVm, Slider>()
                .ForMember(dest => dest.Img, m => m.MapFrom(src => "/Images/" + src.Img.FileName))
                .ForMember(dest => dest.UpdateDate, m => m.MapFrom(src => DateTime.Now));

            #endregion

            #region AboutUsVm

            CreateMap<AboutUs, AboutUsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description))
                .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubTitleAr) : src.SubTitle))
                .ForMember(dest => dest.FeatureOneTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.FeatureOneTitleAr) : src.FeatureOneTitle))
                .ForMember(dest => dest.FeatureTwoTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.FeatureTwoTitleAr) : src.FeatureTwoTitle))
                .ForMember(dest => dest.FeatureThreeTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.FeatureThreeTitleAr) : src.FeatureThreeTitle))
                .ForMember(dest => dest.FeatureFourTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.FeatureFourTitleAr) : src.FeatureFourTitle));


            CreateMap<AboutUsPage, AboutUsPageDto>()
                .ForMember(dest => dest.DescriptionOurMission, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionArOurMission) : src.DescriptionOurMission))
                .ForMember(dest => dest.DescriptionOurGoal, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionArOurGoal) : src.DescriptionOurGoal))
                .ForMember(dest => dest.DescriptionOurVision, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionArOurVision) : src.DescriptionOurVision))
                .ForMember(dest => dest.ManagerWordTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.ManagerWordTitleAr) : src.ManagerWordTitle))
                .ForMember(dest => dest.CompanySkillTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.CompanySkillTitleAr) : src.CompanySkillTitle))
                .ForMember(dest => dest.CompanySkillDescription, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.CompanySkillDescriptionAr) : src.CompanySkillDescription));


            CreateMap<CreateAboutUsPageVm, AboutUsPage>()
                .ForMember(dest => dest.ImageSectionOne, m => m.MapFrom(src => "/Images/" + src.ImageSectionOne.FileName))
                .ForMember(dest => dest.ImageOurVision, m => m.MapFrom(src => "/Images/" + src.ImageOurVision.FileName))
                .ForMember(dest => dest.ImageOurGoal, m => m.MapFrom(src => "/Images/" + src.ImageOurGoal.FileName))
                .ForMember(dest => dest.ImageOurMission, m => m.MapFrom(src => "/Images/" + src.ImageOurMission.FileName))
                .ForMember(dest => dest.ImageManagerWord, m => m.MapFrom(src => "/Images/" + src.ImageManagerWord.FileName))
                .ForMember(dest => dest.CompanySkillImageOne, m => m.MapFrom(src => "/Images/" + src.CompanySkillImageOne.FileName))
                .ForMember(dest => dest.CompanySkillImageTwo, m => m.MapFrom(src => "/Images/" + src.CompanySkillImageTwo.FileName))
                .ForMember(dest => dest.CompanySkillImageThree, m => m.MapFrom(src => "/Images/" + src.CompanySkillImageThree.FileName));


            CreateMap<EditAboutUsPageVm, AboutUsPage>()
               .ForMember(dest => dest.ImageSectionOne, m => m.MapFrom(src => "/Images/" + src.ImageSectionOne.FileName))
                .ForMember(dest => dest.ImageOurVision, m => m.MapFrom(src => "/Images/" + src.ImageOurVision.FileName))
                .ForMember(dest => dest.ImageOurGoal, m => m.MapFrom(src => "/Images/" + src.ImageOurGoal.FileName))
                .ForMember(dest => dest.ImageOurMission, m => m.MapFrom(src => "/Images/" + src.ImageOurMission.FileName))
                .ForMember(dest => dest.ImageManagerWord, m => m.MapFrom(src => "/Images/" + src.ImageManagerWord.FileName))
                .ForMember(dest => dest.CompanySkillImageOne, m => m.MapFrom(src => "/Images/" + src.CompanySkillImageOne.FileName))
                .ForMember(dest => dest.CompanySkillImageTwo, m => m.MapFrom(src => "/Images/" + src.CompanySkillImageTwo.FileName))
                .ForMember(dest => dest.CompanySkillImageThree, m => m.MapFrom(src => "/Images/" + src.CompanySkillImageThree.FileName));

			CreateMap <AboutUsPage, EditAboutUsPageVm>()
               .ForMember(dest => dest.ImageSectionOne, m => m.Ignore())
               .ForMember(dest => dest.ImageOurVision, m => m.Ignore())
               .ForMember(dest => dest.ImageOurGoal, m => m.Ignore())
               .ForMember(dest => dest.ImageOurMission, m => m.Ignore())
               .ForMember(dest => dest.ImageManagerWord, m => m.Ignore())
               .ForMember(dest => dest.CompanySkillImageOne, m => m.Ignore())
               .ForMember(dest => dest.CompanySkillImageTwo, m => m.Ignore())
               .ForMember(dest => dest.CompanySkillImageThree, m => m.Ignore());
            //Model To ViewModel

            CreateMap<AboutUs, EditAboutUsVm>()
                .ForMember(dest => dest.ImgOne, m => m.Ignore())
                .ForMember(dest => dest.ImgTwo, m => m.Ignore())
                .ForMember(dest => dest.ImgThree, m => m.Ignore());


            // ViewModel To Model 
            CreateMap<CreateAboutUsVm, AboutUs>()
                .ForMember(dest => dest.ImgOne, m => m.MapFrom(src => "/Images/" + src.ImgOne.FileName))
                .ForMember(dest => dest.ImgTwo, m => m.MapFrom(src => "/Images/" + src.ImgTwo.FileName))
                .ForMember(dest => dest.ImgThree, m => m.MapFrom(src => "/Images/" + src.ImgThree.FileName))
                .ForMember(dest => dest.Id, m => m.MapFrom(src => Guid.NewGuid()));

            CreateMap<EditAboutUsVm, AboutUs>()
                .ForMember(dest => dest.ImgOne, m => m.MapFrom(src => "/Images/" + src.ImgOne.FileName))
                .ForMember(dest => dest.ImgTwo, m => m.MapFrom(src => "/Images/" + src.ImgTwo.FileName))
                .ForMember(dest => dest.ImgThree, m => m.MapFrom(src => "/Images/" + src.ImgThree.FileName));

            #endregion

            #region Clients

            //Model To ViewModel
            CreateMap<Clinet, ClitentDto>();


            //// ViewModel To Model 
            CreateMap<CreateClient, Clinet>()
                .ForMember(dest => dest.Img, m => m.MapFrom(src => "/Images/" + src.Img.FileName));

            #endregion
            
            #region Blogs

            CreateMap<BlogSection, BlogSectionDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubTitleAr) : src.SubTitle))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description));

            CreateMap<CreateBlogSection, BlogSection>();
            CreateMap<EditBlogSection, BlogSection>();
            CreateMap<BlogSection, EditBlogSection>();


            CreateMap<Blog, BlogDto>()
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description))
                 .ForMember(dest => dest.SubDescription, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubDescriptionAr) : src.SubDescription));

            CreateMap<CreateBlog, Blog>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName));

            CreateMap<EditBlog, Blog>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName));

            CreateMap<Blog, EditBlog>()
                .ForMember(dest => dest.Tags, m => m.Ignore())
                .ForMember(dest => dest.Image, m => m.Ignore());

            CreateMap<BlogCategory, BlogCategoryDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<CreateBlogCategoryDto, BlogCategory>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<EditBlogCategoryDto, BlogCategory>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<BlogCategory, EditBlogCategoryDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<Tag, TagDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));
            ;
            CreateMap<CreateTagDto, Tag>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));
            ;

            #endregion

            #region AcadmeicTraining

            CreateMap<AcadmeicTraining, AcadmeicTrainingDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description));

           
            CreateMap<CreateAcadmeicTrainingDto, AcadmeicTraining>()
                .ForMember(dest => dest.ImageOne, m => m.MapFrom(src => "/Images/" + src.ImageOne.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                .ForMember(dest => dest.ImageThree, m => m.MapFrom(src => "/Images/" + src.ImageThree.FileName))
                ;

            CreateMap<EditAcadmeicTrainingDto, AcadmeicTraining>()
                .ForMember(dest => dest.ImageOne, m => m.MapFrom(src => "/Images/" + src.ImageOne.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                .ForMember(dest => dest.ImageThree, m => m.MapFrom(src => "/Images/" + src.ImageThree.FileName))
                ;

            CreateMap<AcadmeicTraining, EditAcadmeicTrainingDto>()
                .ForMember(dest => dest.ImageOne, m => m.Ignore())
                .ForMember(dest => dest.ImageTwo, m => m.Ignore())
                .ForMember(dest => dest.ImageThree, m => m.Ignore())
                ;


            #endregion

            #region Managers

            CreateMap<Managers, ManagerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.NameAr) : src.Name))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.PositionAr) : src.Position));


            CreateMap<CreateManager, Managers>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                ;

            CreateMap<EditManager, Managers>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                ;

            CreateMap<Managers, EditManager>()
                .ForMember(dest => dest.Image, m => m.Ignore())
                ;

            #endregion

            #region Departments

            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.NameAr) : src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description));


            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(dest => dest.ImageOne, m => m.MapFrom(src => "/Images/" + src.ImageOne.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                ;

            CreateMap<EditDepartmentDto, Department>()
                .ForMember(dest => dest.ImageOne, m => m.MapFrom(src => "/Images/" + src.ImageOne.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                ;

            CreateMap<Department, EditDepartmentDto>()
                .ForMember(dest => dest.ImageOne, m => m.Ignore())
                .ForMember(dest => dest.ImageTwo, m => m.Ignore())
                ;


            #endregion

            #region Service

            CreateMap<ServiceSection, ServiceSectionDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubTitleAr) : src.SubTitle))
                ;
            CreateMap<CreateServiceSection, ServiceSection>();
            CreateMap<EditServiceSection, ServiceSection>();
            CreateMap<ServiceSection, EditServiceSection>();


            CreateMap<Service, ServiceDto>()
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description))
                 .ForMember(dest => dest.SubDescription, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubDescriptionAr) : src.SubDescription))
                 .ForMember(dest => dest.ServiceFeatureOne, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.ServiceFeatureOneAr) : src.ServiceFeatureOne))
                 .ForMember(dest => dest.ServiceFeatureTwo, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.ServiceFeatureTwoAr) : src.ServiceFeatureTwo))
                 .ForMember(dest => dest.ServiceFeatureThree, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.ServiceFeatureThreeAr) : src.ServiceFeatureThree))
                 .ForMember(dest => dest.ServiceFeatureFour, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.ServiceFeatureFourAr) : src.ServiceFeatureFour));

            CreateMap<CreateService, Service>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                .ForMember(dest => dest.ImageThree, m => m.MapFrom(src => "/Images/" + src.ImageThree.FileName))
                .ForMember(dest => dest.Icon, m => m.MapFrom(src => "/Images/" + src.Icon.FileName))
                .ForMember(dest => dest.IconWhite, m => m.MapFrom(src => "/Images/" + src.IconWhite.FileName));

            CreateMap<EditService, Service>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                .ForMember(dest => dest.ImageThree, m => m.MapFrom(src => "/Images/" + src.ImageThree.FileName))
                .ForMember(dest => dest.Icon, m => m.MapFrom(src => "/Images/" + src.Icon.FileName))
                .ForMember(dest => dest.IconWhite, m => m.MapFrom(src => "/Images/" + src.IconWhite.FileName));

            CreateMap<Service, EditService>()
                .ForMember(dest => dest.Image, m => m.Ignore())
                .ForMember(dest => dest.ImageTwo, m => m.Ignore())
                .ForMember(dest => dest.ImageThree, m => m.Ignore())
                .ForMember(dest => dest.Icon, m => m.Ignore())
                .ForMember(dest => dest.IconWhite, m => m.Ignore())
                .ForMember(dest => dest.IconWhite, m => m.Ignore());

            #endregion

            #region Events

            CreateMap<EventSection, EventSectionDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubTitleAr) : src.SubTitle))
                ;
            CreateMap<CreateEventSection, EventSection>();
            CreateMap<EditEventSection, EventSection>();
            CreateMap<EventSection, EditEventSection>();


            CreateMap<Event, EventDto>()
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description))
                 .ForMember(dest => dest.SubDescription, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubDescriptionAr) : src.SubDescription))
                 ;

            CreateMap<CreateEvent, Event>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                .ForMember(dest => dest.ImageThree, m => m.MapFrom(src => "/Images/" + src.ImageThree.FileName));

            CreateMap<EditEvent, Event>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                .ForMember(dest => dest.ImageTwo, m => m.MapFrom(src => "/Images/" + src.ImageTwo.FileName))
                .ForMember(dest => dest.ImageThree, m => m.MapFrom(src => "/Images/" + src.ImageThree.FileName));

            CreateMap<Event, EditEvent>()
                .ForMember(dest => dest.Image, m => m.Ignore())
                .ForMember(dest => dest.ImageTwo, m => m.Ignore())
                .ForMember(dest => dest.ImageThree, m => m.Ignore());

            CreateMap<EventCategory, EventCategoryDto>()
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<CreateEventCategory, EventCategory>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<EditEventCategory, EventCategory>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<EventCategory, EditEventCategory>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<CreateStructure, Structure>()
                 .ForMember(dest => dest.SectionImage, m => m.MapFrom(src => "/Images/" + src.SectionImage.FileName))
                .ForMember(dest => dest.StructureImage, m => m.MapFrom(src => "/Images/" + src.StructureImage.FileName));

            CreateMap<Structure, CreateStructure>()
                 .ForMember(dest => dest.SectionImage, m => m.Ignore())
                .ForMember(dest => dest.StructureImage, m => m.Ignore());


            CreateMap<EventCategory, EditEventCategory>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            #endregion

            #region Statisc

            CreateMap<Statisc, StatiscDto>()
                .ForMember(dest => dest.StatiscOneTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.StatiscOneTitleAr) : src.StatiscOneTitle))
                .ForMember(dest => dest.StatiscTwoTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.StatiscTwoTitleAr) : src.StatiscTwoTitle))
                .ForMember(dest => dest.StatiscThreeTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.StatiscThreeTitleAr) : src.StatiscThreeTitle))
                .ForMember(dest => dest.StatiscFourTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.StatiscFourTitleAr) : src.StatiscFourTitle))
                ;
            CreateMap<CreateStatisc, Statisc>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                .ForMember(dest => dest.StatiscOneIcon, m => m.MapFrom(src => "/Images/" + src.StatiscOneIcon.FileName))
                .ForMember(dest => dest.StatiscTwoIcon, m => m.MapFrom(src => "/Images/" + src.StatiscTwoIcon.FileName))
                .ForMember(dest => dest.StatiscThreeIcon, m => m.MapFrom(src => "/Images/" + src.StatiscThreeIcon.FileName))
                .ForMember(dest => dest.StatiscFourIcon, m => m.MapFrom(src => "/Images/" + src.StatiscFourIcon.FileName));

            CreateMap<EditStatisc, Statisc>()
                .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName))
                .ForMember(dest => dest.StatiscOneIcon, m => m.MapFrom(src => "/Images/" + src.StatiscOneIcon.FileName))
                .ForMember(dest => dest.StatiscTwoIcon, m => m.MapFrom(src => "/Images/" + src.StatiscTwoIcon.FileName))
                .ForMember(dest => dest.StatiscThreeIcon, m => m.MapFrom(src => "/Images/" + src.StatiscThreeIcon.FileName))
                .ForMember(dest => dest.StatiscFourIcon, m => m.MapFrom(src => "/Images/" + src.StatiscFourIcon.FileName));

            CreateMap<Statisc, EditStatisc>()
                .ForMember(dest => dest.Image, m => m.Ignore())
                .ForMember(dest => dest.StatiscOneIcon, m => m.Ignore())
                .ForMember(dest => dest.StatiscTwoIcon, m => m.Ignore())
                .ForMember(dest => dest.StatiscThreeIcon, m => m.Ignore())
                .ForMember(dest => dest.StatiscFourIcon, m => m.Ignore())
                ;


            #endregion

            #region Products
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                ;


            CreateMap<CreateProduct, Product>()
                    .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName));

            CreateMap<EditProduct, Product>()
                    .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName));

            CreateMap<Product, EditProduct>()
                    .ForMember(dest => dest.Image, m => m.Ignore());

            
            CreateMap<ProductCategory, ProductCategoryDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<CreateProductCategory, ProductCategory>();
            CreateMap<EditProductCategory, ProductCategory>();
            CreateMap<ProductCategory, EditProductCategory>();


            #endregion

            #region Projects
            CreateMap<ProjectModel, ProjectDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.DescriptionAr) : src.Description))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.AddressAr) : src.Address))
                ;


                CreateMap<CreateProject, ProjectModel>()
                        .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName));

                CreateMap<EditProject, ProjectModel>()
                        .ForMember(dest => dest.Image, m => m.MapFrom(src => "/Images/" + src.Image.FileName));

            CreateMap<ProjectModel, EditProject>()
                    .ForMember(dest => dest.Image, m => m.Ignore());

            CreateMap<ProjectSection, ProjectSectionDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SubTitleAr) : src.SubTitle));

            CreateMap<CreateProjectSection, ProjectSection>();
            CreateMap<EditProjectSection, ProjectSection>();
            CreateMap<ProjectSection, EditProjectSection>();

            CreateMap<ProjectCategory, ProjectCategoryDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title));

            CreateMap<CreateProjectCategory, ProjectCategory>();
            CreateMap<EditProjectCategory, ProjectCategory>();
            CreateMap<ProjectCategory, EditProjectCategory>();


            #endregion

            #region ContactUs


            CreateMap<ContactUs, ContactUsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.TitleAr) : src.Title))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.LocationAr) : src.Location));
            ;
            //ViewModel To Model
            CreateMap<CreateContactUsVm, ContactUs>()
                .ForMember(dest => dest.Img, m => m.MapFrom(src => "/Images/" + src.Img.FileName))
                .ForMember(dest => dest.Id, m => m.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Slogan, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentUICulture.Name == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(src.SloganAr) : src.Slogan))
                .ForMember(dest => dest.CreatedDate, m => m.MapFrom(src => DateTime.Now));

            CreateMap<SendMessageVm, CustemerReview>()
                .ForMember(x=>x.SentDate , m=>m.MapFrom(src => DateTime.Now));


            #endregion

            #region Apperance

            //Model To ViewModel
            CreateMap<AuthManager.Models.Appearance, AppearanceViewModel>()
                .ForMember(dest => dest.logo, m => m.Ignore())
                .ForMember(dest => dest.favIcon, m => m.Ignore());

            #endregion

        }
        private static string? GetLocalizedDate(DateTime? date)
        {
            if (date == null)
                return null;

            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            return currentCulture == "ar-IQ" ? ConvertNumerals.ToArabicNumerals(date.Value.ToString("dd MMM yyyy")) : date.Value.ToString("dd MMM yyyy");
        }
    }
}
