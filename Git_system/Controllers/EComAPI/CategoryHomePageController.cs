using Git_system.Models.ECom.API;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class CategoryHomePageController : MainController
    {
        // query
        // GET api/Category
        [AllowCrossSite]
        public HttpResponseMessage Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, db.EComCategories.Where(
                                c => c.ActiveStatus.Equals(true) &&
                                c.ShowOnPageStatus.Equals(true) &&
                                c.EComCategoryMaps.Select(p => p.EComProduct).Where(p => p.ActiveStatus.Equals(true)).Count() > 0
                            ).OrderBy(c => c.OrderBy).ToList().ToAPI(languageTypeID: _LanguageTypeId));
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}