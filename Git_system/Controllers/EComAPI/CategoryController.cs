using Git_system.Models;
using Git_system.Models.ECom.API;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class CategoryController : MainController
    {
        // query
        // GET api/Category
        [AllowCrossSite]
        public HttpResponseMessage Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, db.EComCategories.Where(
                                c => c.ActiveStatus.Equals(true) &&
                                c.VisibleStatus.Equals(true) &&
                                c.EComCategoryMaps.Select(p => p.EComProduct).Where(p => p.ActiveStatus.Equals(true)).Count() > 0
                            ).OrderBy(c => c.OrderBy).ToList().ToAPI(languageTypeID: _LanguageTypeId));
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        // get
        // GET api/Category/5
        [AllowCrossSite]
        public HttpResponseMessage Get(int id, int limit = 0)
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            List<EComProduct> eComProducts = new List<EComProduct>();
            List<EComCategoryMap> eComCategoryMaps = db.EComCategoryMaps.Where(c => c.EComCategoryId == id).ToList();
            EComCategory eComCategory = db.EComCategories.Find(id);
            if (eComCategory != null)
            {
                if (eComCategory.SortOrderStatus.Equals(false))
                    eComProducts = eComCategoryMaps.OrderBy(c => c.EComProductId).Select(c => c.EComProduct).Where(p => p.ActiveStatus.Equals(true)).ToList();
                else
                    eComProducts = eComCategoryMaps.OrderBy(c => c.OrderBy).Select(c => c.EComProduct).Where(p => p.ActiveStatus.Equals(true)).ToList();
                var eComProductsWithTake = ((limit == 0 || limit < 0) ? eComProducts : eComProducts.Take(limit)).ToList();
                dataAndStatus = new DataAndStatus(1, eComProductsWithTake.ToAPIOneLanguageWithHistory(membership: new Membership(_MembershipId, (short)_MembershipTypeId), languageTypeID: _LanguageTypeId));
                /// TODO เพิ่มในส่วนของการเก็บประวัติการเข้าชมสินค้า
            }
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}