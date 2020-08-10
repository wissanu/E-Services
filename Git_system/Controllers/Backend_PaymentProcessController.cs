using Git_system.Models;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_PaymentProcessController : BackendController
    {
        public ActionResult Home()
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            var pays = db.Pays.Where(p => p.Type == 2).OrderByDescending(p => p.Id);
            return View(pays.ToList());
        }

        public ActionResult All()
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            var pays = db.Pays;
            return View(pays.ToList());
        }

        public ActionResult Detail(int id = 0)
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            var pay = db.Pays.Find(id);
            ViewBag.ProcessStatusTypeId = new SelectList(db.ProcessStatusTypes, "Id", "Name", pay.ProcessStatusTypeId);
            return View(pay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(Pay pay)
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
                paySave.Note = pay.Note;
                db.Entry(paySave).State = System.Data.Entity.EntityState.Unchanged;
                db.Entry(paySave).Property(x => x.ProcessStatusTypeId).IsModified = true;
                db.Entry(paySave).Property(x => x.Note).IsModified = true;
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสถานะการชำระเงิน PayId = " + paySave.Id, 1);//add to logfile
                if (oldProcessStatus == 1 && paySave.ProcessStatusTypeId == 2)
                {
                    Helper.EmailMethod.SendEmailForPaymentSuccess(paySave);
                }
                return RedirectToAction("Detail");
            }
            ViewBag.ProcessStatusTypeId = new SelectList(db.ProcessStatusTypes, "Id", "Name", pay.ProcessStatusTypeId);
            return View();
        }
    }
}