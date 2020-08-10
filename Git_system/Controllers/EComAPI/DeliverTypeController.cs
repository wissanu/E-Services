using Git_system.Models.ECom.API;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class DeliverTypeController : MainController
    {
        // query
        // GET api/DeliverType
        [AllowCrossSite]
        public HttpResponseMessage Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, db.EComDeliverTypes.ToList().ToAPI(membershipType: _MembershipTypeId, languageTypeID: _LanguageTypeId));
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        // get
        // GET api/DeliverType/5
        [AllowCrossSite]
        public HttpResponseMessage Get(int id)
        {
            int membershipId = _MembershipId;
            System.Console.WriteLine(membershipId);

            DataAndStatus dataAndStatus = new DataAndStatus();
            Models.EComDeliverType eComDeliverType = db.EComDeliverTypes.Find(id);
            if (eComDeliverType != null)
                dataAndStatus = new DataAndStatus(1, eComDeliverType.ToAPI(membershipType: _MembershipTypeId, languageTypeID: _LanguageTypeId));
            /// TODO เพิ่มในส่วนของการเก็บประวัติการเข้าชมสินค้า
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}