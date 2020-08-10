using Git_system.Models.ECom.API;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class BankTypeController : MainController
    {
        [AllowCrossSite]
        public HttpResponseMessage Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, db.ConfirmPaymentBankTypes.ToList().ToAPI(languageTypeID: _LanguageTypeId));
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}