using System.ComponentModel.DataAnnotations;

namespace Git_system.Models.Form
{
    public class Frontend_LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class Frontend_LoginModel2
    {
        public string Email2 { get; set; }

        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        public string Lang { get; set; }
    }

    public static class ExtensionMethodForLoginModel
    {
        public static Frontend_LoginModel ToFrontend_loginModel(this Frontend_LoginModel2 content)
        {
            Frontend_LoginModel login = new Frontend_LoginModel();
            login.Email = content.Email2;
            login.Password = content.Password2;
            return login;
        }
    }
}