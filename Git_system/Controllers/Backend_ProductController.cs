using Git_system.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_ProductController : BackendController
    {
        //
        // GET: /Backend_Product/

        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            Git_system.App_Code.CourseManageDatabase.updateStatusCourse();
            return View(db.Products.Where(p => p.Group != 0).ToList());
        }

        //
        //Create
        public ActionResult Create()
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มหลักสูตร", 1);//add to logfile
                return RedirectToAction("Home");
            }

            return View(product);
        }

        //
        //Edit
        public ActionResult Edit(int id = 0)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขหลักสูตร Id = " + product.Id, 1);//add to logfile
                //return RedirectToAction("Home");
            }
            product.Courses = db.Courses.Where(c => c.ProductId.Equals(product.Id)).ToList();
            return View(product);
        }

        public ActionResult DetailCourse(int courseId, int status)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);

            Course course = db.Courses.Find(courseId);
            if (status != 1)
            {
                System.Collections.Generic.List<PayItem> payitems = new System.Collections.Generic.List<PayItem>();
                foreach (PayItem payitem in course.PayItems)
                {
                    if (payitem.Pay.ProcessStatusTypeId == status)
                        payitems.Add(payitem);
                }
                course.PayItems = payitems;
            }

            return View(course);
        }

        public ActionResult DetailUser(int id)
        {
            CheckPermissionWihtRedirect(false, false, true, false, false);
            var paysAll = db.Pays.Where(p => p.MembershipId.Equals(id)).OrderByDescending(p => p.Id);
            var pays = new System.Collections.Generic.List<Pay>();
            foreach (var pay in paysAll)
            {
                if (pay.PayItems.FirstOrDefault().Course.Product.Group != 0)
                {
                    pays.Add(pay);
                }
            }
            return View(pays.ToList());
        }

        public ActionResult News()
        {
            return View(db.ProductNews.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        public ActionResult News(ProductNews[] productNews)
        {
            foreach (ProductNews productNew in productNews)
            {
                productNew.DetailTH = productNew.DetailTH == null ? "" : productNew.DetailTH;
                productNew.DetailEN = productNew.DetailEN == null ? "" : productNew.DetailEN;
                db.Entry(productNew).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("News");
        }
    }
}