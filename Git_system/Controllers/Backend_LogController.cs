using Git_system.App_Code;
using Git_system.Models;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_LogController : BackendController
    {
        //
        // GET: /Backend_Log/
        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(false, false, false, false, true);
            return View(db.Logs.OrderByDescending(l => l.Id).ToList());
        }

        //
        // POST: /Backend_Log/
        [HttpPost, ActionName("Home")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            CheckPermissionWihtRedirect(false, false, false, false, true);
            var list = db.Logs.ToList();
            foreach (var item in list)
            {
                Log log = db.Logs.Find(item.Id);
                db.Logs.Remove(log);
                db.SaveChanges();
            }
            LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "เคลียร์ล็อก", 1);//add to logfile

            return RedirectToAction("Home");
        }
    }
}