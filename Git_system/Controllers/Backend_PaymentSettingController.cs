using Git_system.Models;
using Git_system.Models.Form;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_PaymentSettingController : BackendController
    {
        //
        // GET: /Backend_PaymentSetting/

        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            ViewBag.ConfirmPaymentBankTypes = db.ConfirmPaymentBankTypes;
            ViewBag.CreditCardSetting = Git_system.Models.ExtensionMethod.GetForCreditCardSetting();
            ViewBag.EserviceVat = new Backend_VatSetting().Get();
            ViewBag.priceFromGroup = new PriceFromGroup();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Home(Backend_CreditCardSetting CreditCardSetting, string command, Backend_VatSetting EServiceVat, PriceFromGroup priceFromGroup)
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            if (command == "SaveCreditCardSetting")
            {
                CreditCardSetting.SetToWebconfig();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "แก้ไขการตั้งค่าบัตรเครดิต", 1);//add to logfile
            }
            else if (command == "SaveVat")
            {
                EServiceVat.Save();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "แก้ไขการตั้งค่าภาษีมูลค่าเพิ่ม", 1);//add to logfile
            }
            else if (command == "SavePriceFromGroup")
            {
                priceFromGroup.SaveMe();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "แก้ไขการตั้งค่าราคาการจัดส่งตามกลุ่ม", 1);//add to logfile
            }

            ViewBag.ConfirmPaymentBankTypes = db.ConfirmPaymentBankTypes;
            ViewBag.CreditCardSetting = CreditCardSetting != null ? CreditCardSetting : Git_system.Models.ExtensionMethod.GetForCreditCardSetting();
            ViewBag.EserviceVat = (EServiceVat.Vat != 0) ? EServiceVat : new Backend_VatSetting().Get();
            ViewBag.priceFromGroup = priceFromGroup != null ? priceFromGroup : new PriceFromGroup();
            return View();
        }

        public ActionResult CreateConfirmPaymentBankType()
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmPaymentBankType(ConfirmPaymentBankType confirmPaymentBankType)
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            if (ModelState.IsValid)
            {
                db.ConfirmPaymentBankTypes.Add(confirmPaymentBankType);
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่งรายละเอียดการชำระเงินผ่านธนาคาร", 1);//add to logfile
                return RedirectToAction("Home"); ;
            }
            return View(confirmPaymentBankType);
        }

        public ActionResult EditConfirmPaymentBankType(int id = 0)
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            ConfirmPaymentBankType confirmPaymentBankType = new ConfirmPaymentBankType();
            confirmPaymentBankType = db.ConfirmPaymentBankTypes.Find(id);
            return View(confirmPaymentBankType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmPaymentBankType(ConfirmPaymentBankType confirmPaymentBankType)
        {
            if (ModelState.IsValid)
            {
                CheckPermissionWihtRedirect(true, false, false, false, false);
                db.Entry(confirmPaymentBankType).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return View(confirmPaymentBankType);
        }
    }
}