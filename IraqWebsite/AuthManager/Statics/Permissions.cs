using IraqWebsite.AuthManager.ViewModels;

namespace IraqWebsite.AuthManager.Statics
{
    public class Permissions
    {
        public static List<string> GeneratePermissions(string module) => new List<string>()
        {
            $"{module} Create",
            $"{module} View",
            $"{module} Edit",
            $"{module} Delete",
        };

        public static class Dashboard
        {
            public const string View = "Dashboard View";
            public const string Delete = "Dashboard Delete";
            public const string Edit = "Dashboard Edit";
            public const string Create = "Dashboard Create";
        }

        public static class UserPolicy
        {
            public const string View = "User View";
            public const string Delete = "User Delete";
            public const string Edit = "User Edit";
            public const string Create = "User Create";
        }

        public static class RolePolicy
        {
            public const string View = "Role View";
            public const string Delete = "Role Delete";
            public const string Edit = "Role Edit";
            public const string Create = "Role Create";
        }

        public static class EmailSettingPolicy
        {
            public const string View = "Email Setting View";
            public const string Delete = "Email Setting Delete";
            public const string Edit = "Email Setting Edit";
            public const string Create = "Email Setting Create";
        }

        public static class PsaawordComplexity
        {
            public const string View = "Psaaword Complexity View";
            public const string Delete = "Psaaword Complexity Delete";
            public const string Edit = "Psaaword Complexity Edit";
            public const string Create = "Psaaword Complexity Create";
        }

        public static class Appearance
        {
            public const string View = "Appearance View";
            public const string Delete = "Appearance Delete";
            public const string Edit = "Appearance Edit";
            public const string Create = "Appearance Create";
        }

        public static class UserManag
        {
            public const string View = "User Manag View";
            public const string Delete = "User Manag Delete";
            public const string Edit = "User Manag Edit";
            public const string Create = "User Manag Create";
        }

        public static class UserLock
        {
            public const string View = "User Lock View";
            public const string Delete = "User Lock Delete";
            public const string Edit = "User Lock Edit";
            public const string Create = "User Lock Create";
        }

        public static class SliderPolicy
        {
            public const string View = "Slider View";
            public const string Delete = "Slider Delete";
            public const string Edit = "Slider Edit";
            public const string Create = "Slider Create";
        }

        public static class ScheduleSectionPolicy
        {
            public const string View = "Schedule Section View";
            public const string Delete = "Schedule Section Delete";
            public const string Edit = "Schedule Section Edit";
            public const string Create = "Schedule Section Create";
        }

        public static class ScheduleSubSectionPolicy
        {
            public const string View = "Schedule Sub Section View";
            public const string Delete = "Schedule Sub Section Delete";
            public const string Edit = "Schedule Sub Section Edit";
            public const string Create = "Schedule Sub Section Create";
        }

        public static class AboutPolicy
        {
            public const string View = "About View";
            public const string Delete = "About Delete";
            public const string Edit = "About Edit";
            public const string Create = "About Create";
        }

        public static class PortfolioPolicy
        {
            public const string View = "Portfolio View";
            public const string Delete = "Portfolio Delete";
            public const string Edit = "Portfolio Edit";
            public const string Create = "Portfolio Create";
        }

        public static class EventPolicy
        {
            public const string View = "Event View";
            public const string Delete = "Event Delete";
            public const string Edit = "Event Edit";
            public const string Create = "Event Create";
        }

        public static class NewsPolicy
        {
            public const string View = "News View";
            public const string Delete = "News Delete";
            public const string Edit = "News Edit";
            public const string Create = "News Create";
        }

        public static class PartnerPolicy
        {
            public const string View = "Partner View";
            public const string Delete = "Partner Delete";
            public const string Edit = "Partner Edit";
            public const string Create = "Partner Create";
        }

        public static class ContactPolicy
        {
            public const string View = "Contact View";
            public const string Delete = "Contact Delete";
            public const string Edit = "Contact Edit";
            public const string Create = "Contact Create";
        }

        public static class FmLinkPolicy
        {
            public const string View = "FmLink View";
            public const string Delete = "FmLink Delete";
            public const string Edit = "FmLink Edit";
            public const string Create = "FmLink Create";
        }

        public static class LogPolicy
        {
            public const string View = "Log View";
            public const string Delete = "Log Delete";
            public const string Edit = "Log Edit";
            public const string Create = "Log Create";
        }

        public static class RecaptchaPolicy
        {
            public const string View = "Recaptcha View";
            public const string Delete = "Recaptcha Delete";
            public const string Edit = "Recaptcha Edit";
            public const string Create = "Recaptcha Create";
        }

        public static class SubscribersPolicy
        {
            public const string View = "Subscribers View";
            public const string Delete = "Subscribers Delete";
            public const string Edit = "Subscribers Edit";
            public const string Create = "Subscribers Create";
        }

        public static class MetaPolicy
        {
            public const string View = "Meta View";
            public const string Delete = "Meta Delete";
            public const string Edit = "Meta Edit";
            public const string Create = "Meta Create";
        }

        public static List<RoleClaimViewModel> AllClaims()
        {
            string[] models = {
                "Dashboard", "User", "Role",
                "Email Setting", "Psaaword Complexity","Appearance",
                "User Manag","User Lock","Slider","Schedule Section","Schedule Sub Section","Subscribers",
                "About","Portfolio","Event","News","Partner","Contact","FmLink","Log","Recaptcha","Meta"
            };

            string[] claimsModel = { "Create", "Edit", "Delete", "View" };
            List<RoleClaimViewModel> roleClaimViewModel = new List<RoleClaimViewModel>();

            for (int i = 0; i < models.Length; i++)
            {
                for (int j = 0; j < claimsModel.Length; j++)
                {
                    roleClaimViewModel.Add(new RoleClaimViewModel { Selected = false, Type = "Permission", Value = models[i] + " " + claimsModel[j] });
                }
            }

            return roleClaimViewModel;
        }
    }
}
