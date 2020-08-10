using Git_system.App_Code;
using Git_system.Models;
using Git_system.Models.Form;
using Git_system.MultiResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    public class HomeController : Git_system.Helper.FrontendController
    {
        private void login_and_save_cookie(Frontend_LoginModel login, List<Membership> memberships)
        {
            Membership membershipLogin = memberships.FirstOrDefault();
            changeLanguage(membershipLogin.isLocal() ? "th" : "en"); // change language by membership.
            string UserData = Newtonsoft.Json.JsonConvert.SerializeObject(new UserAuthorize(membershipLogin.Id, membershipLogin.Email, "User"), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

            System.Web.Security.FormsAuthentication.Initialize();
            System.Web.Security.FormsAuthenticationTicket authTicket = new System.Web.Security.FormsAuthenticationTicket(1, login.Email, DateTime.Now, DateTime.Now.AddMinutes(60), false, UserData);
            string encTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookieAut = new HttpCookie(".FOENAUTH", encTicket);
            faCookieAut.HttpOnly = true;
            Response.Cookies.Add(faCookieAut);
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            HttpCookie facAut = Request.Cookies[".FOENAUTH"];
            System.Web.Security.FormsAuthenticationTicket _FOENAUTH = facAut == null ? new System.Web.Security.FormsAuthenticationTicket(null, true, 0) : System.Web.Security.FormsAuthentication.Decrypt(facAut.Value);

            if (_FOENAUTH.Name != null)//ต้องกำหนดใน webconfig ด้วย
            {
                UserAuthorize backend_authorize = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuthorize>(_FOENAUTH.UserData);
                if (backend_authorize.Type.Equals("User"))
                {
                    return RedirectToAction("Home");
                }
                else
                {
                    frontEndLogout();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Frontend_LoginModel login)
        {
            if (ModelState.IsValid)
            {
                String passHash = (Cryptography.SHA512SaltAddSalt(login.Password, login.Email)).ToString();

                List<Membership> membership = (from c in db.Memberships where c.Email.Equals(login.Email) && c.Password.Equals(passHash) select c).ToList();
                int count_membership = membership.Count();

                if (count_membership == 1)
                {
                    if (membership.FirstOrDefault().Active == true)
                    {
                        login_and_save_cookie(login, membership);
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", Multi.CheckMembershipActive);
                    }
                }
                else
                {
                    ModelState.AddModelError("", Multi.CheckEmailOrPassword);
                }
            }
            return View(login);
        }

        [HttpPost]
        public ActionResult LoginForm(Frontend_LoginModel login)
        {
            String passHash = (Cryptography.SHA512SaltAddSalt(login.Password, login.Email)).ToString();

            List<Membership> membership = (from c in db.Memberships where c.Email.Equals(login.Email) && c.Password.Equals(passHash) select c).ToList();
            int count_membership = membership.Count();

            if (count_membership == 1)
            {
                if (membership.FirstOrDefault().Active == true)
                {
                    login_and_save_cookie(login, membership);
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", Multi.CheckMembershipActive);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", Multi.CheckEmailOrPassword);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult LoginForm2(Frontend_LoginModel2 login2)
        {
            Frontend_LoginModel login = login2.ToFrontend_loginModel();
            String passHash = (Cryptography.SHA512SaltAddSalt(login.Password, login.Email)).ToString();

            changeLanguage(language: login2.Lang);

            List<Membership> membership = (from c in db.Memberships where c.Email.Equals(login.Email) && c.Password.Equals(passHash) select c).ToList();
            int count_membership = membership.Count();

            if (count_membership == 1)
            {
                if (membership.FirstOrDefault().Active == true)
                {
                    login_and_save_cookie(login, membership);
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", Multi.CheckMembershipActive);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", Multi.CheckEmailOrPassword);
                return RedirectToAction("Index");
            }
        }

        public ActionResult RegisterStep1(string Lang)
        {
            changeLanguage(language: Lang);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStep1(Frontend_RegisterStep1 register)
        {
            bool captchaImageCheck = this.Session["CaptchaImageText"] != null ? register.CaptchaImage == this.Session["CaptchaImageText"].ToString() : false;
            if (!captchaImageCheck)
                ModelState.AddModelError("CaptchaImage", Multi.Image_code_is_not_valid);

            if (ModelState.IsValid && captchaImageCheck)
            {
                if (((from c in db.Memberships where (c.Email).ToUpper().Equals((register.Email).ToUpper()) select c).ToList()).Count() >= 1)
                {
                    ModelState.AddModelError("Email", Multi.Email_Is_Using);
                }
                else
                {
                    if (register.MembershipNumber != null && register.MembershipNumber != "")
                        register.MembershipNumber = register.MembershipNumber.ToUpper();

                    Frontend_Register frontend_Register = new Frontend_Register();
                    frontend_Register.Membership = register.toMembership();

                    if (register.ExpCRM != null && register.MembershipNumber != null)
                    {
                        DateTime ExpCRM = new DateTime();
                        if (register.ExpCRM != null)
                            ExpCRM = Convert.ToDateTime(register.ExpCRM);
                        string CheckCRMUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["CheckCRMUrl"];
                        string getforweb = GETForWeb(CheckCRMUrl + "/?code=" + register.MembershipNumber + "&ExpCRM=" + ExpCRM.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("en")));
                        Frontend_CRM frontend_CRM = new Frontend_CRM();
                        if (getforweb != null)
                            frontend_CRM = Newtonsoft.Json.JsonConvert.DeserializeObject<Frontend_CRM>(getforweb);

                        if (frontend_CRM.IsSuccess == true)
                        {
                            frontend_Register.RegisterType = 1;
                            frontend_Register.Membership.Zipcode = frontend_CRM.Zipcode;
                            frontend_Register.Membership.TelNo = frontend_CRM.Tel;
                            frontend_Register.Membership.FaxNo = frontend_CRM.Fax;
                            frontend_Register.Membership.MobileNo = frontend_CRM.Mobile;
                            frontend_Register.Membership.GroupCRM = frontend_CRM.GroupId.ToString();
                            frontend_Register.Membership.MobileCRM = frontend_CRM.Tel;

                            Session.Add("frontend_RegisterGet", Newtonsoft.Json.JsonConvert.SerializeObject(frontend_Register, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore }));
                            return RedirectToAction("RegisterStep2", "Home");
                        }
                        else
                        {
                            register.ExpCRM = null;
                            register.MembershipNumber = null;
                            ModelState.AddModelError("ExpCRM", Multi.Validation_InformationIncorrect);
                        }
                    }
                    else
                    {
                        Session.Add("frontend_RegisterGet", Newtonsoft.Json.JsonConvert.SerializeObject(frontend_Register, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore }));
                        return RedirectToAction("RegisterStep2", "Home");
                    }
                }
            }
            return View(register);
        }

        public ActionResult RegisterStep2(string frontend_RegisterGet)
        {
            if (Session["frontend_RegisterGet"] == null)
                return RedirectToAction("RegisterStep1", "Home");

            Frontend_Register frontend_Register = new Frontend_Register();
            frontend_Register = Newtonsoft.Json.JsonConvert.DeserializeObject<Frontend_Register>(Session["frontend_RegisterGet"].ToString());
            if (_LanguageType == "TH")
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH");
            else
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionEN");

            if (frontend_Register.RegisterType == 1)
                ViewBag.RegisterTypeMessage = Multi.ActionRegisterStep2String1;
            else
                ViewBag.RegisterTypeMessage = Multi.ActionRegisterStep2String2;
            Frontend_RegisterStep2 membership = new Frontend_RegisterStep2();
            membership = frontend_Register.Membership;
            return View(membership);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStep2(Frontend_RegisterStep2 frontend_RegisterStep2)
        {
            var checkSameEmail = db.Memberships.Where(c => c.Email.ToUpper().Equals(frontend_RegisterStep2.Email.ToUpper())).Count() == 0;

            if (!checkSameEmail)
            {
                ModelState.AddModelError("Email", Multi.Email_Is_Using);
            }

            if (frontend_RegisterStep2.Country == Frontend_RegisterStep2.CountryType.Thailand.GetHashCode())
            {
                if (frontend_RegisterStep2.Province == 0)
                    ModelState.AddModelError("Province", Multi.RequiredField);

                if (frontend_RegisterStep2.Amphur == 0)
                    ModelState.AddModelError("Amphur", Multi.RequiredField);

                if (frontend_RegisterStep2.District == 0)
                    ModelState.AddModelError("District", Multi.RequiredField);
            }

            if (ModelState.IsValid && checkSameEmail)
            {
                Membership membership = new Membership();
                membership = frontend_RegisterStep2.toMembership();

                if (_LanguageType == "TH")
                    membership.MembershipRegisterTypeId = 1;
                else
                    membership.MembershipRegisterTypeId = 2;

                String passHash = (Cryptography.SHA512SaltAddSalt(membership.Password, membership.Email)).ToString();
                membership.Password = passHash;

                db.Memberships.Add(membership);
                db.SaveChanges();

                Git_system.Helper.EmailMethod.SendEmailForRegister(membership, _LanguageType);
                Git_system.Helper.EmailMethod.SendEmailForMembershipRegisterToAdmin(membership);

                return RedirectToAction("RegisterStep3");
            }

            if (_LanguageType == "TH")
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH", frontend_RegisterStep2.QuestionListId);
            else
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionEN", frontend_RegisterStep2.QuestionListId);

            return View(frontend_RegisterStep2);
        }

        public ActionResult RegisterStep3()
        {
            Session["frontend_RegisterGet"] = null;
            return View();
        }

        [Frontend_AuthorizeAttrinbite]
        public ActionResult Home()
        {
            Membership membership = db.Memberships.Find(CurrentUserId);
            return View(membership);
        }

        public ActionResult RegisterMembership(string Lang)
        {
            changeLanguage(language: Lang);

            Membership membership = db.Memberships.Find(CurrentUserId);
            membership = membership.CheckMembershipType(Multi.__LanguageType);
            int MembershipRegisterTypeId = membership.MembershipRegisterTypeId;
            if (membership.isCorporate() || membership.isIndividual())
            {
                int idRegister = 0;
                if (MembershipRegisterTypeId == 3) { idRegister = 1; }
                else if (MembershipRegisterTypeId == 5) { idRegister = 2; }
                else if (MembershipRegisterTypeId == 4) { idRegister = 3; }
                else if (MembershipRegisterTypeId == 6) { idRegister = 4; }

                return RedirectToAction("MembershipRegister", new { @id = idRegister });
            }
            double product_Individual_Price = 0;
            double product_Corparate_Price = 0;
            switch (MembershipRegisterTypeId)
            {
                case 1:
                    product_Individual_Price = db.Products.Find(1).Price;
                    product_Corparate_Price = db.Products.Find(2).Price;
                    break;

                case 2:
                    product_Individual_Price = db.Products.Find(3).PriceInter;
                    product_Corparate_Price = db.Products.Find(4).PriceInter;
                    break;
            }
            ViewBag.product_Individual_Price = product_Individual_Price;
            ViewBag.product_Corparate_Price = product_Corparate_Price;
            return View(membership);
        }

        public ActionResult Product(string Lang)
        {
            changeLanguage(language: Lang);

            Membership membershipView = db.Memberships.Find(CurrentUserId);
            membershipView = membershipView.CheckMembershipType(Multi.__LanguageType);
            ViewBag.membershipView = membershipView;

            CourseManageDatabase.updateStatusCourse();
            List<Product> product = db.Products.Where(p => p.Active == true && p.Group != 0).OrderByDescending(p => p.Id).ToList();

            product = Models.Helper.check_culture(_LanguageType, membershipView) == "th" ? product.ToList() : product.ToList();
            product = Models.Helper.check_culture(_LanguageType, membershipView) == "en-GB" ? product.Where(p => p.SupportEN == true).ToList() : product.ToList();

            List<Product> product_show = new List<Product>();
            foreach (Product item in product)
            {
                item.Courses = item.Courses.Where(c => c.Active == true && c.DateStart >= DateTime.Now.AddDays(-1)).OrderBy(c => c.Id).ToList();
                if (item.Courses.Count != 0)
                    product_show.Add(item);
            }

            ViewBag.productNews = db.ProductNews.ToList();

            return View(product_show);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Frontend_AuthorizeAttrinbite]
        public ActionResult Product(int[] selectedObjects)
        {
            if (selectedObjects == null)
                return RedirectToAction("Product");

            List<PayItem> listpayitem = new List<PayItem>();
            foreach (var payitem in selectedObjects.Distinct())
            {
                PayItem addpay = new PayItem();
                addpay.CourseId = payitem;
                addpay.Quantity = 1;
                listpayitem.Add(addpay);
            }

            HttpCookie frontendCookiePayItem = new HttpCookie("PIM");//Username
            frontendCookiePayItem.Value = Newtonsoft.Json.JsonConvert.SerializeObject(listpayitem, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            Response.Cookies.Add(frontendCookiePayItem);

            return RedirectToAction("Payment");
        }

        [Frontend_AuthorizeAttrinbite]
        public ActionResult Payment()
        {
            List<PayItem> payItems;

            try
            {
                payItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PayItem>>(Request.Cookies["PIM"].Value);
                foreach (var item in payItems)
                {
                    Course course = (from c in db.Courses where c.Id.Equals(item.CourseId) && c.DateStart >= DateTime.Now select c).FirstOrDefault();
                    item.Course = course;
                }
            }
            catch
            {
                HttpCookie frontendCookiePayItem = new HttpCookie("PIM");
                frontendCookiePayItem.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(frontendCookiePayItem);

                return RedirectToAction("Product");
            }
            Membership membershipView = db.Memberships.Find(CurrentUserId);
            ViewBag.membershipView = membershipView;

            System.Collections.Generic.List<SelectListItem> list = new System.Collections.Generic.List<SelectListItem> { };
            list.Add(new SelectListItem { Value = "1", Text = Multi._MembershipRegisterString5, Selected = true });
            if (Git_system.Models.ExtensionMethod.GetForCreditCardSetting().Enable_CreditCard == true)
                if (payItems.Count() == payItems.Where(p => p.Course.Product.isCreditCard.Equals(true)).Count())
                    list.Add(new SelectListItem { Value = "2", Text = Multi._MembershipRegisterString6, Selected = false });
            ViewBag.PaymentTypes = list;

            return View(payItems.OrderBy(p => p.Course.ProductId).ToList());
        }

        [Frontend_AuthorizeAttrinbite]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(int id = 0, short payType = 0, string payFullname = "", string payAddress = "")
        {
            Membership membership = db.Memberships.Find(CurrentUserId);

            List<PayItem> payItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PayItem>>(Request.Cookies["PIM"].Value);
            Pay pay = SavePayment(membership.Id, 2, payType, payItems, payFullname, payAddress);//Save payment to database

            HttpCookie frontendCookiePayItem = new HttpCookie("PIM");
            frontendCookiePayItem.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(frontendCookiePayItem);

            if (payType == 2 && pay.ProcessStatusTypeId == 1)
            {
                return RedirectToAction("RedirectToCreditCard", "Home", new { amount = pay.Price.ToString("0.00"), reference_number = pay.Id });
            }
            else if (payType == 1 && pay.ProcessStatusTypeId == 1)
            {
                Git_system.Helper.EmailMethod.SendEmailForPayment(membership, _LanguageType, pay);
                //Debug email pay success
                //Git_system.Helper.EmailMethod.SendEmailForPaymentSuccess(pay, _LanguageType);
            }
            if (Array.Exists(new int[] { 1, 2 }, e => e == payType))
                Git_system.Helper.EmailMethod.SendEmailForPaymentToAdmin(membership, pay);

            return new Frontend_ShowMessage(Multi.ActionPaymentShowMessageTitle, Multi.ActionPaymentShowMessageHead, Multi.ActionPaymentShowMessageBody).getAction();
        }

        private List<SelectListItem> getBankList(string language = "TH")
        {
            List<SelectListItem> bankList = new List<SelectListItem>();
            bankList = db.ConfirmPaymentBankTypes.ToList().ConvertAll(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = String.Format("{0} ({1})", language == "TH" ? b.BanknameTH : b.BanknameEN, b.AccountNo)
                }
            );
            return bankList;
        }

        public ActionResult ConfirmPayment(int id)
        {
            if (id == 0)
                return RedirectToAction("Home");

            ViewBag.ConfirmPaymentBankTypeId = getBankList(_LanguageType);

            Pay pay = db.Pays.Find(id);
            var confirmPayment = new ConfirmPayment();
            confirmPayment.PayId = id;
            confirmPayment.Pay = pay;
            confirmPayment.Date = DateTime.Now;
            confirmPayment.Name = (pay.Membership.Firstname + "    " + pay.Membership.Lastname);

            return View(confirmPayment);
        }

        private Boolean validationFile(ModelStateDictionary modelState, string setErrTo, HttpPostedFileBase file, int limitsize)
        {
            if (!(file != null && file.ContentLength > 0))
            {
                modelState.AddModelError(setErrTo, Multi.RequiredFile);
                return false;
            }
            else if (file.ContentLength > limitsize)//size more limitsize MB
            {
                modelState.AddModelError(setErrTo, Multi.SizeMustNotMoreThan10MB);
                return false;
            }
            return true;
        }

        private string saveFile(string prefix, HttpPostedFileBase file, string pathSave = "~/Images/Uploads")
        {
            var fileName = Path.GetFileName(file.FileName);
            string filename = prefix + DateTime.Now.Ticks.ToString("X") + Path.GetExtension(fileName);//convert time to filename base16
            var path = Path.Combine(Server.MapPath(pathSave), filename);
            file.SaveAs(path);

            return filename;
        }

        private Boolean saveConfirmPayment(ConfirmPayment confirmPayment, HttpPostedFileBase file, HttpPostedFileBase filePersonal)
        {
            if (1 != confirmPayment.Pay.Type && confirmPayment.Pay.Type != 2)
                return false;
            confirmPayment.Invoice = "1";

            string filename = saveFile("P", file);
            confirmPayment.Filename = filename;

            if (confirmPayment.Pay.Type == 1)
            {
                string filenamePersonal = saveFile("C", filePersonal);
                confirmPayment.FilenameConfirm = filenamePersonal;
            }

            confirmPayment.Currency = Models.Helper.getCurrency(confirmPayment.Pay.Membership);
            db.ConfirmPayments.Add(confirmPayment);
            db.SaveChanges();

            Git_system.Helper.EmailMethod.SendEmailForConfirmPaymentToAdmin(confirmPayment.Pay.Membership, confirmPayment);

            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPayment(ConfirmPayment confirmPayment, HttpPostedFileBase file, HttpPostedFileBase filePersonal)
        {
            if (confirmPayment.PayId != 0)
                confirmPayment.Pay = db.Pays.Find(confirmPayment.PayId);

            if (ModelState.IsValid)
            {
                int countErr = 0;
                if (confirmPayment.Pay.Type == 1)
                {
                    if (!validationFile(ModelState, "UploadFilePersonal", filePersonal, 10485760))
                        countErr++;

                    if (!validationFile(ModelState, "UploadFile", file, 10485760))
                        countErr++;
                }
                else
                {
                    if (!validationFile(ModelState, "UploadFile", file, 10485760))
                        countErr++;
                }

                if (countErr == 0)
                {
                    if (saveConfirmPayment(confirmPayment, file, filePersonal))
                        return new Frontend_ShowMessage(Multi.ActionConfirmPaymentShowMessageTitle, Multi.ActionConfirmPaymentShowMessageHead, Multi.ActionConfirmPaymentShowMessageBody).getAction();
                }
            }

            ViewBag.ConfirmPaymentBankTypeId = getBankList(_LanguageType);

            return View(confirmPayment);
        }

        [Frontend_AuthorizeAttrinbite]
        public ActionResult Edit_Profile()
        {
            Membership membership = db.Memberships.Find(CurrentUserId);

            Frontend_RegisterStep2 frontend_RegisterStep2 = membership.toFrontend_RegisterStep2();

            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            bool ignoreCase = true;
            StringComparer compTH = StringComparer.Create(ci, ignoreCase);
            ViewBag.Amphur = new SelectList(db.Amphurs.Where(a => a.PROVINCE_ID.Equals(frontend_RegisterStep2.Province)).ToList().OrderBy(a => a.AMPHUR_NAME, compTH), "AMPHUR_ID", "AMPHUR_NAME", frontend_RegisterStep2.Amphur);
            ViewBag.Province = new SelectList(db.Provinces.ToList().OrderBy(p => p.PROVINCE_NAME, compTH), "PROVINCE_ID", "PROVINCE_NAME", frontend_RegisterStep2.Province);
            ViewBag.District = new SelectList(db.Districts.Where(d => d.AMPHUR_ID.Equals(frontend_RegisterStep2.Amphur)).ToList().OrderBy(d => d.DISTRICT_NAME, compTH), "DISTRICT_ID", "DISTRICT_NAME", frontend_RegisterStep2.District);
            ViewBag.Country = new SelectList(db.Countries.OrderBy(c => c.Name), "Id", "Name", frontend_RegisterStep2.Country);

            if (_LanguageType == "TH")
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH", frontend_RegisterStep2.QuestionListId);
            else
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionEN", frontend_RegisterStep2.QuestionListId);

            return View(frontend_RegisterStep2);
        }

        [Frontend_AuthorizeAttrinbite]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Profile(Frontend_RegisterStep2 frontend_RegisterStep2)
        {
            Membership membershipOriginalValues = db.Memberships.Find(CurrentUserId);
            Membership membership = frontend_RegisterStep2.toMembership();
            membership.Email = membershipOriginalValues.Email;
            membership.Password = membershipOriginalValues.Password.ToString();
            membership.RegisterDate = membershipOriginalValues.RegisterDate;
            membership.RegisterDateExp = membershipOriginalValues.RegisterDateExp;
            membership.Id = membershipOriginalValues.Id;
            membership.IdCRM = membershipOriginalValues.IdCRM;
            membership.MembershipRegisterType = membershipOriginalValues.MembershipRegisterType;
            membership.MembershipRegisterTypeId = membershipOriginalValues.MembershipRegisterTypeId;
            membership.Active = membershipOriginalValues.Active;
            frontend_RegisterStep2 = membership.toFrontend_RegisterStep2();

            if (frontend_RegisterStep2.Country == Frontend_RegisterStep2.CountryType.Thailand.GetHashCode())
            {
                if (frontend_RegisterStep2.Province == 0)
                    ModelState.AddModelError("Province", Multi.RequiredField);

                if (frontend_RegisterStep2.Amphur == 0)
                    ModelState.AddModelError("Amphur", Multi.RequiredField);

                if (frontend_RegisterStep2.District == 0)
                    ModelState.AddModelError("District", Multi.RequiredField);
            }

            if (ModelState.IsValid)
            {
                db.Entry(membershipOriginalValues).CurrentValues.SetValues(membership);

                db.SaveChanges();
                return RedirectToAction("Home");
            }

            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            bool ignoreCase = true;
            StringComparer compTH = StringComparer.Create(ci, ignoreCase);
            ViewBag.Amphur = new SelectList(db.Amphurs.Where(a => a.PROVINCE_ID.Equals(frontend_RegisterStep2.Province)).ToList().OrderBy(a => a.AMPHUR_NAME, compTH), "AMPHUR_ID", "AMPHUR_NAME", frontend_RegisterStep2.Amphur);
            ViewBag.Province = new SelectList(db.Provinces.ToList().OrderBy(p => p.PROVINCE_NAME, compTH), "PROVINCE_ID", "PROVINCE_NAME", frontend_RegisterStep2.Province);
            ViewBag.District = new SelectList(db.Districts.Where(d => d.AMPHUR_ID.Equals(frontend_RegisterStep2.Amphur)).ToList().OrderBy(d => d.DISTRICT_NAME, compTH), "DISTRICT_ID", "DISTRICT_NAME", frontend_RegisterStep2.District);
            ViewBag.Country = new SelectList(db.Countries.OrderBy(c => c.Name), "Id", "Name", frontend_RegisterStep2.Country);

            if (_LanguageType == "TH")
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH", frontend_RegisterStep2.QuestionListId);
            else
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionEN", frontend_RegisterStep2.QuestionListId);

            return View(frontend_RegisterStep2);
        }

        [Frontend_AuthorizeAttrinbite]
        public ActionResult MembershipRegister(int id = 0, string Lang = null)
        {
            changeLanguage(language: Lang);

            var MemberRegister = db.Courses.Where(c => c.Id == id);
            if (MemberRegister.Count() == 0)
            {
                return RedirectToAction("RegisterMembership");//Error
            }

            Membership membershipView = db.Memberships.Find(CurrentUserId);
            ViewBag.membershipView = membershipView;

            System.Collections.Generic.List<SelectListItem> list = new System.Collections.Generic.List<SelectListItem> { };
            list.Add(new SelectListItem { Value = "1", Text = Multi._MembershipRegisterString5, Selected = true });
            if (Git_system.Models.ExtensionMethod.GetForCreditCardSetting().Enable_CreditCard == true)
                if (MemberRegister.Count() == MemberRegister.Where(p => p.Product.isCreditCard.Equals(true)).Count())
                    list.Add(new SelectListItem { Value = "2", Text = Multi._MembershipRegisterString6, Selected = false });
            ViewBag.PaymentTypes = list;

            ViewBag.Benefit = (id == 1 || id == 3) ?
                                (Multi.__LanguageType == "TH") ?
                                    db.Benefits.Find(1).DetailTH : db.Benefits.Find(1).DetailEN
                                : (Multi.__LanguageType == "TH") ?
                                    db.Benefits.Find(2).DetailTH : db.Benefits.Find(2).DetailEN;

            return View(MemberRegister);
        }

        [Frontend_AuthorizeAttrinbite]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MembershipRegister(int id = 0, short payType = 0)
        {
            Membership membership = db.Memberships.Find(CurrentUserId);

            PayItem payitem = new PayItem();
            payitem.CourseId = id;
            payitem.Quantity = 1;

            List<PayItem> payItems = new List<PayItem>();
            payItems.Add(payitem);
            Pay pay = SavePayment(membership.Id, 1, payType, payItems);

            if (payType == 2)
            {
                return RedirectToAction("RedirectToCreditCard", "Home", new { amount = pay.Price.ToString("0.00"), reference_number = pay.Id });
            }
            else if (payType == 1)
            {
                Git_system.Helper.EmailMethod.SendEmailForMembershipRegister(membership, _LanguageType, pay);
                Git_system.Helper.EmailMethod.SendEmailForPaymentMembershipRegisterToAdmin(membership, pay);
            }

            return new Frontend_ShowMessage(Multi.ActionMembershipRegisterShowMessageTitle, Multi.ActionMembershipRegisterShowMessageHead, Multi.ActionMembershipRegisterShowMessageBody).getAction();
        }

        public ActionResult SendResetPassword(string Lang)
        {
            changeLanguage(language: Lang);

            if (_LanguageType == "TH")
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH");
            else
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionEN");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendResetPassword(Frontend_SendResetPassword Form)
        {
            if (ModelState.IsValid)
            {
                Membership membership = (from c in db.Memberships where c.Email.Equals(Form.Email) select c).FirstOrDefault();

                if (membership.Id == 0)
                {
                    ModelState.AddModelError("aLLerror", Multi.Validation_InformationIncorrect);//แจ้งเตือน
                }
                else
                {
                    if (Form.BirthDay != Convert.ToDateTime(membership.BirthDay) || Form.QuestionListId != membership.QuestionListId || Form.Answer != membership.Answer)
                        ModelState.AddModelError("aLLerror", Multi.Validation_InformationIncorrect);//แจ้งเตือน
                    else
                    {
                        Git_system.Helper.EmailMethod.SendEmailForResetPassword(membership, _LanguageType);

                        return new Frontend_ShowMessage(Multi.ActionSendResetPasswordShowMessageTitle, Multi.ActionSendResetPasswordShowMessageHead, Multi.ActionSendResetPasswordShowMessageBody).getAction();
                    }
                }
            }
            if (_LanguageType == "TH")
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH", Form.QuestionListId);
            else
                ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionEN", Form.QuestionListId);
            return View();
        }

        public ActionResult ResetPassword(string User, string Token)
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
        public ActionResult ResetPassword(Frontend_ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                long date = DateTime.UtcNow.Ticks;
                long dateout = date / 10000000;//1 sec
                dateout = dateout / 3600; //60 min
                string TokenHash = Git_system.App_Code.Cryptography.SHA1(string.Concat(resetPassword.User, dateout.ToString("X"))).ToString();
                bool tokenCheck = false;
                tokenCheck = (TokenHash == resetPassword.Token);
                ViewBag.CheckToken = tokenCheck;
                if (tokenCheck)
                {
                    Membership membership = new Membership();
                    membership = resetPassword.toMembership();

                    Membership membershipOriginalValues = db.Memberships.Find(membership.Id);
                    db.Entry(membershipOriginalValues).CurrentValues.SetValues(membership);

                    db.SaveChanges();
                    return new Frontend_ShowMessage(Multi.ActionSendResetPasswordShowMessageTitle, Multi.Password_Resetting, Multi.Complete_change_password).getAction();
                }
            }
            return View();
        }

        public ActionResult ShowMessage(string Title, string Header, string Message, string RedirectAction = "Home")
        {
            Frontend_ShowMessage frontend_ShowMessage = new Frontend_ShowMessage(Title, Header, Message, RedirectAction);
            return View(frontend_ShowMessage);
        }

        [HttpGet]
        public void ChangeLanuage(string language)
        {
            changeLanguage(language: language);
        }

        [HttpGet]
        [ActionName("ChangeLanguageType")]
        public ActionResult ChangeLanguageType(string actionname)
        {
            string CultureNameLanguageTypeValue = null;

            HttpCookie cultureCookie = Request.Cookies["FCNLT"];

            string Language = null;
            Language = cultureCookie.Value;
            switch (Language)
            {
                case "f11b8154674d1f97d062be097d3a5ccab0fc27ee":
                    CultureNameLanguageTypeValue = "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45";
                    break;

                case "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45":
                    CultureNameLanguageTypeValue = "f11b8154674d1f97d062be097d3a5ccab0fc27ee";
                    break;
            }
            HttpCookie CultureNameLanguageType = new HttpCookie("FCNLT");
            CultureNameLanguageType.Value = CultureNameLanguageTypeValue;
            CultureNameLanguageType.HttpOnly = true;
            Response.Cookies.Add(CultureNameLanguageType);

            return RedirectToAction(actionname);
        }

        public ActionResult error404()
        {
            return View();
        }

        public ActionResult ConfirmMembership(string K, string P)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(P);
            string email = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            string key = Cryptography.SHA1(string.Concat(email, "3*as09-3=128098okmaeji0IIlo-"));
            if (key == K)
            {
                Membership membership = db.Memberships.Where(m => m.Email.Equals(email)).FirstOrDefault();
                Membership membershipSave = membership;
                membershipSave.Active = true;
                db.Entry(membership).CurrentValues.SetValues(membershipSave);

                db.SaveChanges();
            }

            return new Frontend_ShowMessage(Multi.ActionMembershipRegisterShowMessageTitle, Multi.ActionMembershipRegisterShowMessageHead, Multi.MembershipConfirm).getAction();
        }

        [Frontend_AuthorizeAttrinbite]
        public string RedirectToCreditCard(double amount, int reference_number, string currency = "thb", string transaction = "sale")
        {
            var reference = "";
            if (transaction == "sale")
                reference = "s" + reference_number;
            else
                reference = "e" + reference_number;

            if (transaction == "sale")
            {
                Pay pay = db.Pays.Find(reference_number);
                if (pay.Membership.MembershipRegisterTypeId == 1 || pay.Membership.MembershipRegisterTypeId == 3 || pay.Membership.MembershipRegisterTypeId == 5)
                    currency = "thb";
                else
                    currency = "usd";
            }

            string str = Helper.PostMethod.RedirectAndPOSTString(Helper.CreditCard.getURL(), Helper.CreditCard.getRequest(reference: reference, amount: amount, currency: currency));

            return str;
        }

        public ActionResult receipt()
        {
            return View();
        }

        private static string beforRefNo = "";

        [HttpPost]
        public ActionResult receipt(FormCollection collection = null, string signature = "", string req_reference_number = "", string req_currency = "", string req_amount = "", DateTime? signed_date_time = null, string req_bill_to_forename = "", string req_bill_to_surname = "", string transaction_id = null, string decision = "DECLINE")
        {
            var transaction = new CreditCardTransaction(collection, signature, req_reference_number, req_currency, req_amount, signed_date_time, req_bill_to_forename, req_bill_to_surname, transaction_id, decision);

            return transaction.process(_LanguageType).getAction();
        }

        public ActionResult ResendActiveEmail(string email)
        {
            string emailMembership = System.Net.WebUtility.HtmlDecode(email);
            Membership membership = new Membership();
            membership = db.Memberships.Where(m => m.Email.Equals(emailMembership)).FirstOrDefault();

            Git_system.Helper.EmailMethod.SendEmailForRegister(membership, _LanguageType);

            return RedirectToAction("Index");
            //return View();
        }

        [Frontend_AuthorizeAttrinbite]
        public ActionResult PaymentHistory(int show = 1)
        {
            Membership membership = db.Memberships.Find(CurrentUserId);
            var pay = new List<Pay>();
            ViewBag.getShow = 1;
            if (show == 1)
            {
                pay = db.Pays.Where(p => p.MembershipId.Equals(membership.Id)).Where(p => p.PayTypeId == 1 && p.ProcessStatusTypeId == 1).OrderByDescending(p => p.Id).ToList();
            }
            else if (show == 2)
            {
                pay = db.Pays.Where(p => p.MembershipId.Equals(membership.Id)).OrderByDescending(p => p.Id).ToList();
                ViewBag.getShow = 2;
            }

            return View(pay);
        }

        //
        //--------------------ECom--------------------

        public ActionResult ProductView()
        {
            return View();
        }

        public ActionResult ProductViewDetail()
        {
            return View();
        }

        public ActionResult ProductViewMore()
        {
            return View();
        }

        public ActionResult ProductShoppingCart()
        {
            return View();
        }

        public ActionResult ProductShoppingDeliver()
        {
            return View();
        }

        public ActionResult ProductShoppingPayment()
        {
            return View();
        }

        public ActionResult ProductTrackShipment()
        {
            return View();
        }

        public ActionResult ProductOrderHistory()
        {
            return View();
        }

        public ActionResult ProductOrderHistoryDetail()
        {
            return View();
        }

        public ActionResult ProductConfirmPayment()
        {
            return View();
        }

        public ActionResult ProductElectronic()
        {
            return View();
        }

        //--------------------ECom--------------------

        public string APICheckPromotion(int M = 1, string P = "W3siSWQiOm51bGwsIlF1YW50aXR5IjpudWxsLCJQYXlJZCI6bnVsbCwiQ291cnNlSWQiOm51bGwsIlBheSI6bnVsbCwiQ291cnNlIjpudWxsfV0=")
        {
            List<PayItem> payItems = new List<PayItem>();
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(P);
                string jsonPayItems = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                payItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PayItem>>(jsonPayItems);
            }
            catch
            {
                return @"[{""Id"":0,""NameTH"":""Err key"",""NameEN"":""Err key"",""Price"":0}]";
            }

            string APIoutput = "";
            APIoutput = Newtonsoft.Json.JsonConvert.SerializeObject(APICheckPromotionWithVatAndTotalPrice(M, payItems), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            return APIoutput;
        }

        public string APILocation(int D, int V = -1)
        {
            string APIOut = "";

            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            bool ignoreCase = true;
            StringComparer compTH = StringComparer.Create(ci, ignoreCase);
            switch (D)
            {
                case 0://Countries
                    APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Countries.ToList().OrderBy(p => p.Name), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    break;

                case 1://Geographies
                    if (V == -1)
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Geographies.ToList().PlaningToJson(), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    else
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Geographies.Where(g => g.GEO_ID.Equals(V)).ToList().PlaningToJson(), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    break;

                case 2://Provinces
                    if (V == -1)
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Provinces.ToList().PlaningToJson().OrderBy(p => p.PROVINCE_NAME, compTH), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    else
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Provinces.Where(p => p.GEO_ID.Equals(V)).ToList().PlaningToJson().OrderBy(p => p.PROVINCE_NAME, compTH), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    break;

                case 3://Amphurs
                    if (V == -1)
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Amphurs.ToList().PlaningToJson().OrderBy(p => p.AMPHUR_NAME, compTH), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    else
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Amphurs.Where(a => a.PROVINCE_ID.Equals(V)).ToList().PlaningToJson().OrderBy(p => p.AMPHUR_NAME, compTH), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

                    break;

                case 4://Districts
                    if (V == -1)
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Districts.ToList().PlaningToJson().OrderBy(p => p.DISTRICT_NAME, compTH), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    else
                        APIOut = Newtonsoft.Json.JsonConvert.SerializeObject(db.Districts.Where(d => d.AMPHUR_ID.Equals(V)).ToList().PlaningToJson().OrderBy(p => p.DISTRICT_NAME, compTH), Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                    break;
            }

            return APIOut;
        }

        public string GETForWeb(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                try
                {
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                        // log errorText
                    }
                    throw;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}