using Git_system.Models;
using System;
using System.Threading;
using System.Web;

namespace Git_system.Helper
{
    public abstract class FrontendController : Git_system.Controllers.FrontendController
    {
        protected override bool DisableAsyncSupport { get { return true; } }

        protected override void ExecuteCore()
        {
            try
            {
                tryExecuteCore();
            }
            catch (Exception e)
            {
                Server.saveToLog(e, "Frontend");

                Response.Redirect(Url.Action("Home", "Home"));
            }
        }

        private void tryExecuteCore()
        {
            string CultureName = null;
            HttpCookie cultureCookie = Request.Cookies["FCNLT"];
            if (cultureCookie != null)
            {
                string Language = null;
                Language = cultureCookie.Value;
                switch (Language)
                {
                    case "f11b8154674d1f97d062be097d3a5ccab0fc27ee":
                        CultureName = "en-GB";
                        break;

                    case "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45":
                        CultureName = "th";
                        break;

                    default:
                        CultureName = "th";
                        break;
                }
            }
            else
            {
                CultureName = "th";

                HttpCookie CultureNameLanguageType = new HttpCookie("FCNLT");//HashCookie
                CultureNameLanguageType.Value = "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45";
                CultureNameLanguageType.HttpOnly = true;
                Response.Cookies.Add(CultureNameLanguageType);
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            this._LanguageType = Git_system.MultiResources.Multi.__LanguageType;

            HttpCookie facAut = Request.Cookies[".FOENAUTH"];
            System.Web.Security.FormsAuthenticationTicket _FOENAUTH = facAut == null ? new System.Web.Security.FormsAuthenticationTicket(null, true, 0) : System.Web.Security.FormsAuthentication.Decrypt(facAut.Value);

            if (_FOENAUTH.Name != null)
            {
                Git_system.Models.Form.UserAuthorize backend_authorize = new Models.Form.UserAuthorize();
                try { backend_authorize = Newtonsoft.Json.JsonConvert.DeserializeObject<Git_system.Models.Form.UserAuthorize>(_FOENAUTH.UserData); }
                catch { System.Web.Security.FormsAuthentication.SignOut(); }
                CurrentUserId = backend_authorize.Id;

                Membership membership = db.Memberships.Find(CurrentUserId);
                if (membership != null)
                    setLanguageForMembership(membership);
            }
            base.ExecuteCore();
        }

        private void setLanguageForMembership(Membership membership)
        {
            changeLanguage(membership.isLocal() ? "th" : "en");
        }
    }
}