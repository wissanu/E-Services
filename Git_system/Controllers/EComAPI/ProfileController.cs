using Git_system.App_Code;
using Git_system.Models;
using Git_system.Models.ECom.API;
using Git_system.Models.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Git_system.Controllers.EComAPI
{
    public class ProfileController : MainController
    {
        public class SignInFrom
        {
            public SignInFrom()
            {
            }

            public SignInFrom(object value)
            {
                SignInFrom signInForm = new SignInFrom();
                signInForm = Newtonsoft.Json.JsonConvert.DeserializeObject<SignInFrom>(value.ToString());
                this.Email = signInForm.Email != null ? signInForm.Email : "";
                this.Password = signInForm.Password != null ? signInForm.Password : "";
            }

            public string Email { get; set; }

            public string Password { get; set; }
        }

        [AllowCrossSite]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, new DataAndStatus().authen(_MembershipId, () =>
            {
                Membership membership = db.Memberships.Find(_MembershipId);
                return new DataAndStatus(1, membership.toAPI());
            }));
        }

        [AllowCrossSite]
        public HttpResponseMessage Post(SignInFrom value)
        {
            //SignInFrom signInFrom = new SignInFrom(value: value);
            string Email = value.Email;
            string Password = value.Password;
            DataAndStatus dataAndStatus = new DataAndStatus();
            if (Email == null || Password == null)
            {
                dataAndStatus = new DataAndStatus(303, null);
                return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
            }
            String passHash = (Cryptography.SHA512SaltAddSalt(Password, Email)).ToString();
            List<Membership> membership = (from c in db.Memberships where c.Email.Equals(Email) && c.Password.Equals(passHash) select c).ToList();
            int count_membership = membership.Count();
            if (count_membership == 1)
            {
                if (membership.FirstOrDefault().Active == true)
                {
                    Membership membershipLogin = membership.FirstOrDefault();
                    string UserData = Newtonsoft.Json.JsonConvert.SerializeObject(new UserAuthorize(membershipLogin.Id, membershipLogin.Email, "User"), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

                    System.Web.Security.FormsAuthentication.Initialize();
                    System.Web.Security.FormsAuthenticationTicket authTicket = new System.Web.Security.FormsAuthenticationTicket(1, Email, DateTime.Now, DateTime.Now.AddMinutes(60), false, UserData);
                    string encTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);

                    var cookie = new HttpCookie(".FOENAUTH", encTicket);
                    SetCookieLanguage(membership.FirstOrDefault().isLocal() ? "th" : "en-GB");

                    HttpContext.Current.Response.Cookies.Add(cookie);

                    dataAndStatus = new DataAndStatus(1, null);

                    return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
                }
                else
                {
                    dataAndStatus = new DataAndStatus(301, null);
                    return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
                }
            }
            else
            {
                dataAndStatus = new DataAndStatus(302, null);
                return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
            }
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}