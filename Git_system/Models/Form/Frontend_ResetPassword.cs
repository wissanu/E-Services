using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Compare = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Git_system.Models.Form
{
    public class Frontend_ResetPassword
    {
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation does not match.")]
        public string Confirm_Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public string User { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public string Token { get; set; }
    }

    public static class ExtensionMethod_ResetPassword
    {
        public static Membership toMembership(this Frontend_ResetPassword resetPassword)
        {
            Database_MainEntities1 db = new Database_MainEntities1();

            Membership membership = new Membership();
            int id = System.Convert.ToInt32(resetPassword.User, 16);
            membership = db.Memberships.Find(id);

            Membership content = new Membership();
            content = membership;

            string passHash = Git_system.App_Code.Cryptography.SHA512SaltAddSalt(resetPassword.Password, content.Email);
            content.Password = passHash;
            return content;
        }
    }
}