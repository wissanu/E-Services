using Git_system.Models;
using Git_system.Models.ECom.API;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Git_system.Controllers.EComAPI
{
    public abstract class MainController : ApiController
    {
        //init variable default
        protected int _LanguageTypeId { get; set; }

        protected int _MembershipId { get; set; }

        protected int _MembershipTypeId { get; set; }

        protected string _Hostname { get; set; }

        protected Git_system.Models.Database_MainEntities1 db = new Git_system.Models.Database_MainEntities1();

        //protected override bool DisableAsyncSupport { get { return true; } }

        //protected override void ExecuteCore()
        //{
        //    this._LanguageTypeId = CheckLanguageTypeId();
        //    this._MembershipId = CheckMembershipIdIsAuthenticated();
        //    this._MembershipTypeId = CheckMembershipType(this._MembershipId);
        //    //base.ExecuteCore();
        //}

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this._LanguageTypeId = CheckLanguageTypeId();
            if (_LanguageTypeId == 1)
                SetLanguage("th");
            else
                SetLanguage("en-GB");
            this._MembershipId = CheckMembershipIdIsAuthenticated();
            this._MembershipTypeId = CheckMembershipType(this._MembershipId);

            //this._Hostname = string.Format("{0}:{1}", Request.RequestUri.Host, Request.RequestUri.Port);

            HttpResponseMessage Response = new HttpResponseMessage();
            Response.Headers.Add("Access-Control-Allow-Origin", string.Format("{0}:{1}", Request.RequestUri.Host, Request.RequestUri.Port));
        }

        /// <summary>
        /// ทำการตรวจสอบการเข้าสู่ระบบของ User
        /// </summary>
        /// <returns>ค่า DataAndStatus ที่มี Data คือ Backend_Authorize</returns>
        protected DataAndStatus CheckIsAuthenticated()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();

            HttpCookie facAut = HttpContext.Current.Request.Cookies[".FOENAUTH"];

            Func<String> getHeaderAuthorization = () => HttpContext.Current.Request.Headers["Authorization"];
            Func<String, String> cleanBearer = c => c != null ? c.Replace("Bearer ", "") : null;
            Func<String> getTokenAuthorization = () => cleanBearer(getHeaderAuthorization());

            var authToken = facAut == null ? getTokenAuthorization() : facAut.Value;

            Func<Func<object>, dynamic> tryNull = (f) =>
            {
                try
                {
                    return f();
                }
                catch
                {
                    return null;
                }
            };

            System.Web.Security.FormsAuthenticationTicket _FOENAUTH = tryNull(() => System.Web.Security.FormsAuthentication.Decrypt(authToken));

            if (_FOENAUTH != null)
            {
                Git_system.Models.Form.UserAuthorize backend_authorize = Newtonsoft.Json.JsonConvert.DeserializeObject<Git_system.Models.Form.UserAuthorize>(_FOENAUTH.UserData);

                if (backend_authorize.Type.Equals("User"))//ตรวจสอบว่าเป็น user ประเภท User หรือไม่
                {
                    dataAndStatus = new DataAndStatus(1, backend_authorize);
                }
            }
            return dataAndStatus;
        }

        /// <summary>
        /// ทำการตรวจสอบ membershipId ของ User
        /// </summary>
        /// <returns>0 เมื่อไม่ได้ทำการ login เป็นค่าอื่นๆเมื่อทำการ login แล้ว</returns>
        private int CheckMembershipIdIsAuthenticated()
        {
            int membershipId = 0;
            DataAndStatus dataAndStatus = CheckIsAuthenticated();
            if (dataAndStatus.status.Equals(1))
            {
                Git_system.Models.Form.UserAuthorize backend_authorize = dataAndStatus.data;
                if (backend_authorize.Type.Equals("User"))
                {
                    if (db.Memberships.Find(backend_authorize.Id) != null)
                        membershipId = backend_authorize.Id;
                }
            }
            return membershipId;
        }

        /// <summary>
        /// ทำการตรวจสอบค่า MembershipTypeId ของ MembershipId
        /// </summary>
        /// <param name="membershipId">Membership ของ User</param>
        /// <returns>คืนค่า 1 เมื่อไม่พบหรือเป็นบุคคลทั่วไป ค่าอื่นๆตามประเภทสมาชิก</returns>
        private int CheckMembershipType(int membershipId)
        {
            Membership membership = new Membership();
            membership = db.Memberships.Find(membershipId);

            if (membership == null)
                return _LanguageTypeId == 1 ? 1 : 2;

            if (membership.RegisterDateExp >= System.DateTime.Now)
                return membership.MembershipRegisterTypeId;
            else
            {
                if (membership.MembershipRegisterTypeId == 1 || membership.MembershipRegisterTypeId == 3 || membership.MembershipRegisterTypeId == 5)
                    return 1;
                else if (membership.MembershipRegisterTypeId == 2 || membership.MembershipRegisterTypeId == 4 || membership.MembershipRegisterTypeId == 6)
                    return 2;
            }
            return 1;
        }

        /// <summary>
        /// Set language cookie.
        /// </summary>
        /// <param name="cultureName">Culrure name</param>
        protected void SetCookieLanguage(string cultureName)
        {
            string cookiesLanguage = cultureName == "en-GB" ? "f11b8154674d1f97d062be097d3a5ccab0fc27ee" : "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45";
            var resp = new HttpResponseMessage();
            //var cookie = new CookieHeaderValue("FCNLT", cookiesLanguage);
            //resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            var cookie = new HttpCookie("FCNLT", cookiesLanguage);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Set language.
        /// </summary>
        /// <param name="cultureName">Culture name</param>
        private void SetLanguage(string cultureName)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Check number of language.
        /// </summary>
        /// <returns>1 is thai ,2 is english</returns>
        private int CheckLanguageTypeId()
        {
            CookieHeaderValue cultureCookie = Request.Headers.GetCookies("FCNLT").FirstOrDefault();
            if (cultureCookie != null)
            {
                string Language = null;
                Language = cultureCookie["FCNLT"].Value;
                switch (Language)
                {
                    case "f11b8154674d1f97d062be097d3a5ccab0fc27ee":
                        return 2;

                    case "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45":
                        return 1;

                    default:
                        return 1;
                }
            }
            else
            {
                SetCookieLanguage("th");
                return 1;
            }
        }
    }
}