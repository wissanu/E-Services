using System.ComponentModel.DataAnnotations;

namespace Git_system.Models.Form
{
    public class Frontend_SendResetPassword
    {
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_EmailCorrect")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public System.DateTime BirthDay { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredQuestion")]
        public int QuestionListId { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredAnswer")]
        public string Answer { get; set; }

    }
}