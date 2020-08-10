using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Compare = System.ComponentModel.DataAnnotations.CompareAttribute;
namespace Git_system.Models.Form
{
    public class Frontend_Register
    {
        public Frontend_RegisterStep2 Membership = new Frontend_RegisterStep2();

        public int RegisterType { get; set; }

        public Frontend_Register()
        {
            this.RegisterType = 0;
        }
    }

    public class Frontend_RegisterStep1
    {
        public Frontend_RegisterStep1()
        {
            this.MembershipNumber = "";
        }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredFirstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredLastname")]
        public string Lastname { get; set; }

        public string FirstnameTH { get; set; }

        public string LastnameTH { get; set; }

        public string FirstnameEN { get; set; }

        public string LastnameEN { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_EmailCorrect")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Email")]
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_EmailConfirm")]
        public string Confirm_Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Password")]
        [MinLength(8, ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "PasswordMustBeLongerThan8Characters")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_PasswordConfirm")]
        public string Confirm_Password { get; set; }

        public string MembershipNumber { get; set; }

        public Nullable<System.DateTime> ExpCRM { get; set; }

        public string CaptchaImage { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static Frontend_RegisterStep2 toMembership(this Frontend_RegisterStep1 RegisterStep1)
        {
            Frontend_RegisterStep2 content = new Frontend_RegisterStep2();

            content.Firstname = RegisterStep1.Firstname;
            content.Lastname = RegisterStep1.Lastname;

            content.FirstnameEN = RegisterStep1.FirstnameEN;
            content.FirstnameTH = RegisterStep1.FirstnameTH;
            content.LastnameEN = RegisterStep1.LastnameEN;
            content.LastnameTH = RegisterStep1.LastnameTH;
            content.Password = RegisterStep1.Password;
            content.Email = RegisterStep1.Email;
            content.IdCRM = RegisterStep1.MembershipNumber;
            content.ExpCRM = RegisterStep1.ExpCRM;
            return content;
        }
    }
}