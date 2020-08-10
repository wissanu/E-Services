using Git_system.Models;
using Git_system.Models.Form;
using System;
using System.Data.Entity;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_ProductCourseController : BackendController
    {
        public ActionResult Create(int id = 0)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            Course course = new Course();
            course.ProductId = id;

            course.DateStart = DateTime.Now;
            course.DateEnd = DateTime.Now;
            Backend_Course Backend_Course = new Backend_Course(course);
            return View(Backend_Course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Backend_Course backend_Course)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            if (ModelState.IsValid)
            {
                db.Courses.Add(backend_Course.ToCourse());
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มคอร์สอบรม", 1);//add to logfile
                return RedirectToAction("Edit", "Backend_Product", new { id = backend_Course.ProductId });
            }
            return View(backend_Course);
        }

        //
        //
        //Edit
        public ActionResult Edit(int id = 0)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            Git_system.Models.Form.Backend_Course course = new Backend_Course(db.Courses.Find(id));
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Backend_Course backend_Course)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            if (ModelState.IsValid)
            {
                db.Entry(backend_Course.ToCourse()).State = EntityState.Modified;
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขคอร์สอบรม CourseId = " + backend_Course.Id, 1);//add to logfile
                //return RedirectToAction("Edit", "Backend_Product", new { id = backend_Course.ProductId });
            }
            return View(backend_Course);
        }
    }
}