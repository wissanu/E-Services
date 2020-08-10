using Git_system.Models;
using Git_system.Models.ECom.API;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class ProductController : MainController
    {
        private List<EComProduct> queryEComProductIsActive()
        {
            return db.EComProducts.Where(p => p.ActiveStatus.Equals(true)).ToList();
        }

        private List<EComProduct> findRelated(List<EComProduct> mainEComProducts, int productId)
        {
            List<EComProduct> eComProducts = new List<EComProduct>();

            List<EComProduct> eComProudtsAllActive = mainEComProducts;
            EComProduct eComProduct = eComProudtsAllActive.Where(p => p.Id.Equals(productId)).FirstOrDefault();
            List<int> categoryIdForProduct = eComProduct.EComCategoryMaps.Where(c => c.EComCategory.VisibleStatus.Equals(true)).Select(c => c.EComCategoryId).ToList();

            List<EComProduct> mP = new List<EComProduct>();
            mP.Add(eComProduct);
            foreach (EComProduct p in eComProudtsAllActive.Except(mP))
            {
                if (p.EComCategoryMaps.Select(c => c.EComCategoryId).Intersect(categoryIdForProduct).Count() > 0)
                {
                    eComProducts.Add(p);
                }
            }
            if (eComProducts.Count() < 4)
            {
                foreach (EComProduct p in eComProudtsAllActive.Except(eComProducts).Except(mP))
                {
                    eComProducts.Add(p);
                    if (eComProducts.Count > 4)
                        break;
                }
            }
            return eComProducts;
        }

        // query
        // GET api/product
        [AllowCrossSite]
        public HttpResponseMessage Get(int Category = 0, string Search = "", int Related = 0)
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            List<EComProduct> eComProducts = new List<EComProduct>();
            if (Category == 0)
            {
                eComProducts = db.EComProducts.Where(p => p.ActiveStatus.Equals(true)).ToList();
            }
            else
            {
                eComProducts = db.EComCategoryMaps.Where(c => c.EComCategoryId == Category).Select(c => c.EComProduct).Where(p => p.ActiveStatus.Equals(true)).ToList();
            }
            if (Search != "")
            {
                eComProducts = eComProducts.Where(p => (p.NameTH + " " + p.NameEN).ToLower().Contains(Search.ToLower())).ToList();// ทำสั้งที่ใช้ในการค้นหา
                EComKeyword keyword = new EComKeyword();
                keyword.Datetime = System.DateTime.Now;
                keyword.Keyword = Search;
                if (_MembershipId != 0)
                    keyword.MembershipId = _MembershipId;
                db.EComKeywords.Add(keyword);
                db.SaveChanges();
            }
            else if (Related != 0)
            {
                eComProducts = findRelated(eComProducts, Related);
            }

            dataAndStatus = new DataAndStatus(1, eComProducts.ToAPIOneLanguage(membershipType: _MembershipTypeId, languageTypeID: _LanguageTypeId));

            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        // get
        // GET api/product/5
        [AllowCrossSite]
        public HttpResponseMessage Get(int id)
        {
            int membershipId = _MembershipId;
            System.Console.WriteLine(membershipId);

            DataAndStatus dataAndStatus = new DataAndStatus();
            EComProduct eComProduct = db.EComProducts.Find(id);

            dataAndStatus = new DataAndStatus(1, eComProduct.ToAPIOneLanguageWithHistory(membership: new Membership(_MembershipId, (short)_MembershipTypeId), languageTypeID: _LanguageTypeId));

            var logView = new EComLogView();
            logView.EComProductId = eComProduct.Id;
            if (membershipId != 0)
                logView.MembershipId = membershipId;
            logView.Datetime = System.DateTime.Now;
            db.EComLogViews.Add(logView);
            db.SaveChanges();

            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}