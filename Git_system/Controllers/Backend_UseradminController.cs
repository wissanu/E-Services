using Git_system.App_Code;
using Git_system.Models;
using Git_system.Models.Form;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_UseradminController : BackendController
    {
        //
        // GET: /Backend_Useradmin/

        [BackendPermission(isAdmin: true)]
        public ActionResult Home()
        {
            return View(db.UserAdmins.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isAdmin: true)]
        public ActionResult Home(string sendMailTo)
        {
            int userAdminId = System.Convert.ToInt32(sendMailTo);
            UserAdmin userAdmin = db.UserAdmins.Find(userAdminId);
            Git_system.Helper.EmailMethod.SendEmailForResetPasswordBackend(userAdmin);

            return RedirectToAction("Home");
        }

        //
        // GET: /Backend_Useradmin/Create
        [BackendPermission(isAdmin: true)]
        public ActionResult Create()
        {
            ViewBag.Permission_permissionId = new SelectList(db.Permissions, "permissionId", "Name");
            return View();
        }

        //
        // POST: /Backend_Useradmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isAdmin: true)]
        public ActionResult Create(Backend_Useradmin useradmin)
        {
            if (ModelState.IsValid)
            {
                int count_error = 0;
                if (useradmin.Password == "" || useradmin.Password == null)
                {
                    ModelState.AddModelError("Password", "กรุณากรอกรหัสผ่าน");
                    count_error++;
                }
                if (((from c in db.UserAdmins where (c.userName).ToUpper().Equals((useradmin.userName).ToUpper()) select c).ToList()).Count() >= 1)
                {
                    ModelState.AddModelError("userName", "ชื่อผู้ใช้นี่มีอยู่ในระบบแล้ว");
                    count_error++;
                }
                if (((from c in db.UserAdmins where c.Email.Equals(useradmin.Email) select c).ToList()).Count() >= 1)
                {
                    ModelState.AddModelError("Email", "อีเมลนี้มีอยู่ในระบบแล้ว");
                    count_error++;
                }
                if (count_error == 0)
                {
                    String passHash = (Cryptography.SHA512SaltAddSalt(useradmin.Password, useradmin.userName)).ToString();
                    useradmin.Password = passHash;

                    UserAdmin useradmin_save = new UserAdmin();
                    useradmin_save = useradmin.ToUserAdmin();

                    db.UserAdmins.Add(useradmin_save);

                    db.SaveChanges();
                    LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มผู้ดูแลระบบ", 1);//add to logfile

                    return RedirectToAction("Home");
                }
            }

            ViewBag.Permission_permissionId = new SelectList(db.Permissions, "permissionId", "Name", useradmin.Permission_permissionId);
            return View(useradmin);
        }

        //
        // GET: /Backend_Useradmin/Edit/5
        [BackendPermission(isAdmin: true)]
        public ActionResult Edit(int id = 0)
        {
            UserAdmin useradmin = db.UserAdmins.Find(id);
            Backend_Useradmin useradminfrom = new Backend_Useradmin();
            useradminfrom = useradmin.ToBackend_Useradmin();

            useradminfrom.Password = "";
            useradminfrom.Confirm_Password = "";

            if (useradmin == null)
            {
                return HttpNotFound();
            }

            ViewBag.Permission_permissionId = new SelectList(db.Permissions, "permissionId", "Name", useradmin.Permission_permissionId);
            return View(useradminfrom);
        }

        //
        // POST: /Backend_Useradmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isAdmin: true)]
        public ActionResult Edit(Backend_Useradmin useradmin)
        {
            if (ModelState.IsValid)
            {
                if (useradmin.Password == "" || useradmin.Password == null)
                {
                    UserAdmin OriginalValues = db.UserAdmins.Find(useradmin.Id);
                    useradmin.LastSignIn = OriginalValues.LastSignIn;
                    useradmin.Password = OriginalValues.Password;
                    db.Entry(OriginalValues).CurrentValues.SetValues(useradmin.ToUserAdmin());
                    db.SaveChanges();
                }
                else
                {
                    string passHash = Cryptography.SHA512SaltAddSalt(useradmin.Password, useradmin.userName);
                    useradmin.Password = passHash;

                    UserAdmin OriginalValues = db.UserAdmins.Find(useradmin.Id);
                    useradmin.LastSignIn = OriginalValues.LastSignIn;
                    db.Entry(OriginalValues).CurrentValues.SetValues(useradmin.ToUserAdmin());
                    db.SaveChanges();
                }

                LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขผู้ดูแลระบบ UseradminId = " + useradmin.Id, 1);//add to logfile
                //return RedirectToAction("Home");
            }
            ViewBag.Permission_permissionId = new SelectList(db.Permissions, "permissionId", "Name", useradmin.Permission_permissionId);
            return View(useradmin);
        }

        public ActionResult AdminProfile(int id = 0)
        {
            if (CurrentUseradminId != id)
                if (!CheckPermission(isAdmin: true))
                    return RedirectToAction("Permission", "Backend");

            UserAdmin useradmin = db.UserAdmins.Find(id);
            Backend_Useradmin useradminfrom = new Backend_Useradmin();
            useradminfrom = useradmin.ToBackend_Useradmin();

            useradminfrom.Password = "";
            useradminfrom.Confirm_Password = "";

            if (useradmin == null)
            {
                return HttpNotFound();
            }

            ViewBag.Permission_permissionId = new SelectList(db.Permissions, "permissionId", "Name", useradmin.Permission_permissionId);
            return View(useradminfrom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminProfile(Backend_Useradmin useradmin)
        {
            if (CurrentUseradminId != useradmin.Id)
                if (!CheckPermission(isAdmin: true))
                    return RedirectToAction("Permission", "Backend");

            if (ModelState.IsValid)
            {
                if (useradmin.Password == "" || useradmin.Password == null)
                {
                    UserAdmin OriginalValues = db.UserAdmins.Find(useradmin.Id);
                    useradmin.LastSignIn = OriginalValues.LastSignIn;
                    useradmin.Password = OriginalValues.Password;
                    db.Entry(OriginalValues).CurrentValues.SetValues(useradmin.ToUserAdmin());
                    db.SaveChanges();
                }
                else
                {
                    string passHash = Cryptography.SHA512SaltAddSalt(useradmin.Password, useradmin.userName);
                    useradmin.Password = passHash;

                    UserAdmin OriginalValues = db.UserAdmins.Find(useradmin.Id);
                    useradmin.LastSignIn = OriginalValues.LastSignIn;
                    db.Entry(OriginalValues).CurrentValues.SetValues(useradmin.ToUserAdmin());
                    db.SaveChanges();
                }

                LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขผู้ดูแลระบบ UseradminId = " + useradmin.Id, 1);//add to logfile
                //return RedirectToAction("Home");
            }
            ViewBag.Permission_permissionId = new SelectList(db.Permissions, "permissionId", "Name", useradmin.Permission_permissionId);
            return View(useradmin);
        }
    }
}