using Git_system.Models;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_ProductSKUController : BackendController
    {
        private System.Collections.Generic.List<SelectListItem> list = new System.Collections.Generic.List<SelectListItem>{
            new SelectListItem {Value = "1",Text = "ส่วนลดสมาชิก"},
            new SelectListItem {Value = "2",Text = "จำนวนคนทั้งหมด"},
            new SelectListItem {Value = "3",Text = "ตามหลักสูตร"},
            new SelectListItem {Value = "4",Text = "จำนวนคนตามรุ่นการอบรม"},
            new SelectListItem {Value = "5",Text = "จำนวนหลักสูตร"}
            };

        //
        // GET: /Backend_ProductSKU/
        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);

            var productSKUs = db.ProductSKUs;
            return View(productSKUs.ToList());
        }

        public ActionResult Create()
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            ViewBag.Type = new SelectList(list, "Value", "Text");

            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.Product.Group == 1), "Id", "TitleTH");

            ProductSKU productSKU = new ProductSKU();
            return View(productSKU);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductSKU productSKU)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            if (ModelState.IsValid)
            {
                if (productSKU.Type == 1)
                {
                    productSKU.Operator = null;
                    productSKU.CourseId = null;
                }
                else if (productSKU.Type == 2)
                    productSKU.CourseId = null;
                else if (productSKU.Type == 3)
                    productSKU.Operator = null;

                db.ProductSKUs.Add(productSKU);
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มโปรโมชัน", 1);//add to logfile
                return RedirectToAction("Home");
            }

            ViewBag.Type = new SelectList(list, "Value", "Text");

            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.Product.Group == 1), "Id", "TitleTH");
            return View();
        }

        public ActionResult Detail(int id = 0)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            ProductSKU productSKU = db.ProductSKUs.Find(id);

            ViewBag.Type = new SelectList(list, "Value", "Text", productSKU.Type);
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.Product.Group == 1), "Id", "TitleTH", productSKU.CourseId);
            return View(productSKU);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(ProductSKU productSKU)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            if (ModelState.IsValid)
            {
                if (productSKU.Type == 1)
                {
                    productSKU.Operator = null;
                    productSKU.CourseId = null;
                }
                else if (productSKU.Type == 2)
                    productSKU.CourseId = null;
                else if (productSKU.Type == 3)
                    productSKU.Operator = null;

                db.Entry(productSKU).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขโปรโมชั่น ProductSKUId = " + productSKU.Id, 1);//add to logfile
                //return RedirectToAction("Home");
            }
            ViewBag.Type = new SelectList(list, "Value", "Text", productSKU.Type);
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.Product.Group == 1), "Id", "TitleTH", productSKU.CourseId);
            return View(productSKU);
        }
    }
}