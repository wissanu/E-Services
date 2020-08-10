using Git_system.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_BenefitSettingController : BackendController
    {
        //
        // GET: /Backend_BenefitSetting/

        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            return View(db.Benefits.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        public ActionResult Home(Benefit[] benefits)
        {
            foreach (Benefit benefit in benefits)
            {
                db.Entry(benefit).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Home");
        }
    }
}