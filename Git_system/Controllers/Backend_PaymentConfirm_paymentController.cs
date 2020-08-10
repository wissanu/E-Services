using Git_system.Models;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_PaymentConfirm_paymentController : BackendController
    {
        public ActionResult Home()
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            var ConfirmPayments = db.ConfirmPayments.Where(c => c.Pay.Type == 2).OrderByDescending(m => m.Id);
            return View(ConfirmPayments.ToList());
        }

        public ActionResult Detail(int id = 0)
        {
            if (!CheckPermission(isPayment: true))
                return RedirectToAction("Permission", "Backend");
            ConfirmPayment ConfirmPayment = db.ConfirmPayments.Find(id);
            return View(ConfirmPayment);
        }
    }
}