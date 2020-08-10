using System.ComponentModel.DataAnnotations;

namespace Git_system.Models.Form
{
    public class Backend_LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Passwoed { get; set; }
    }
}