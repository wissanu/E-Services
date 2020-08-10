using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Compare = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Git_system.Models.Form
{
    public partial class Backend_Useradmin : UserAdmin
    {
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation does not match.")]
        public string Confirm_Password { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
    }

    public static class Backend_UseradminAndUserAdmin
    {
        public static UserAdmin ToUserAdmin(this Backend_Useradmin useradmin)
        {
            UserAdmin content = new UserAdmin();

            content.Id = useradmin.Id;
            content.userName = useradmin.userName;
            content.Password = useradmin.Password;
            content.Firstname = useradmin.Firstname;
            content.Lastname = useradmin.Lastname;
            content.JobTitle = useradmin.JobTitle;
            content.Department = useradmin.Department;
            content.Email = useradmin.Email;
            content.Tel = useradmin.Tel;
            content.Permission_permissionId = useradmin.Permission_permissionId;
            content.LastSignIn = useradmin.LastSignIn;
            content.Active = useradmin.Active;

            return content;
        }

        public static Backend_Useradmin ToBackend_Useradmin(this UserAdmin useradmin)
        {
            Backend_Useradmin content = new Backend_Useradmin();

            content.Id = useradmin.Id;
            content.userName = useradmin.userName;
            content.Password = useradmin.Password;
            content.Confirm_Password = useradmin.Password;
            content.Firstname = useradmin.Firstname;
            content.Lastname = useradmin.Lastname;
            content.JobTitle = useradmin.JobTitle;
            content.Department = useradmin.Department;
            content.Email = useradmin.Email;
            content.Tel = useradmin.Tel;
            content.Permission_permissionId = useradmin.Permission_permissionId;
            content.LastSignIn = useradmin.LastSignIn;
            content.Active = useradmin.Active;

            return content;
        }
    }
}