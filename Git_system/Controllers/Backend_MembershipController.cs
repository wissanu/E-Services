using Git_system.Models;
using Git_system.Models.Form;
using Git_system.MultiResources;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_MembershipController : BackendController
    {
        //
        //
        // HOME
        public ActionResult Home()
        {
            if (!CheckPermission(isUser: true))
                return RedirectToAction("Permission", "Backend");
            var memberships = db.Memberships.Where(m => m.MembershipRegisterTypeId != 1 && m.MembershipRegisterTypeId != 2);
            System.Collections.Generic.List<Frontend_RegisterStep2> frontend_RegisterStep2 = new System.Collections.Generic.List<Frontend_RegisterStep2>();
            foreach (Membership item in memberships)
            {
                item.Mobile = item.Mobile.toPhoneString();
                item.Fax = item.Fax.toPhoneString();
                item.Tel = item.Tel.toPhoneString();
                item.MobileCRM = item.MobileCRM.toPhoneString();
                frontend_RegisterStep2.Add(item.toFrontend_RegisterStep2());
            }
            return View(frontend_RegisterStep2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Home(string sendMailTo)
        {
            if (!CheckPermission(isUser: true))
                return RedirectToAction("Permission", "Backend");
            int membershipId = System.Convert.ToInt32(sendMailTo);
            string Language = "TH";
            Membership membership = db.Memberships.Find(membershipId);
            if (membership.MembershipRegisterTypeId == 2 || membership.MembershipRegisterTypeId == 4 || membership.MembershipRegisterTypeId == 6)
                Language = "EN";
            Git_system.Helper.EmailMethod.SendEmailForResetPassword(membership, Language);
            return RedirectToAction("Home");
        }

        public ActionResult Detail(int id = 0)
        {
            if (!CheckPermission(isUser: true))
                return RedirectToAction("Permission", "Backend");
            Membership membership = db.Memberships.Find(id);

            Frontend_RegisterStep2 frontend_RegisterStep2 = membership.toFrontend_RegisterStep2();

            ViewBag.MembershipRegisterTypeId = new SelectList(db.MembershipRegisterTypes, "Id", "Name", frontend_RegisterStep2.MembershipRegisterTypeId);

            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            bool ignoreCase = true;
            StringComparer compTH = StringComparer.Create(ci, ignoreCase);
            ViewBag.Amphur = new SelectList(db.Amphurs.Where(a => a.PROVINCE_ID.Equals(frontend_RegisterStep2.Province)).ToList().OrderBy(a => a.AMPHUR_NAME, compTH), "AMPHUR_ID", "AMPHUR_NAME", frontend_RegisterStep2.Amphur);
            ViewBag.Province = new SelectList(db.Provinces.ToList().OrderBy(p => p.PROVINCE_NAME, compTH), "PROVINCE_ID", "PROVINCE_NAME", frontend_RegisterStep2.Province);
            ViewBag.District = new SelectList(db.Districts.Where(d => d.AMPHUR_ID.Equals(frontend_RegisterStep2.Amphur)).ToList().OrderBy(d => d.DISTRICT_NAME, compTH), "DISTRICT_ID", "DISTRICT_NAME", frontend_RegisterStep2.District);
            ViewBag.Country = new SelectList(db.Countries.OrderBy(c => c.Name), "Id", "Name", frontend_RegisterStep2.Country);

            ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH", frontend_RegisterStep2.QuestionListId);

            return View(frontend_RegisterStep2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(Frontend_RegisterStep2 frontend_RegisterStep2)
        {
            if (!CheckPermission(isUser: true))
                return RedirectToAction("Permission", "Backend");

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
                Membership membershipOriginalValues = db.Memberships.Find(frontend_RegisterStep2.Id);
                Membership membership = frontend_RegisterStep2.toMembership();
                membership.Email = membershipOriginalValues.Email;
                membership.Password = membershipOriginalValues.Password.ToString();
                membership.Id = membershipOriginalValues.Id;
                membership.Active = membershipOriginalValues.Active;
                if (membership.RegisterDateExp == null)
                    if ((membership.MembershipRegisterTypeId != membershipOriginalValues.MembershipRegisterTypeId) || (membership.MembershipRegisterTypeId != 1 && membership.MembershipRegisterTypeId != 2))
                        membership.RegisterDateExp = System.DateTime.Now;

                if ((membershipOriginalValues.IdCRM == null && membership.IdCRM != null) || (membership.IdCRM != membershipOriginalValues.IdCRM))
                    Helper.EmailMethod.SendEmailWhenChangeCRM(membership: membership);

                db.Entry(membershipOriginalValues).CurrentValues.SetValues(membership);
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสมาชิก Id = " + membership.Id, 1);//add to logfile

                //return RedirectToAction("Home");
                return RedirectToAction("Detail");
            }
            ViewBag.MembershipRegisterTypeId = new SelectList(db.MembershipRegisterTypes, "Id", "Name", frontend_RegisterStep2.MembershipRegisterTypeId);

            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            bool ignoreCase = true;
            StringComparer compTH = StringComparer.Create(ci, ignoreCase);
            ViewBag.Amphur = new SelectList(db.Amphurs.Where(a => a.PROVINCE_ID.Equals(frontend_RegisterStep2.Province)).ToList().OrderBy(a => a.AMPHUR_NAME, compTH), "AMPHUR_ID", "AMPHUR_NAME", frontend_RegisterStep2.Amphur);
            ViewBag.Province = new SelectList(db.Provinces.ToList().OrderBy(p => p.PROVINCE_NAME, compTH), "PROVINCE_ID", "PROVINCE_NAME", frontend_RegisterStep2.Province);
            ViewBag.District = new SelectList(db.Districts.Where(d => d.AMPHUR_ID.Equals(frontend_RegisterStep2.Amphur)).ToList().OrderBy(d => d.DISTRICT_NAME, compTH), "DISTRICT_ID", "DISTRICT_NAME", frontend_RegisterStep2.District);
            ViewBag.Country = new SelectList(db.Countries.OrderBy(c => c.Name), "Id", "Name", frontend_RegisterStep2.Country);

            ViewBag.QuestionListId = new SelectList(db.QuestionLists, "Id", "QuestionTH", frontend_RegisterStep2.QuestionListId);

            return View(frontend_RegisterStep2);
        }

        public ActionResult Payment()
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            var pays = db.Pays.Where(p => p.Type == 1).OrderByDescending(p => p.Id);
            return View(pays.ToList());
        }

        public ActionResult PaymentDetail(int id = 0)
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            Pay pay = db.Pays.Find(id);
            var confirmPayments = db.ConfirmPayments.Where(c => c.PayId == id);
            ViewBag.confirmPayments = confirmPayments;
            return View(pay);
        }

        public ActionResult Confirm()
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            var ConfirmPayments = db.ConfirmPayments.Where(c => c.Pay.Type == 1).OrderByDescending(m => m.Id);
            return View(ConfirmPayments.ToList());
        }

        public ActionResult ConfirmDetail(int id = 0)
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            ConfirmPayment ConfirmPayment = db.ConfirmPayments.Find(id);
            return View(ConfirmPayment);
        }

        public ActionResult ConfirmMembership(int id = 0)
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            Pay pay = db.Pays.Find(id);
            ViewBag.ProcessStatusTypeId = new SelectList(db.ProcessStatusTypes, "Id", "Name", pay.ProcessStatusTypeId);
            return View(pay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmMembership(Pay pay)
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            if (ModelState.IsValid)
            {
                var privateDb = new Database_MainEntities1();
                var oldPay = privateDb.Pays.Find(pay.Id);
                var oldProcessStatus = oldPay.ProcessStatusTypeId;
                privateDb.Dispose();
                pay.Type = oldPay.Type;
                var paySave = oldPay;
                paySave.ProcessStatusTypeId = pay.ProcessStatusTypeId;
                paySave.PayItems = db.PayItems.Where(p => p.PayId == pay.Id).ToList();
                db.Entry(paySave).State = EntityState.Unchanged;
                db.Entry(paySave).Property(x => x.ProcessStatusTypeId).IsModified = true;
                db.SaveChanges();

                if (oldProcessStatus == 1 && paySave.ProcessStatusTypeId == 2)
                    if (PayHelper.comfirmRegisterMembership(paySave))
                    {
                        Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสถานะการจ่ายเงิน PayId = " + paySave.Id + " และ แก้ไขสถาณะของผู้ลงทะเบียน MembershipId = " + pay.MembershipId, 1);

                        Helper.EmailMethod.SendEmailForPaymentSuccess(paySave);
                    }

                //return RedirectToAction("Home");
                return RedirectToAction("ConfirmMembership");
            }
            ViewBag.ProcessStatusTypeId = new SelectList(db.ProcessStatusTypes, "Id", "Name", pay.ProcessStatusTypeId);
            return View(pay);
        }

        public ActionResult ConfigMembershipRegister()
        {
            if (!CheckPermission(isAdmin: true))
                return RedirectToAction("Permission", "Backend");

            var products = db.Products.Where(p => p.Group == 0);
            return View(products.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfigMembershipRegister(Product item, String command = "", Boolean membershipCredit = false)
        {
            if (!CheckPermission(isAdmin: true))
                return RedirectToAction("Permission", "Backend");

            if (command == "membershipPrice")
            {
                Product OriginalValues = db.Products.Find(item.Id);
                Product product = new Product();
                product = OriginalValues;

                product.Price = item.Price;
                product.PriceInter = item.Price;
                product.PriceIndividual = item.Price;
                product.PriceIndividualInter = item.Price;
                product.PriceCorporate = item.Price;
                product.PriceCorporateInter = item.Price;

                db.Entry(OriginalValues).CurrentValues.SetValues(product);
                db.SaveChangesAsync();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขราคาสมัครสมาชิกสถาบัน", 1);
            }
            else if (command == "membershipCredit")
            {
                var ps = db.Products.Where(p => p.Group.Equals(0));
                foreach (var p in ps)
                {
                    p.isCreditCard = membershipCredit;
                    db.Entry(p).State = EntityState.Modified;
                }
                db.SaveChangesAsync();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขการชำระด้วยบัตรเครดิตของการสมัครสมาชิกสถาบัน", 1);
            }

            return RedirectToAction("ConfigMembershipRegister");
        }
    }
}