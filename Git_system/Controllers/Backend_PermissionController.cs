using Git_system.Models;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_PermissionController : BackendController
    {
        //
        // GET: /Backend_Permission/

        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(isAdmin: true);
            return View(db.Permissions.ToList());
        }

        //
        // GET: /Backend_Permission/Create
        public ActionResult Create()
        {
            CheckPermissionWihtRedirect(isAdmin: true);
            return View(new Permission());
        }

        //
        // POST: /Backend_Permission/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Permission permission)
        {
            CheckPermissionWihtRedirect(isAdmin: true);
            if (ModelState.IsValid)
            {
                db.Permissions.Add(permission);
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่ม Permission", 1);//add to logfile
                return RedirectToAction("Home");
            }

            return View(permission);
        }

        public ActionResult Edit(int id = 0)
        {
            CheckPermissionWihtRedirect(isAdmin: true);
            Permission permission = new Permission();
            permission = db.Permissions.Find(id);
            return View(permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Permission permission)
        {
            CheckPermissionWihtRedirect(isAdmin: true);
            if (ModelState.IsValid)
            {
                Permission membershipOriginalValues = db.Permissions.Find(permission.permissionId);
                db.Entry(membershipOriginalValues).CurrentValues.SetValues(permission);
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไข Permission Id = " + permission.permissionId, 1);//add to logfile
                //return RedirectToAction("Home");
            }
            return View(permission);
        }
    }
}