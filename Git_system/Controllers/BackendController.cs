using Git_system.App_Code;
using Git_system.Helper;
using Git_system.Models;
using Git_system.Models.Form;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Git_system.Controllers
{
    public class Backend_AuthorizeAttrinbite : FilterAttribute, IAuthorizationFilter
    {
        // For web
        // http://stackoverflow.com/questions/17116494/how-to-create-a-custom-authorizeattribute-that-redirects
        // http://www.codeproject.com/Articles/421639/Customizing-the-Authorize-attribute
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity id = (System.Web.Security.FormsIdentity)filterContext.HttpContext.User.Identity;
                UserAuthorize userAuthorize = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuthorize>(id.Ticket.UserData);

                if (!userAuthorize.Type.Equals("Admin"))//ตรวจสอบว่าเป็น user ประเภท admin หรือไม่
                {
                    filterContext.Result = new RedirectToRouteResult(new
                                                RouteValueDictionary(new { controller = "Backend", action = "Index" }));
                    System.Web.Security.FormsAuthentication.SignOut();
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Backend", action = "Index" }));
            }
        }
    }

    public class BackendPermission : FilterAttribute, IAuthorizationFilter
    {
        private bool isAdmin { get; set; }
        private bool isUser { get; set; }
        private bool isProduct { get; set; }
        private bool isPayment { get; set; }
        private bool isLog { get; set; }
        private bool isESlide { get; set; }
        private bool isEProduct { get; set; }
        private bool isEStock { get; set; }
        private bool isEPayView { get; set; }
        private bool isEPayConfirm { get; set; }
        private bool isEDeliver { get; set; }
        private bool isEPromotion { get; set; }
        private bool isEStatistic { get; set; }

        public BackendPermission(
                    bool isAdmin = false,
                    bool isUser = false,
                    bool isProduct = false,
                    bool isPayment = false,
                    bool isLog = false,
                    bool isESlide = false,
                    bool isEProduct = false,
                    bool isEStock = false,
                    bool isEPayView = false,
                    bool isEPayConfirm = false,
                    bool isEDeliver = false,
                    bool isEPromotion = false,
                    bool isEStatistic = false)
        {
            this.isAdmin = isAdmin;
            this.isUser = isUser;
            this.isProduct = isProduct;
            this.isPayment = isPayment;
            this.isLog = isLog;
            this.isESlide = isESlide;
            this.isEProduct = isEProduct;
            this.isEStock = isEStock;
            this.isEPayView = isEPayView;
            this.isEPayConfirm = isEPayConfirm;
            this.isEDeliver = isEDeliver;
            this.isEPromotion = isEPromotion;
            this.isEStatistic = isEStatistic;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            int? currentUseradminId = null;
            if ((filterContext.HttpContext.User != null) && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity id;
                id = (System.Web.Security.FormsIdentity)filterContext.HttpContext.User.Identity;
                UserAuthorize userAuthorize = new Models.Form.UserAuthorize();
                try
                {
                    userAuthorize = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuthorize>(id.Ticket.UserData);
                    currentUseradminId = userAuthorize.Id;
                }
                catch
                {
                    currentUseradminId = null;
                }
            }

            if (!checkPermission(currentUseradminId, isAdmin, isUser, isProduct, isPayment, isLog, isESlide, isEProduct, isEStock, isEPayView, isEPayConfirm, isEDeliver, isEPromotion, isEStatistic))
            {
                filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Backend", action = "Permission" }));
            }
        }

        /// <summary>
        /// Give true when have permission
        /// </summary>
        /// <param name="currentUseradminId">Id of admin</param>
        /// <param name="isAdmin">Request Admin</param>
        /// <param name="isUser">Request User</param>
        /// <param name="isProduct">Request Product</param>
        /// <param name="isPayment">Request Payment</param>
        /// <param name="isLog">Request Log</param>
        /// <param name="isESlide">Request ESlide</param>
        /// <param name="isEProduct">Request EProduct</param>
        /// <param name="isEStock">Request EStock</param>
        /// <param name="isEPayView">Request EPayView</param>
        /// <param name="isEPayConfirm">Request EPayConfirm</param>
        /// <param name="isEDeliver">Request EDeliver</param>
        /// <param name="isEPromotion">Request EPromotion</param>
        /// <param name="isEStatistic">Request EStatistic</param>
        /// <returns>Have permission</returns>
        private bool checkPermission(
                    int? currentUseradminId,
                    bool isAdmin = false,
                    bool isUser = false,
                    bool isProduct = false,
                    bool isPayment = false,
                    bool isLog = false,
                    bool isESlide = false,
                    bool isEProduct = false,
                    bool isEStock = false,
                    bool isEPayView = false,
                    bool isEPayConfirm = false,
                    bool isEDeliver = false,
                    bool isEPromotion = false,
                    bool isEStatistic = false)
        {
            if (currentUseradminId.HasValue)
            {
                using (var context = new Database_MainEntities1())
                {
                    UserAdmin useradmin = new UserAdmin();
                    useradmin = context.UserAdmins.Find(currentUseradminId);
                    Permission permission = useradmin.Permission;

                    if (isAdmin == true && permission.isAdmin == false)
                        return false;

                    if (isUser == true && permission.isUser == false)
                        return false;

                    if (isProduct == true && permission.isProduct == false)
                        return false;

                    if (isPayment == true && permission.isPayment == false)
                        return false;

                    if (isLog == true && permission.isLog == false)
                        return false;

                    if (isESlide == true && permission.isESlide == false)
                        return false;

                    if (isEProduct == true && permission.isEProduct == false)
                        return false;

                    if (isEStock == true && permission.isEStock == false)
                        return false;

                    if (isEPayView == true && permission.isEPayView == false)
                        return false;

                    if (isEPayConfirm == true && permission.isEPayConfirm == false)
                        return false;

                    if (isEDeliver == true && permission.isEDeliver == false)
                        return false;

                    if (isEPromotion == true && permission.isEPromotion == false)
                        return false;

                    if (isEStatistic == true && permission.isEStatistic == false)
                        return false;

                    return true;
                }
            }
            return false;
        }
    }

    public abstract class BackendMain : Controller
    {
        protected int CurrentUseradminId { set; get; }

        protected override bool DisableAsyncSupport { get { return true; } }

        protected override void ExecuteCore()
        {
            try
            {
                tryExecuteCore();
            }
            catch (Exception e)
            {
                Server.saveToLog(e, "Backend");

                Response.Redirect(Url.Action("Dashboard", "Backend"));
            }
        }

        private void tryExecuteCore()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            bool Login = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (Login)
            {
                System.Web.Security.FormsIdentity id;
                id = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                UserAuthorize backend_authorize = new Models.Form.UserAuthorize();
                try { backend_authorize = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuthorize>(id.Ticket.UserData); }
                catch { System.Web.Security.FormsAuthentication.SignOut(); }
                CurrentUseradminId = backend_authorize.Id;
            }

            ViewBag.CurrentUseradminId = CurrentUseradminId;
            base.ExecuteCore();
        }
    }

    public class BackendController : BackendMain
    {
        protected Database_MainEntities1 db = new Database_MainEntities1();

        //
        // GET: /Backend/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Backend/
        [HttpPost]
        public ActionResult Index(Backend_LoginModel model)
        {
            //string returnstring = model.UserName + "," + model.Passwoed;
            if (ModelState.IsValid)
            {
                string passHash = Cryptography.SHA512SaltAddSalt(model.Passwoed, model.UserName);
                List<UserAdmin> Useradmin = (from c in db.UserAdmins where c.userName.Equals(model.UserName) && c.Password.Equals(passHash) select c).ToList();

                if (Useradmin.Count() == 1)
                {
                    UserAdmin useradmin = Useradmin.FirstOrDefault();
                    string UserData = Newtonsoft.Json.JsonConvert.SerializeObject(new UserAuthorize(useradmin.Id, useradmin.userName, "Admin"), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

                    System.Web.Security.FormsAuthentication.Initialize();
                    System.Web.Security.FormsAuthenticationTicket authTicket = new System.Web.Security.FormsAuthenticationTicket(2, useradmin.userName, DateTime.Now, DateTime.Now.AddMinutes(60), false, UserData);
                    string encTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    useradmin.LastSignIn = DateTime.Now;
                    db.Entry(useradmin).State = EntityState.Modified;
                    db.SaveChanges();

                    LogManageDatabase.add_to_database(useradmin.userName, "Login", 1);//add to logfile

                    return RedirectToAction("Dashboard", "Backend");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is not correct");
                }
            }

            return View(model);
        }

        public ActionResult Reset(string User, string Token)
        {
            int userId = Convert.ToInt32(User, 16);

            long date = DateTime.UtcNow.Ticks;
            long dateout = date / 10000000;//1 sec
            dateout = dateout / 3600; //60 min

            string TokenHash = Git_system.App_Code.Cryptography.SHA1(string.Concat(User, dateout.ToString("X"))).ToString();
            bool tokenCheck = false;
            tokenCheck = (TokenHash == Token);
            ViewBag.CheckToken = tokenCheck;
            if (tokenCheck)
            {
                Frontend_ResetPassword reset = new Frontend_ResetPassword();
                reset.User = User;
                reset.Token = Token;
                return View(reset);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(Frontend_ResetPassword resetPassword)
        {
            long date = DateTime.UtcNow.Ticks;
            long dateout = date / 10000000;//1 sec
            dateout = dateout / 3600; //60 min
            string TokenHash = Git_system.App_Code.Cryptography.SHA1(string.Concat(resetPassword.User, dateout.ToString("X"))).ToString();
            bool tokenCheck = false;
            tokenCheck = (TokenHash == resetPassword.Token);
            ViewBag.CheckToken = tokenCheck;

            if (ModelState.IsValid)
            {
                if (tokenCheck)
                {
                    int userAdminid = System.Convert.ToInt32(resetPassword.User, 16);
                    UserAdmin UserAdminOriginalValues = db.UserAdmins.Find(userAdminid);
                    UserAdmin UserAdminValue = UserAdminOriginalValues;
                    string passHash = Cryptography.SHA512SaltAddSalt(resetPassword.Password, UserAdminOriginalValues.userName);
                    UserAdminValue.Password = passHash;
                    db.Entry(UserAdminOriginalValues).CurrentValues.SetValues(UserAdminValue);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            int userId = Convert.ToInt32(resetPassword.User, 16);

            if (tokenCheck)
            {
                Frontend_ResetPassword reset = new Frontend_ResetPassword();
                reset.User = resetPassword.User;
                reset.Token = resetPassword.Token;
                return View(reset);
            }
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Backend");
        }

        [Backend_AuthorizeAttrinbite]
        public ActionResult Dashboard()
        {
            var pro = db.Products.Where(p => p.Group == 1).Count();
            var co = db.Courses.Where(c => c.Product.Group == 1).Count();

            var online_membership = db.Memberships.Where(m => m.MembershipRegisterTypeId == 1 || m.MembershipRegisterTypeId == 2).Count();
            var membership = db.Memberships.Where(m => m.MembershipRegisterTypeId != 1 && m.MembershipRegisterTypeId != 2).Count();

            var pay = db.Pays.Where(p => p.Type == 2).Count();
            var confirm = db.ConfirmPayments.Where(c => c.Pay.Type == 2).Count();

            ViewBag.pro = pro;
            ViewBag.co = co;

            ViewBag.Online_membership = online_membership;
            ViewBag.Membership = membership;

            ViewBag.Pay = pay;
            ViewBag.Confirm = confirm;
            return View();
        }

        [Backend_AuthorizeAttrinbite]
        public ActionResult Permission()
        {
            return View();
        }

        #region Helpers

        public System.Web.Security.FormsAuthenticationTicket LoginInformation_Backend()
        {
            System.Web.Security.FormsIdentity id;
            System.Web.Security.FormsAuthenticationTicket ticket = null;

            id = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
            ticket = id.Ticket;
            return ticket;
        }

        [Obsolete("Method have some error. You should create new method.")]
        protected void CheckPermissionWihtRedirect(
            bool isAdmin = false,
            bool isUser = false,
            bool isProduct = false,
            bool isPayment = false,
            bool isLog = false,
            bool isESlide = false,
            bool isEProduct = false,
            bool isEStock = false,
            bool isEPayView = false,
            bool isEPayConfirm = false,
            bool isEDeliver = false,
            bool isEPromotion = false,
            bool isEStatistic = false)
        {
            if (!CheckPermission(
                isAdmin: isAdmin,
                isUser: isUser,
                isProduct: isProduct,
                isPayment: isPayment,
                isLog: isLog,
                isESlide: isESlide,
                isEProduct: isEProduct,
                isEStock: isEStock,
                isEPayView: isEPayView,
                isEPayConfirm: isEPayConfirm,
                isEDeliver: isEDeliver,
                isEPromotion: isEPromotion,
                isEStatistic: isEStatistic))
                Response.Redirect(Url.Action("Permission", "Backend"));
        }

        /// <summary>
        /// Give true when have permission
        /// </summary>
        /// <param name="isAdmin">Request Admin</param>
        /// <param name="isUser">Request User</param>
        /// <param name="isProduct">Request Product</param>
        /// <param name="isPayment">Request Payment</param>
        /// <param name="isLog">Request Log</param>
        /// <param name="isESlide">Request ESlide</param>
        /// <param name="isEProduct">Request EProduct</param>
        /// <param name="isEStock">Request EStock</param>
        /// <param name="isEPayView">Request EPayView</param>
        /// <param name="isEPayConfirm">Request EPayConfirm</param>
        /// <param name="isEDeliver">Request EDeliver</param>
        /// <param name="isEPromotion">Request EPromotion</param>
        /// <param name="isEStatistic">Request EStatistic</param>
        /// <returns>Have permission</returns>
        protected bool CheckPermission(
                    bool isAdmin = false,
                    bool isUser = false,
                    bool isProduct = false,
                    bool isPayment = false,
                    bool isLog = false,
                    bool isESlide = false,
                    bool isEProduct = false,
                    bool isEStock = false,
                    bool isEPayView = false,
                    bool isEPayConfirm = false,
                    bool isEDeliver = false,
                    bool isEPromotion = false,
                    bool isEStatistic = false)
        {
            using (var context = new Database_MainEntities1())
            {
                UserAdmin useradmin = new UserAdmin();
                useradmin = context.UserAdmins.Find(CurrentUseradminId);
                Permission permission = useradmin.Permission;

                if (isAdmin == true && permission.isAdmin == false)
                    return false;

                if (isUser == true && permission.isUser == false)
                    return false;

                if (isProduct == true && permission.isProduct == false)
                    return false;

                if (isPayment == true && permission.isPayment == false)
                    return false;

                if (isLog == true && permission.isLog == false)
                    return false;

                if (isESlide == true && permission.isESlide == false)
                    return false;

                if (isEProduct == true && permission.isEProduct == false)
                    return false;

                if (isEStock == true && permission.isEStock == false)
                    return false;

                if (isEPayView == true && permission.isEPayView == false)
                    return false;

                if (isEPayConfirm == true && permission.isEPayConfirm == false)
                    return false;

                if (isEDeliver == true && permission.isEDeliver == false)
                    return false;

                if (isEPromotion == true && permission.isEPromotion == false)
                    return false;

                if (isEStatistic == true && permission.isEStatistic == false)
                    return false;

                return true;
            }
        }

        #endregion Helpers
    }
}