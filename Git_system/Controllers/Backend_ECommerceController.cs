using Git_system.Models;
using Git_system.Models.ECom;
using Git_system.MultiResources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    #region EComHelper

    /// <summary>
    /// Class หาชื่อตัวแปร
    /// http://stackoverflow.com/questions/9801624/get-name-of-a-variable-or-parameter
    /// </summary>
    public static class MemberInfoGetting
    {
        /// <summary>
        /// รับค่าตัวแปร และ คืนค่าชื่อของตัวแประ
        /// ตัวอย่าง
        /// MemberInfoGetting.GetMemberName(() => ImageFiles)
        /// </summary>
        /// <param name="memberExpression">ตัวแปรที่ต้องการทราบชื่อ</param>
        /// <returns>ชื่อของตัวแปร</returns>
        public static string GetMemberName<T>(System.Linq.Expressions.Expression<Func<T>> memberExpression)
        {
            System.Linq.Expressions.MemberExpression expressionBody = (System.Linq.Expressions.MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }

    /// <summary>
    /// Class ตัวทดสอบ Models
    /// </summary>
    public class CheckModels
    {
        public CheckModels()
        {
        }

        /// <summary>
        /// นำค่าไปเก็บใน Class
        /// </summary>
        /// <param name="data">Model</param>
        /// <param name="key">ชื่อของ Model</param>
        /// <param name="errorMessage">ข้อความเมื่อค่าเว่าง</param>
        public CheckModels(object data, string key, string errorMessage)
        {
            this.data = data;
            this.errorMessage = errorMessage;
            this.key = key;
            this.type = 1;
        }

        public object data { get; set; }

        public string errorMessage { get; set; }

        public string key { get; set; }

        public int type { get; set; }
    }

    public static partial class Extends
    {
        public static Boolean CheckModelsIsValid(this ModelStateDictionary modelState, List<CheckModels> checkModels)
        {
            int countErr = 0;
            foreach (CheckModels checkModel in checkModels)
            {
                if (checkModel.type == 1)
                {
                    if (checkModel.data != null)
                        if (checkModel.data.GetType() == typeof(string))
                            if (checkModel.data == "")
                                checkModel.data = null;

                    if (checkModel.data == null)
                    {
                        modelState.AddModelError(checkModel.key, checkModel.errorMessage);
                        System.Diagnostics.Debug.WriteLine("Model : " + checkModel.key + " is null!");//Print debug to output.
                        countErr++;
                    }
                }
            }
            return countErr == 0;
        }
    }

    #endregion EComHelper

    [Backend_AuthorizeAttrinbite]
    public class Backend_ECommerceController : BackendController
    {
        /// <summary>
        /// ทำการตรวจสอบค่า Models ว่าเป็นค่าว่างหรือไม่
        /// </summary>
        /// <param name="checkModels">ตัวทดสอบ</param>
        /// <returns>ค่าที่ตรวจสอบว่ามี Models ว่าหรือไม่</returns>
        private Boolean CheckModelsIsValid(List<CheckModels> checkModels)
        {
            int countErr = 0;
            foreach (CheckModels checkModel in checkModels)
            {
                if (checkModel.type == 1)
                {
                    if (checkModel.data != null)
                        if (checkModel.data.GetType() == typeof(string))
                            if (checkModel.data == "")
                                checkModel.data = null;

                    if (checkModel.data == null)
                    {
                        ModelState.AddModelError(checkModel.key, checkModel.errorMessage);
                        System.Diagnostics.Debug.WriteLine("Model : " + checkModel.key + " is null!");//Print debug to output.
                        countErr++;
                    }
                }
            }
            return countErr == 0;
        }

        //
        // GET: /Backend_E-Commerce/
        public ActionResult Home()
        {
            return View();
        }

        #region Product

        //
        //Product
        [BackendPermission(isEProduct: true)]
        public ActionResult Product()
        {
            return View(db.EComProducts.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEProduct: true)]
        public ActionResult Product(EComProduct eComProduct)
        {
            EComProduct originalEComProduct = db.EComProducts.Find(eComProduct.Id);
            EComProduct eComProductSave = originalEComProduct;
            eComProductSave.PinStatus = eComProduct.PinStatus;
            eComProductSave.PinWeight = eComProduct.PinWeight;
            db.Entry(originalEComProduct).CurrentValues.SetValues(eComProductSave);
            db.SaveChanges();
            return View(db.EComProducts.ToList());
        }

        private IEnumerable<dynamic> getListDeliverType()
        {
            var mp = from d in db.DeliverTypes
                     join tm in db.TranslateMaps
                     on d.NameTranslateId equals tm.Id
                     where d.Id != 1 && d.Id != 4 // Disable where id = 1 and 7
                     select new
                     {
                         d.Id,
                         tm.Translates.Where(l => l.Language_code.Equals("th")).FirstOrDefault().Text
                     };

            //var mp = from d in db.DeliverTypes
            //         join tm in db.TranslateMaps
            //         on d.NameTranslateId equals tm.Id
            //         join t in db.Translates
            //         on tm.Id equals t.TranslateMapId
            //         where t.Language_code == "th"
            //         select new
            //         {
            //             d.Id,
            //             t.Text
            //         };

            //var getTranslateText = new Func<TranslateMap, String, String>((tm, language_code) => tm.Translates.Where(t => t.Language_code.Equals(language_code)).FirstOrDefault().Text);

            //var mp = from d in db.DeliverTypes
            //          select new
            //          {
            //              d.Id,
            //              Text = getTranslateText(d.NameTranslateMap, "th")
            //          };

            return mp.ToList();
        }

        private IEnumerable<dynamic> getListVatType()
        {
            var mp = from d in db.VatTypes
                     join tm in db.TranslateMaps
                     on d.NameTranslate equals tm.Id
                     select new
                     {
                         d.Id,
                         tm.Translates.Where(l => l.Language_code.Equals("th")).FirstOrDefault().Text
                     };
            return mp.ToList();
        }

        [BackendPermission(isEProduct: true)]
        public ActionResult Product_Create()
        {
            if (db.EComCategories.Count() == 0)
                return RedirectToAction("ProductCategory");

            EComProduct eComProduct = new EComProduct();
            eComProduct.EComProductDeliver = new EComProductDeliver();
            ViewBag.EComCategoryMapsValue = new SelectList(db.EComCategories, "Id", "NameTH");
            ViewBag.EComProductDeliver_DeliverTypeId = new SelectList(getListDeliverType(), "Id", "Text", eComProduct.EComProductDeliver.DeliverTypeId);
            ViewBag.EComProduct_VatTypeId = new SelectList(getListVatType(), "Id", "Text", eComProduct.VatTypeId);
            return View(eComProduct);
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        [BackendPermission(isEProduct: true)]
        public ActionResult Product_Create(EComProduct eComProduct, HttpPostedFileBase[] ImageFiles, HttpPostedFileBase[] OtherFiles, HttpPostedFileBase[] ElectronicFiles, HttpPostedFileBase[] DemoElectronicFiles, int[] EComCategoryMapsValue, string Command, string fileId, int EComProductDeliver_DeliverTypeId = 0, int EComProduct_VatTypeId = 2)
        {
            /// บันทึกสินค้าใหม่
            if (Command == null)
            {
                if (ModelState.IsValid)
                {
                    List<CheckModels> checkModels = new List<CheckModels>();
                    checkModels.Add(new CheckModels(EComCategoryMapsValue, MemberInfoGetting.GetMemberName(() => EComCategoryMapsValue), "ไม่ได้เลือกประเภทสินค้า"));
                    checkModels.Add(new CheckModels(ImageFiles, MemberInfoGetting.GetMemberName(() => ImageFiles), "ไม่ได้เลือกรูปภาพ"));
                    //checkModels.Add(new CheckModels(OtherFiles, MemberInfoGetting.GetMemberName(() => OtherFiles), "ไม่ได้เลือกไฟล์ประกอบ"));
                    checkModels.Add(new CheckModels(eComProduct.NameTH, MemberInfoGetting.GetMemberName(() => eComProduct.NameTH), "กรุณากรอกชือสินค้า"));
                    checkModels.Add(new CheckModels(eComProduct.NameEN, MemberInfoGetting.GetMemberName(() => eComProduct.NameEN), "กรุณากรอกชื่อสินค้า"));

                    if (CheckModelsIsValid(checkModels))
                    {
                        eComProduct.VatTypeId = EComProduct_VatTypeId;

                        /// เพิ่มรูปเข้าไปในเครื่องและบันทึกลง fileLinkImages
                        eComProduct.ImageFiles = ImageFiles.UploadAndSave("~/E-Commerce/Images/").ToJson();

                        /// เพิ่มไฟล์ประกอบเข้าไปในเครื่องและบันทึกลง fileLinkOtherFiles
                        eComProduct.OtherFiles = OtherFiles.UploadAndSave("~/E-Commerce/Other/").ToJson();

                        /// เพิ่มตัวอย่างไฟล์อิเล็กทรอนิคเข้าไปในเครื่องและบันทึกลง fileLinkDemoElectronicFiles
                        eComProduct.DemoElectronicFiles = DemoElectronicFiles.UploadAndSave("~/E-Commerce/Other/").ToJson();

                        /// เพิ่มไฟล์อิเล็กทรอนิคเข้าไปในเครื่องและบันทึกลง fileLinkElectronicFiles
                        eComProduct.ElectronicFiles = ElectronicFiles.UploadAndSave("~/E-Commerce/Files/").ToJson();

                        eComProduct.EComProductDeliver.DeliverTypeId = EComProductDeliver_DeliverTypeId;
                        db.EComProducts.Add(eComProduct);
                        db.SaveChanges();

                        /// ทำการเพิ่ง CategoryMap ให้กับ Product
                        eComProduct.InsertCategoryMap(EComCategoryMapsValue);

                        Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มสินค้า ProductId = " + eComProduct.Id, 1);//add to logfile

                        return RedirectToAction("Product_Detail", new { @id = eComProduct.Id });
                    }
                }
            }
            else if (Command == "DeleteFile")
            {
            }

            ViewBag.EComCategoryMapsValue = new SelectList(db.EComCategories, "Id", "NameTH");
            ViewBag.EComProductDeliver_DeliverTypeId = new SelectList(getListDeliverType(), "Id", "Text", eComProduct.EComProductDeliver.DeliverTypeId);
            ViewBag.EComProduct_VatTypeId = new SelectList(getListVatType(), "Id", "Text", eComProduct.VatTypeId);
            return View(eComProduct);
        }

        [BackendPermission(isEProduct: true)]
        public ActionResult Product_Detail(int id = 0)
        {
            EComProduct eComProduct = db.EComProducts.Find(id);
            MultiSelectList multiSelectList = new MultiSelectList(db.EComCategories, "Id", "NameTH", eComProduct.EComCategoryMaps.Select(c => c.EComCategoryId).ToArray());
            if (eComProduct.EComProductDeliver == null)
                eComProduct.EComProductDeliver = new EComProductDeliver();
            ViewBag.EComCategoryMapsValue = multiSelectList;
            ViewBag.EComProductDeliver_DeliverTypeId = new SelectList(getListDeliverType(), "Id", "Text", eComProduct.EComProductDeliver.DeliverTypeId);
            ViewBag.EComProduct_VatTypeId = new SelectList(getListVatType(), "Id", "Text", eComProduct.VatTypeId);
            return View(eComProduct);
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        [BackendPermission(isEProduct: true)]
        public ActionResult Product_Detail(int id, EComProduct eComProduct, HttpPostedFileBase[] ImageFiles, HttpPostedFileBase[] OtherFiles, HttpPostedFileBase[] ElectronicFiles, HttpPostedFileBase[] DemoElectronicFiles, int[] EComCategoryMapsValue, string Command, string fileId, int EComProductDeliver_DeliverTypeId = 0, int EComProduct_VatTypeId = 2)
        {
            /// แก้ไขสินค้า
            if (Command == null)
            {
                if (ModelState.IsValid)
                {
                    List<CheckModels> checkModels = new List<CheckModels>();
                    checkModels.Add(new CheckModels(EComCategoryMapsValue, MemberInfoGetting.GetMemberName(() => EComCategoryMapsValue), "ไม่ได้เลือกประเภทสินค้า"));
                    checkModels.Add(new CheckModels(ImageFiles, MemberInfoGetting.GetMemberName(() => ImageFiles), "ไม่ได้เลือกรูปภาพ"));
                    //checkModels.Add(new CheckModels(OtherFiles, MemberInfoGetting.GetMemberName(() => OtherFiles), "ไม่ได้เลือกไฟล์ประกอบ"));
                    checkModels.Add(new CheckModels(eComProduct.NameTH, MemberInfoGetting.GetMemberName(() => eComProduct.NameTH), "กรุณากรอกชือสินค้า"));
                    checkModels.Add(new CheckModels(eComProduct.NameEN, MemberInfoGetting.GetMemberName(() => eComProduct.NameEN), "กรุณากรอกชื่อสินค้า"));

                    if (CheckModelsIsValid(checkModels))
                    {
                        EComProduct originalEComProduct = db.EComProducts.Find(id);

                        /// เพิ่มรูปเข้าไปในเครื่องและบันทึกลง fileLinkImages
                        eComProduct.ImageFiles = originalEComProduct.ImageFiles.ToListFileLink().Add(ImageFiles.UploadAndSave("~/E-Commerce/Images/")).ToJson();

                        if (OtherFiles.FirstOrDefault() != null)
                        {
                            /// ลบไฟล์ทั้งหมดใน fileLinkOtherFiles
                            originalEComProduct.OtherFiles.ToListFileLink().ForEach(o => o.removeFileLink());
                            /// เพิ่มไฟล์ประกอบเข้าไปในเครื่องและบันทึกลง fileLinkOtherFiles
                            eComProduct.OtherFiles = OtherFiles.UploadAndSave("~/E-Commerce/Other/").ToJson();
                        }
                        else
                            eComProduct.OtherFiles = originalEComProduct.OtherFiles;

                        if (DemoElectronicFiles.FirstOrDefault() != null)
                        {
                            /// ลบไฟล์ทั้งหมดใน fileLinkDemoElectronicFiles
                            originalEComProduct.DemoElectronicFiles.ToListFileLink().ForEach(o => o.removeFileLink());
                            /// เพิ่มตัวอย่างไฟล์อิเล็กทรอนิคเข้าไปในเครื่องและบันทึกลง fileLinkElectronicFiles
                            eComProduct.DemoElectronicFiles = DemoElectronicFiles.UploadAndSave("~/E-Commerce/Other/").ToJson();
                        }
                        else
                            eComProduct.DemoElectronicFiles = originalEComProduct.DemoElectronicFiles;

                        if (ElectronicFiles.FirstOrDefault() != null)
                        {
                            /// ลบไฟล์ทั้งหมดใน fileLinkElectronicFiles
                            originalEComProduct.ElectronicFiles.ToListFileLink().ForEach(o => o.removeFileLink());
                            /// เพิ่มไฟล์อิเล็กทรอนิคเข้าไปในเครื่องและบันทึกลง fileLinkElectronicFiles
                            eComProduct.ElectronicFiles = ElectronicFiles.UploadAndSave("~/E-Commerce/Files/").ToJson();
                        }
                        else
                            eComProduct.ElectronicFiles = originalEComProduct.ElectronicFiles;

                        eComProduct.EComProductDeliver.EComProduct = originalEComProduct;
                        eComProduct.EComProductDeliver.DeliverTypeId = EComProductDeliver_DeliverTypeId;
                        eComProduct.VatTypeId = EComProduct_VatTypeId;
                        if (originalEComProduct.EComProductDeliver == null)
                            db.EComProductDelivers.Add(eComProduct.EComProductDeliver);
                        else
                            db.Entry(db.EComProductDelivers.Find(eComProduct.EComProductDeliver.Id)).CurrentValues.SetValues(eComProduct.EComProductDeliver);

                        db.Entry(originalEComProduct).CurrentValues.SetValues(eComProduct);
                        db.SaveChanges();

                        /// แก้ไข CategoryMap ของ Product
                        int[] arrayEComCategoryMaps = originalEComProduct.EComCategoryMaps.Select(c => c.EComCategoryId).ToArray();
                        bool eComCategoryMapsEqual = Enumerable.SequenceEqual((arrayEComCategoryMaps), (EComCategoryMapsValue));
                        if (!eComCategoryMapsEqual)
                        {
                            originalEComProduct.UpdateCategoryMap(EComCategoryMapsValue);
                        }
                        eComProduct.EComCategoryMaps = db.EComProducts.Find(id).EComCategoryMaps;

                        Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสินค้า ProductId = " + eComProduct.Id, 1);//add to logfile

                        return RedirectToAction("Product_Detail", new { id = eComProduct.Id });
                    }
                }
            }
            else if (Command == "DeleteFile")
            {
                EComProduct originalEComProduct = db.EComProducts.Find(id);
                eComProduct = originalEComProduct;

                List<FileLink> imageFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(eComProduct.ImageFiles);
                List<FileLink> otherFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(eComProduct.OtherFiles);
                List<FileLink> electronicFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(eComProduct.ElectronicFiles);

                imageFiles.RemoveAndSave(imageFiles.Where(f => f.FileId.Equals(fileId)).FirstOrDefault());
                otherFiles.RemoveAndSave(otherFiles.Where(f => f.FileId.Equals(fileId)).FirstOrDefault());
                electronicFiles.RemoveAndSave(electronicFiles.Where(f => f.FileId.Equals(fileId)).FirstOrDefault());

                eComProduct.ImageFiles = Newtonsoft.Json.JsonConvert.SerializeObject(imageFiles);
                eComProduct.OtherFiles = Newtonsoft.Json.JsonConvert.SerializeObject(otherFiles);
                eComProduct.ElectronicFiles = Newtonsoft.Json.JsonConvert.SerializeObject(electronicFiles);

                db.Entry(originalEComProduct).CurrentValues.SetValues(eComProduct);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "ลบรูปภาพของ ProductId = " + eComProduct.Id, 1);//add to logfile

                return RedirectToAction("Product_Detail", new { id = eComProduct.Id });
            }

            MultiSelectList multiSelectList = new MultiSelectList(db.EComCategories, "Id", "NameTH", EComCategoryMapsValue);
            ViewBag.EComCategoryMapsValue = multiSelectList;
            ViewBag.EComProductDeliver_DeliverTypeId = new SelectList(getListDeliverType(), "Id", "Text", eComProduct.EComProductDeliver.DeliverTypeId);
            ViewBag.EComProduct_VatTypeId = new SelectList(getListVatType(), "Id", "Text", eComProduct.VatTypeId);
            return View(eComProduct);
        }

        //
        //ProductCategory
        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory()
        {
            return View(db.EComCategories.ToList().OrderBy(c => c.OrderBy));
        }

        [HttpPost]
        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory(EComCategory eComCategory, string Command, int? category_id, double? category_price, double? category_priceinter)
        {
            if (Command == "sortUp")
            {
                EComCategory eComCategoryOriginal = db.EComCategories.Find(eComCategory.Id);
                int currentOrder = Convert.ToInt32(eComCategoryOriginal.OrderBy);
                if (currentOrder != 1)
                {
                    // Set other category
                    EComCategory eComCategory_OrderHight = db.EComCategories.Where(c => c.OrderBy == currentOrder - 1).FirstOrDefault();
                    if (eComCategory_OrderHight != null)
                    {
                        EComCategory eComCategory_OrderHight_ReOrder = eComCategory_OrderHight;
                        eComCategory_OrderHight_ReOrder.OrderBy = currentOrder;
                        db.Entry(eComCategory_OrderHight).CurrentValues.SetValues(eComCategory_OrderHight_ReOrder);
                        db.SaveChanges();
                    }

                    // Set current category
                    eComCategory = eComCategoryOriginal;
                    eComCategory.OrderBy = currentOrder - 1;
                    db.Entry(eComCategoryOriginal).CurrentValues.SetValues(eComCategory);
                    db.SaveChanges();

                    Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เรียงลำดับประเภทสินค้า", 1);//add to logfile
                }
            }
            else if (Command == "sortDown")
            {
                List<EComCategory> eComCategories = db.EComCategories.ToList();
                foreach (EComCategory item in eComCategories)
                {
                    item.OrderBy = Convert.ToInt32(item.OrderBy == null ? 0 : item.OrderBy);
                }
                int max = eComCategories.Count == 0 ? 0 : eComCategories.Select(p => Convert.ToInt32(p.OrderBy)).Max();

                EComCategory eComCategoryOriginal = db.EComCategories.Find(eComCategory.Id);
                int currentOrder = Convert.ToInt32(eComCategoryOriginal.OrderBy);
                if (currentOrder != max)
                {
                    // Set other category
                    EComCategory eComCategory_OrderLow = db.EComCategories.Where(c => c.OrderBy == currentOrder + 1).FirstOrDefault();
                    if (eComCategory_OrderLow != null)
                    {
                        EComCategory eComCategory_OrderHight_ReOrder = eComCategory_OrderLow;
                        eComCategory_OrderHight_ReOrder.OrderBy = currentOrder;
                        db.Entry(eComCategory_OrderLow).CurrentValues.SetValues(eComCategory_OrderHight_ReOrder);
                        db.SaveChanges();
                    }

                    // Set current category
                    eComCategory = eComCategoryOriginal;
                    eComCategory.OrderBy = currentOrder + 1;
                    db.Entry(eComCategoryOriginal).CurrentValues.SetValues(eComCategory);
                    db.SaveChanges();

                    Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เรียงลำดับประเภทสินค้า", 1);//add to logfile
                }
            }
            else if (Command == "setPrice")
            {
                List<EComProduct> lp = db.EComCategoryMaps.Where(c => c.EComCategoryId == category_id.GetValueOrDefault(0)).Select(c => c.EComProduct).ToList();
                foreach (var p in lp)
                {
                    p.EComProductDeliver.Price = category_price.GetValueOrDefault(0);
                    p.EComProductDeliver.PriceInter = category_priceinter.GetValueOrDefault(0);
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return View(db.EComCategories.ToList().OrderBy(c => c.OrderBy));
        }

        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory_Create()
        {
            return View(new EComCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory_Create(EComCategory eComCategory)
        {
            if (ModelState.IsValid)
            {
                List<EComCategory> eComCategories = db.EComCategories.ToList();
                foreach (EComCategory item in eComCategories)
                {
                    item.OrderBy = Convert.ToInt32(item.OrderBy == null ? 0 : item.OrderBy);
                }
                int max = eComCategories.Count == 0 ? 0 : eComCategories.Select(p => Convert.ToInt32(p.OrderBy)).Max();
                eComCategory.OrderBy = max + 1;
                db.EComCategories.Add(eComCategory);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มประเภทสินค้า CategoryId = " + eComCategory.Id, 1);//add to logfile

                return RedirectToAction("ProductCategory");
            }
            return View(eComCategory);
        }

        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory_Detail(int id = 0)
        {
            return View(db.EComCategories.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory_Detail(EComCategory eComCategory)
        {
            if (ModelState.IsValid)
            {
                EComCategory originalEComCategory = db.EComCategories.Find(eComCategory.Id);
                eComCategory.OrderBy = originalEComCategory.OrderBy;
                db.Entry(originalEComCategory).CurrentValues.SetValues(eComCategory);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขประเภทสินค้า CategoryId = " + eComCategory.Id, 1);//add to logfile
            }
            return View(eComCategory);
        }

        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory_With_Product(int id = 0)
        {
            return View(db.EComCategoryMaps.Where(c => c.EComCategoryId.Equals(id)).OrderBy(c => c.OrderBy).ToList());
        }

        [HttpPost]
        [BackendPermission(isEProduct: true)]
        public ActionResult ProductCategory_With_Product(EComCategoryMap eComCategoryMap, string Command)
        {
            if (Command == "sortUp")
            {
                eComCategoryMap.SortChange(-1);
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เรียงลำดับสินค้า", 1);//add to logfile
            }
            else if (Command == "sortDown")
            {
                eComCategoryMap.SortChange(1);
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เรียงลำดับสินค้า", 1);//add to logfile
            }
            eComCategoryMap = db.EComCategoryMaps.Find(eComCategoryMap.Id);
            return View(db.EComCategoryMaps.Where(c => c.EComCategoryId.Equals(eComCategoryMap.EComCategoryId)).OrderBy(c => c.OrderBy).ToList());
        }

        [BackendPermission(isEStock: true)]
        public ActionResult ProductStock()
        {
            return View(db.EComProducts.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEStock: true)]
        public ActionResult ProductStock(EComProduct eComProduct, int stockParameter = 0, int stockQuantity = 0)
        {
            if (stockQuantity > 0)
            {
                EComProduct originalEComProduct = db.EComProducts.Find(eComProduct.Id);
                eComProduct = originalEComProduct;
                if (stockParameter == 1)
                {
                    eComProduct.Quantity += stockQuantity;
                    Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มสินค้าในคลังให้กับ ProductId = " + eComProduct.Id + " จำนวน " + stockQuantity, 1);//add to logfile
                }
                else if (stockParameter == 2)
                {
                    eComProduct.Quantity -= stockQuantity;
                    if (eComProduct.Quantity < 0)
                        eComProduct.Quantity = 0;
                    Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "ลดสินค้าในคลังให้กับ ProductId = " + eComProduct.Id + " จำนวน " + stockQuantity, 1);//add to logfile
                }
                db.Entry(originalEComProduct).CurrentValues.SetValues(eComProduct);
                db.SaveChanges();
            }
            return View(db.EComProducts.ToList());
        }

        #endregion Product

        #region Order

        //
        //Order
        [BackendPermission(isEPayView: true)]
        public ActionResult Order()
        {
            return View(db.EComOrders.OrderByDescending(o => o.Datetime).ToList());
        }

        [BackendPermission(isEPayView: true)]
        public ActionResult Order_Detail(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Order");
            return View(db.EComOrders.Find(id));
        }

        #endregion Order

        #region Payment

        //
        //Payment
        [BackendPermission(isEPayConfirm: true)]
        public ActionResult Payment(int id = 0)
        {
            if (id != 0)
                return View(db.EComConfirmOrders.Where(c => c.EComOrderId.Equals(id)).OrderByDescending(o => o.Datetime).ToList());
            else
                return View(db.EComConfirmOrders.OrderByDescending(o => o.Datetime).ToList());
        }

        [BackendPermission(isEPayConfirm: true)]
        public ActionResult Payment_Detail(int id = 0)
        {
            EComConfirmOrder eComConfirmOrder = db.EComConfirmOrders.Find(id);
            var selectList = new SelectList(db.PaymentProcessStatus, "Id", "NameTH", eComConfirmOrder.EComOrder.PaymentProcessStatusId);
            ViewBag.EComOrder_PaymentProcessStatusId = selectList;
            return View(db.EComConfirmOrders.Find(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [BackendPermission(isEPayConfirm: true)]
        public ActionResult Payment_Detail(EComConfirmOrder confirmOrder, int EComOrder_PaymentProcessStatusId)
        {
            EComConfirmOrder confirmOrderOriginal = db.EComConfirmOrders.Find(confirmOrder.Id);
            EComOrder orderOriginal = confirmOrderOriginal.EComOrder;
            var oldPaymentStatus = orderOriginal.PaymentProcessStatusId;
            confirmOrderOriginal.EComOrder.PaymentProcessStatusId = EComOrder_PaymentProcessStatusId;
            orderOriginal.PaymentProcessStatusId = confirmOrderOriginal.EComOrder.PaymentProcessStatusId;
            db.Entry(orderOriginal).State = EntityState.Modified;
            db.SaveChanges();

            if ((oldPaymentStatus == 1 || oldPaymentStatus == 2) && orderOriginal.PaymentProcessStatusId == 3)
            {
                Helper.EmailMethod.sendEmailForEComConfirmOrderSuccess(orderOriginal);
            }

            var paymentProcessStatus = db.PaymentProcessStatus.ToList();
            Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสถานะการชำระเงินของ OrderId = " + confirmOrder.EComOrderId + " เป็น " + paymentProcessStatus.Where(e => e.Id == EComOrder_PaymentProcessStatusId).FirstOrDefault().NameTH, 1);//add to logfile

            var selectList = new SelectList(paymentProcessStatus, "Id", "NameTH", EComOrder_PaymentProcessStatusId);
            ViewBag.EComOrder_PaymentProcessStatusId = selectList;
            return View(confirmOrderOriginal);
        }

        #endregion Payment

        #region Deliver

        //
        //Deliver
        [BackendPermission(isEDeliver: true)]
        public ActionResult Deliver(int id = 0)
        {
            if (id != 0)
            {
                List<EComOrder> eComOrders = db.EComOrders.Where(o => o.PaymentProcessStatusId == 3).Where(c => c.Id.Equals(id)).OrderByDescending(o => o.Datetime).ToList();
                if (PayHelper.checkEletronicOrder(eComOrders.Find(c => c.Id.Equals(id))))
                    return View(new List<EComOrder>());
                else if (eComOrders.Count > 0)
                    return RedirectToAction("Deliver_Detail", new { @id = id });
                return View(eComOrders);
            }
            else
                return View(db.EComOrders.Where(o => o.PaymentProcessStatusId == 3).OrderByDescending(o => o.Datetime).ToList());
        }

        [BackendPermission(isEDeliver: true)]
        public ActionResult Deliver_Detail(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Deliver");

            EComOrder eComOrder = db.EComOrders.Find(id);
            var selectList = new SelectList(db.DeliverProcessStatus, "Id", "NameTH", eComOrder.DeliverProcessStatusId);
            ViewBag.DeliverProcessStatusId = selectList;
            return View(eComOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEDeliver: true)]
        public ActionResult Deliver_Detail(int id, EComOrder eComOrder, string DeliverSendDate_Day, string DeliverSendDate_Time)
        {
            if (id != eComOrder.Id)
                return RedirectToAction("Deliver");

            if (DeliverSendDate_Day != string.Empty && DeliverSendDate_Time != string.Empty)
                eComOrder.DeliverSendDate = System.DateTime.ParseExact(System.String.Format("{0:dd/MM/yyyy}", DeliverSendDate_Day) + " " + System.String.Format("{0:HH:mm}", DeliverSendDate_Time), "dd/MM/yyyy HH:mm", null);

            var defaultDeliverProcessStatusId = 1;
            EComOrder orderOriginal = db.EComOrders.Find(id);
            if (ModelState.IsValid)
            {
                if (eComOrder.DeliverProcessStatusId == 2)
                {
                    List<CheckModels> checkModels = new List<CheckModels>();
                    checkModels.Add(new CheckModels(eComOrder.DeliverNumber, MemberInfoGetting.GetMemberName(() => eComOrder.DeliverNumber), Multi.RequiredField));
                    checkModels.Add(new CheckModels(eComOrder.DeliverSendName, MemberInfoGetting.GetMemberName(() => eComOrder.DeliverSendName), Multi.RequiredField));
                    checkModels.Add(new CheckModels(DeliverSendDate_Day, "DeliverSendDate_Day", Multi.RequiredField));
                    checkModels.Add(new CheckModels(DeliverSendDate_Time, "DeliverSendDate_Time", Multi.RequiredField));

                    if (ModelState.CheckModelsIsValid(checkModels))
                    {
                        defaultDeliverProcessStatusId = orderOriginal.DeliverProcessStatusId;
                        orderOriginal.DeliverNumber = eComOrder.DeliverNumber;
                        orderOriginal.DeliverSendName = eComOrder.DeliverSendName;
                        orderOriginal.DeliverSendDate = eComOrder.DeliverSendDate;
                        var changeToFinishStatus = eComOrder.DeliverProcessStatusId == 2 && defaultDeliverProcessStatusId != 2;
                        if (changeToFinishStatus)
                            foreach (var item in orderOriginal.EComOrderItems.Where(o => o.EComProduct.DeliverStatus == true))
                            {
                                item.EComProduct.Quantity = item.EComProduct.Quantity - item.Quantity;
                                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสถานะการจัดส่งของ OrderId = " + eComOrder.Id + " และทำการ " + "ลดสินค้าในคลังให้กับ ProductId = " + item.EComProduct.Id + " จำนวน " + item.Quantity, 1);//add to logfile
                            }

                        if (changeToFinishStatus)
                        {
                            Git_system.Helper.EmailMethod.sendEmailForEComDeliverSent(order: orderOriginal);
                        }
                    }
                }

                orderOriginal.DeliverProcessStatusId = eComOrder.DeliverProcessStatusId;
                using (var context = new Database_MainEntities1())
                {
                    var cOrder = context.EComOrders.Find(eComOrder.Id);
                    defaultDeliverProcessStatusId = cOrder.DeliverProcessStatusId;
                    cOrder.DeliverNumber = eComOrder.DeliverNumber;
                    cOrder.DeliverSendName = eComOrder.DeliverSendName;
                    cOrder.DeliverSendDate = eComOrder.DeliverSendDate;
                    var changeToFinishStatus = eComOrder.DeliverProcessStatusId == 2 && defaultDeliverProcessStatusId != 2;
                    if (changeToFinishStatus)
                        foreach (var item in cOrder.EComOrderItems.Where(o => o.EComProduct.DeliverStatus == true))
                        {
                            item.EComProduct.Quantity = item.EComProduct.Quantity - item.Quantity;
                        }
                    cOrder.DeliverProcessStatusId = eComOrder.DeliverProcessStatusId;
                    context.Entry(cOrder).State = EntityState.Modified;
                    context.SaveChanges();
                    orderOriginal.DeliverProcessStatu = cOrder.DeliverProcessStatu;
                }

                //db.Entry(orderOriginal).State = EntityState.Modified;
                //db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสถานะการจัดส่งของ OrderId = " + eComOrder.Id + " เป็น " + orderOriginal.DeliverProcessStatu.NameTH, 1);//add to logfile
            }

            var deliverProcessStatus = db.DeliverProcessStatus.ToList();
            var selectList = new SelectList(deliverProcessStatus, "Id", "NameTH", eComOrder.DeliverProcessStatusId);
            ViewBag.DeliverProcessStatusId = selectList;
            return View(orderOriginal);
        }

        //
        //DeliverType
        [BackendPermission(isEDeliver: true)]
        public ActionResult DeliverType()
        {
            return View(db.EComDeliverTypes.ToList());
        }

        [BackendPermission(isEDeliver: true)]
        public ActionResult DeliverType_Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEDeliver: true)]
        public ActionResult DeliverType_Create(EComDeliverType eComDeliverType)
        {
            if (ModelState.IsValid)
            {
                db.EComDeliverTypes.Add(eComDeliverType);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มประเภทการจัดส่ง DeliverTypeId = " + eComDeliverType.Id, 1);//add to logfile

                return RedirectToAction("DeliverType_Detail", new { id = eComDeliverType.Id });
            }

            return View(eComDeliverType);
        }

        [BackendPermission(isEDeliver: true)]
        public ActionResult DeliverType_Detail(int id = 0)
        {
            if (id.Equals(0))
                return RedirectToAction("DeliverType");
            return View(db.EComDeliverTypes.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEDeliver: true)]
        public ActionResult DeliverType_Detail(EComDeliverType eComDeliverType)
        {
            if (ModelState.IsValid)
            {
                //EComDeliverType eComDeliverTypeOriginal = db.EComDeliverTypes.Find(eComDeliverType.Id);
                db.Entry(eComDeliverType).State = EntityState.Modified;
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขประเภทการจัดส่ง DeliverTypeId = " + eComDeliverType.Id, 1);//add to logfile

                return RedirectToAction("DeliverType_Detail", new { id = eComDeliverType.Id });
            }

            return View(eComDeliverType);
        }

        #endregion Deliver

        #region Promotion

        //
        //Promotion
        private System.Collections.Generic.List<SelectListItem> listPromotionName = new System.Collections.Generic.List<SelectListItem>{
            new SelectListItem {Value = "1",Text = "ส่วนลดสมาชิก"}
            };

        [BackendPermission(isEPromotion: true)]
        public ActionResult Promotion()
        {
            return View(db.EComPromotions.ToList());
        }

        [BackendPermission(isEPromotion: true)]
        public ActionResult Promotion_Create()
        {
            ViewBag.Type = new SelectList(listPromotionName, "Value", "Text");
            ViewBag.EComCategoryId = new SelectList(db.EComCategories, "Id", "NameTH");
            EComPromotion eComPromotion = new EComPromotion();
            return View(eComPromotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEPromotion: true)]
        public ActionResult Promotion_Create(EComPromotion eComPromotion)
        {
            if (ModelState.IsValid)
            {
                db.EComPromotions.Add(eComPromotion);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มโปรโมชั่นสินค้า ProductPromotion = " + eComPromotion.Id, 1);//add to logfile

                return RedirectToAction("Promotion_Detail", new { id = eComPromotion.Id });
            }
            ViewBag.Type = new SelectList(listPromotionName, "Value", "Text");
            ViewBag.EComCategoryId = new SelectList(db.EComCategories, "Id", "NameTH");
            return View(eComPromotion);
        }

        [BackendPermission(isEPromotion: true)]
        public ActionResult Promotion_Detail(int id = 0)
        {
            if (id.Equals(0))
                return RedirectToAction("Promotion");
            ViewBag.Type = new SelectList(listPromotionName, "Value", "Text");
            ViewBag.EComCategoryId = new SelectList(db.EComCategories, "Id", "NameTH");
            return View(db.EComPromotions.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEPromotion: true)]
        public ActionResult Promotion_Detail(EComPromotion eComPromotion)
        {
            if (eComPromotion.Id.Equals(0))
                return RedirectToAction("Promotion");

            var defaulteComPromotion = db.EComPromotions.Find(eComPromotion.Id);
            if (ModelState.IsValid)
            {
                db.Entry(defaulteComPromotion).CurrentValues.SetValues(eComPromotion);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขโปรโมชั่นสินค้า ProductPromotion = " + eComPromotion.Id, 1);//add to logfile
            }

            ViewBag.Type = new SelectList(listPromotionName, "Value", "Text", eComPromotion.Type);
            ViewBag.EComCategoryId = new SelectList(db.EComCategories, "Id", "NameTH", eComPromotion.EComCategoryId);
            return View(eComPromotion);
        }

        //TODO add promotion detail Post method

        #endregion Promotion

        #region Statistic

        /// <summary>
        /// Get Log user view by product
        /// </summary>
        /// <param name="ds">Search by datetime</param>
        /// <param name="membershipId">Id of Membership</param>
        /// <returns>Grouping[productId, EComLogView]</returns>
        private IEnumerable<IGrouping<Int32, EComLogView>> getEComLogViewsGroup(DateTime? ds = null, int? membershipId = null)
        {
            var filterByMembership = db.EComLogViews.ToList();
            if (membershipId != null)
                filterByMembership = filterByMembership.Where(p => p.MembershipId == membershipId.GetValueOrDefault(0)).ToList();

            if (ds != null)
                return (from p in filterByMembership where p.Datetime > ds group p by p.EComProductId into pp orderby pp.Count() descending select pp);
            else
                return (from p in filterByMembership group p by p.EComProductId into pp orderby pp.Count() descending select pp);
        }

        /// <summary>
        /// Get Log user view by membership
        /// </summary>
        /// <param name="ds">Search by datetime</param>
        /// <param name="membershipId">Id of Membership</param>
        /// <returns>Grouping[membershipId or null, EComLogView]</returns>
        private IEnumerable<IGrouping<Int32?, EComLogView>> getEComLogViewsGroupByMembership(DateTime? ds = null, int? membershipId = null)
        {
            var filterByMembership = db.EComLogViews.ToList();
            if (membershipId != null)
                filterByMembership = filterByMembership.Where(p => p.MembershipId == membershipId.GetValueOrDefault(0)).ToList();

            if (ds != null)
                return (from p in filterByMembership where p.Datetime > ds group p by p.MembershipId);
            else
                return (from p in filterByMembership group p by p.MembershipId);
        }

        /// <summary>
        /// Get Keyword
        /// </summary>
        /// <param name="ds">Search by datetime</param>
        /// <param name="membershipId">Id of Membership</param>
        /// <returns>Grouping[keyword, EComKeyword]</returns>
        private IEnumerable<IGrouping<String, EComKeyword>> getEComKeyWordGroup(DateTime? ds = null, int? membershipId = null)
        {
            var filterByMembership = db.EComKeywords.ToList();
            if (membershipId != null)
                filterByMembership = filterByMembership.Where(p => p.MembershipId == membershipId.GetValueOrDefault(0)).ToList();

            if (ds != null)
                return (from p in filterByMembership where p.Datetime > ds group p by p.Keyword into pp orderby pp.Count() descending select pp);
            else
                return (from p in filterByMembership group p by p.Keyword into pp orderby pp.Count() descending select pp);
        }

        /// <summary>
        /// Get Payment OrderItem
        /// </summary>
        /// <param name="ds">Search by datetime</param>
        /// <param name="membershipId">Id of Membership</param>
        /// <returns>Grouping[productId, EComOrderItem]</returns>
        private IEnumerable<IGrouping<Int32, EComOrderItem>> getEComOrderItemGroup(DateTime? ds = null, int? membershipId = null)
        {
            var filterByMembership = db.EComOrderItems.ToList();
            if (membershipId != null)
                filterByMembership = filterByMembership.Where(p => p.EComOrder.MembershipId == membershipId.GetValueOrDefault(0)).ToList();

            if (ds != null)
                return (from o in filterByMembership where o.EComOrder.Datetime > ds group o by o.EComProductId into oi orderby oi.Where(i => i.EComOrder.PaymentProcessStatusId == 3).Sum(i => i.Quantity) descending select oi);
            else
                return (from o in filterByMembership group o by o.EComProductId into oi orderby oi.Where(i => i.EComOrder.PaymentProcessStatusId == 3).Sum(i => i.Quantity) descending select oi);
        }

        /// <summary>
        /// Get datetime by datastring
        /// </summary>
        /// <param name="sIn">datastring D7 last7day, M1 last1Month, M3 last3Month, Y1 last1Year, else null</param>
        /// <returns>datetime</returns>
        private DateTime? getDatetimeByString(string sIn)
        {
            DateTime? date = null;
            switch (sIn)
            {
                case "D7":
                    date = DateTime.Today.AddDays(-7);
                    break;

                case "M1":
                    date = DateTime.Today.AddMonths(-1);
                    break;

                case "M3":
                    date = DateTime.Today.AddMonths(-3);
                    break;

                case "Y1":
                    date = DateTime.Today.AddYears(-1);
                    break;
            }
            return date;
        }

        //
        //Statistic
        [BackendPermission(isEStatistic: true)]
        public ActionResult Statistic(string s = "AL", string pv = "AL", string pm = "AL")
        {
            ViewBag.Keyword = getEComKeyWordGroup(getDatetimeByString(s));
            ViewBag.Product = getEComLogViewsGroup(getDatetimeByString(pv));
            ViewBag.Order = getEComOrderItemGroup(getDatetimeByString(pm));

            ViewBag.OrderOfMembership = db.EComOrders.GroupBy(g => g.MembershipId);
            ViewBag.LogViewsOfMembership = getEComLogViewsGroupByMembership();

            return View();
        }

        public ActionResult Statistic_LogView()
        {
            return View();
        }

        public ActionResult Statistic_Keyword()
        {
            return View();
        }

        [BackendPermission(isEStatistic: true)]
        public ActionResult Statistic_Membership_Detail(int? id, string s = "AL", string pv = "AL", string pm = "AL")
        {
            if (id == null)
                RedirectToAction("Statistic");

            ViewBag.Keyword = getEComKeyWordGroup(getDatetimeByString(s), id);
            ViewBag.Product = getEComLogViewsGroup(getDatetimeByString(pv), id);
            ViewBag.Order = getEComOrderItemGroup(getDatetimeByString(pm), id);

            return View();
        }

        #endregion Statistic

        #region SlideShow

        [BackendPermission(isESlide: true)]
        public ActionResult SlideShow()
        {
            var slideShow = db.Slideshows.ToList();

            return View(slideShow);
        }

        [BackendPermission(isESlide: true)]
        public ActionResult SlideShow_Add()
        {
            var slideShow = new Slideshow();
            return View(slideShow);
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        [BackendPermission(isESlide: true)]
        public ActionResult SlideShow_Add(Slideshow slideShow, HttpPostedFileBase[] Image, SlideshowTranslate stTh, SlideshowTranslate stEn)
        {
            slideShow.SlideshowTranslates.Add(stTh);
            slideShow.SlideshowTranslates.Add(stEn);
            if (ModelState.IsValid)
            {
                slideShow.Image = Image.UploadAndSave("~/E-Commerce/Images/Uploads/").ToJson();
                db.Slideshows.Add(slideShow);
                db.SaveChanges();
                return RedirectToAction("SlideShow_Detail", slideShow.Id);
            }
            return View(slideShow);
        }

        [BackendPermission(isESlide: true)]
        public ActionResult SlideShow_Detail(int id = 0)
        {
            var slideshow = db.Slideshows.Find(id);
            if (slideshow == null)
                return RedirectToAction("SlideShow");
            return View(slideshow);
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        [BackendPermission(isESlide: true)]
        public ActionResult SlideShow_Detail(int id, Slideshow slideShow, HttpPostedFileBase[] Image, SlideshowTranslate stTh, SlideshowTranslate stEn)
        {
            slideShow.SlideshowTranslates.Add(stTh);
            slideShow.SlideshowTranslates.Add(stEn);
            if (ModelState.IsValid)
            {
                var originalSlidshow = db.Slideshows.Find(id);
                slideShow.Image = originalSlidshow.Image;

                if (Image.FirstOrDefault() != null)
                {
                    var ImageList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(originalSlidshow.Image);
                    ImageList.RemoveAndSave(ImageList.FirstOrDefault());
                    slideShow.Image = ImageList.Add(Image.UploadAndSave("~/E-Commerce/Images/Uploads/")).ToJson();
                }

                db.Entry(originalSlidshow).CurrentValues.SetValues(slideShow);
                db.Entry(originalSlidshow.getSlideshowTranslate("th")).CurrentValues.SetValues(stTh);
                db.Entry(originalSlidshow.getSlideshowTranslate("en")).CurrentValues.SetValues(stEn);
                db.SaveChanges();
            }
            return View(slideShow);
        }

        #endregion SlideShow

        #region DeliverPromotion

        //
        //DeliverPromotion
        private System.Collections.Generic.List<SelectListItem> listDeliverPromotionName = new System.Collections.Generic.List<SelectListItem>{
            new SelectListItem {Value = "1",Text = "ลดตามราคาสินค้า"},
            //new SelectListItem {Value = "2",Text = "ลดตามราคาสินค้าแต่ละสินค้า"}
            };

        [BackendPermission(isEPromotion: true)]
        public ActionResult DeliverPromotion()
        {
            return View(db.EComDeliverPromotions.ToList());
        }

        [BackendPermission(isEPromotion: true)]
        public ActionResult DeliverPromotionCreate()
        {
            var eComProducts = db.EComProducts.Where(p => p.EComDeliverPromotionMaps.Count() == 0).ToList();
            ViewBag.PromotionProductMap = new MultiSelectList(eComProducts, "Id", "NameTh");
            ViewBag.Type = new SelectList(listDeliverPromotionName, "Value", "Text");

            return View(new EComDeliverPromotion());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEPromotion: true)]
        public ActionResult DeliverPromotionCreate(EComDeliverPromotion eComDeliverPromotion, int[] PromotionProductMap)
        {
            if (ModelState.IsValid)
            {
                db.EComDeliverPromotions.Add(eComDeliverPromotion);
                if (PromotionProductMap != null)
                    foreach (int productId in PromotionProductMap)
                    {
                        var eComDeliverPromotionMap = new EComDeliverPromotionMap();
                        eComDeliverPromotionMap.EComDeliverPromotion = eComDeliverPromotion;
                        eComDeliverPromotionMap.EComProductId = productId;
                        db.EComDeliverPromotionMaps.Add(eComDeliverPromotionMap);
                    }
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "เพิ่มโปรโมชั่นการจัดส่ง DeliverPromotion = " + eComDeliverPromotion.Id, 1);//add to logfile

                return RedirectToAction("DeliverPromotionDetail", new { id = eComDeliverPromotion.Id });
            }
            var eComProducts = db.EComProducts.Where(p => p.EComDeliverPromotionMaps.Count() == 0).ToList();
            ViewBag.PromotionProductMap = new MultiSelectList(eComProducts, "Id", "NameTh", PromotionProductMap);
            ViewBag.Type = new SelectList(listDeliverPromotionName, "Value", "Text");

            return View(eComDeliverPromotion);
        }

        [BackendPermission(isEPromotion: true)]
        public ActionResult DeliverPromotionDetail(int id)
        {
            var eComDeliverPromotion = db.EComDeliverPromotions.Find(id);
            var eComProducts = db.EComProducts.Where(p => p.EComDeliverPromotionMaps.Count() == 0).ToList();
            eComProducts = eComProducts.Union(eComDeliverPromotion.EComDeliverPromotionMaps.Select(p => p.EComProduct)).OrderBy(p => p.Id).ToList();
            ViewBag.PromotionProductMap = new MultiSelectList(eComProducts, "Id", "NameTh", eComDeliverPromotion.EComDeliverPromotionMaps.Where(p => p.EComDeliverPromotion.Type == 2).Select(p => p.EComProduct.Id).ToArray());
            ViewBag.Type = new SelectList(listDeliverPromotionName, "Value", "Text", eComDeliverPromotion.Type);
            return View(eComDeliverPromotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BackendPermission(isEPromotion: true)]
        public ActionResult DeliverPromotionDetail(int id, EComDeliverPromotion eComDeliverPromotion, int[] PromotionProductMap)
        {
            var defaultEComDeliverPromotion = db.EComDeliverPromotions.Find(id);
            if (ModelState.IsValid)
            {
                if (PromotionProductMap != null)
                {
                    int[] removeProduct = defaultEComDeliverPromotion.EComDeliverPromotionMaps.Select(p => p.EComProductId).Except(PromotionProductMap).ToArray();
                    int[] addProduct = PromotionProductMap.Except(defaultEComDeliverPromotion.EComDeliverPromotionMaps.Select(p => p.EComProductId)).ToArray();

                    if (removeProduct.Count() > 0)
                    {
                        var eComDeliverPromotionMaps = defaultEComDeliverPromotion.EComDeliverPromotionMaps.AsParallel().Where(p => Array.Exists(removeProduct, e => e == p.EComProductId)).ToList();
                        foreach (var eComDeliverPromotionMap in eComDeliverPromotionMaps)
                        {
                            db.EComDeliverPromotionMaps.Remove(eComDeliverPromotionMap);
                        }
                    }
                    if (addProduct.Count() > 0)
                    {
                        foreach (int productId in addProduct)
                        {
                            var eComDeliverPromotionMap = new EComDeliverPromotionMap();
                            eComDeliverPromotionMap.EComDeliverPromotion = defaultEComDeliverPromotion;
                            eComDeliverPromotionMap.EComProductId = productId;
                            db.EComDeliverPromotionMaps.Add(eComDeliverPromotionMap);
                        }
                    }
                }

                db.Entry(defaultEComDeliverPromotion).CurrentValues.SetValues(eComDeliverPromotion);
                db.SaveChanges();

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขโปรโมชั่นการจัดส่ง DeliverPromotion = " + eComDeliverPromotion.Id, 1);//add to logfile

                return RedirectToAction("DeliverPromotionDetail", new { id = id });
            }
            var eComProducts = db.EComProducts.Where(p => p.EComDeliverPromotionMaps.Count() == 0).ToList();
            eComProducts = eComProducts.Union(defaultEComDeliverPromotion.EComDeliverPromotionMaps.Select(p => p.EComProduct)).OrderBy(p => p.Id).ToList();
            ViewBag.PromotionProductMap = new MultiSelectList(eComProducts, "Id", "NameTh", PromotionProductMap);
            ViewBag.Type = new SelectList(listDeliverPromotionName, "Value", "Text", defaultEComDeliverPromotion.Type);

            return View(eComDeliverPromotion);
        }

        #endregion DeliverPromotion

        //E-Commerce/
        //├── Product/                        # ดูสินค้าทั้งหมด และสามารถกำหนดสินค้าเด่นในหน้านี้ได้
        //│   ├── Create/                     # สร้างสินค้าตัวใหม่
        //│   └── Detail/                     # แก้ไขสินค้า
        //├── Order/                          # แสดงรายการสั่งซื้อทั้งหมด
        //│   └── Detail/                     # ดูรายละเอียดรายการสั่งซื้อ
        //├── Payment/                        # ดูรายการชำระเงิน
        //│   └── Detail/                     # ดูรายละเอียดการชำระเงิน และเปลี่ยนสถานะการชำระเงิน
        //├── Deliver/                        # ดูรายการส่งสินค้า
        //│   └── Detail/                     # ดูรายละเอียดการส่งสินค้า และเปลี่ยนสถานะการส่งสินค้า
        //├── Promotion/                      # แสดงรายการส่งเสิรมการขายทั้งหมด
        //│   ├── Create/                     # สร้างกิจกรรมส่งเสริงการขาย
        //│   └── Detail/                     # แก้ไขกิจกรรมส่งเสริมการขาย
        //└── Statistic                       # แสดงคำค้นหายอดนิยม และ สินค้ายอดนิยม
        //    ├── LogView/                    # แสดงประวัติการเข้าดูสินค้าของผู้ใช้แต่ละราย
        //    └── Keyword/                    # แสดงประวัติการค้นหาของผู้ใช้แต่ละราย
    }
}