using Git_system.Models;
using Git_system.Models.ECom;
using Git_system.Models.ECom.API;
using System.Data.Entity;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Git_system.Controllers.EComAPI
{
    public class ConfirmOrderController : MainController
    {
        public class ConfirmOrder
        {
            //public ConfirmOrder()
            //{
            //    this.FilenameConfirm = new HttpPostedFile();
            //    this.FilenamePayment = new HttpPostedFile();
            //}

            public int Id { get; set; }

            public int EComOrderId { get; set; }

            public double Price { get; set; }

            public System.DateTime Datetime { get; set; }

            public HttpPostedFile FilenamePayment { get; set; }

            public HttpPostedFile FilenameConfirm { get; set; }

            public short ConfirmPaymentBankTypeId { get; set; }
        }

        [AllowCrossSite]
        public HttpResponseMessage Post()
        {
            const int paymentWaitingConfirmation = 1;
            const int paymentWaitingVerify = 2;
            const int paymentSuccess = 3;

            const int deliverShipping = 1;
            const int deliverShipped = 2;

            HttpResponseMessage result = null;

            var httpRequest = HttpContext.Current.Request;
            ConfirmOrder confirmOrder = new ConfirmOrder();

            confirmOrder.EComOrderId = System.Convert.ToInt32(httpRequest.Params["EComOrderId"]);

            DataAndStatus dataAndStatus = new DataAndStatus();

            var orderFind = db.EComOrders.Find(confirmOrder.EComOrderId);
            if (orderFind == null)
            {
                dataAndStatus = new DataAndStatus(111, null);
            }
            else if (orderFind.MembershipId != _MembershipId)
            {
                dataAndStatus = new DataAndStatus(113, null);
            }
            else if (httpRequest.Files.Count == 0)
            {
                dataAndStatus = new DataAndStatus(114, null);
            }
            else
            {
                confirmOrder.Price = System.Convert.ToDouble(httpRequest.Params["Price"]);
                confirmOrder.ConfirmPaymentBankTypeId = System.Convert.ToInt16(httpRequest.Params["ConfirmPaymentBankTypeId"]);
                if (httpRequest.Params["Datetime"].ToString().Trim() == "")
                {
                    dataAndStatus = new DataAndStatus(112, null);
                    return Request.CreateResponse(HttpStatusCode.OK, dataAndStatus);
                }
                else
                {
                    var datetimeString = httpRequest.Params["Datetime"].ToString();
                    if (httpRequest.Params["FCNLT"].ToString() == "f11b8154674d1f97d062be097d3a5ccab0fc27ee")
                    {
                        var usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                        confirmOrder.Datetime = System.Convert.ToDateTime(datetimeString, usDtfi);
                    }
                    else
                        confirmOrder.Datetime = System.Convert.ToDateTime(datetimeString);
                }
                if (httpRequest.Files.Count > 0)
                {
                    confirmOrder.FilenamePayment = httpRequest.Files["FilenamePayment"];
                    confirmOrder.FilenameConfirm = httpRequest.Files["FilenameConfirm"];
                }

                EComConfirmOrder eComConfirmOrder = new EComConfirmOrder();

                eComConfirmOrder.Datetime = confirmOrder.Datetime;
                eComConfirmOrder.EComOrderId = confirmOrder.EComOrderId;
                eComConfirmOrder.Price = confirmOrder.Price;
                eComConfirmOrder.ConfirmPaymentBankTypeId = confirmOrder.ConfirmPaymentBankTypeId;
                eComConfirmOrder.FilenameConfirm = confirmOrder.FilenameConfirm.UploadAndSave("~/E-Commerce/Confirm/").ToJson();
                eComConfirmOrder.FilenamePayment = confirmOrder.FilenamePayment.UploadAndSave("~/E-Commerce/Confirm/").ToJson();

                db.EComConfirmOrders.Add(eComConfirmOrder);
                //db.SaveChanges();
                if (eComConfirmOrder.EComOrder.PaymentProcessStatusId == paymentWaitingConfirmation)
                {
                    db.Entry(eComConfirmOrder.EComOrder).State = EntityState.Modified;
                    eComConfirmOrder.EComOrder.PaymentProcessStatusId = paymentWaitingVerify;
                    if (PayHelper.checkEletronicOrder(eComConfirmOrder.EComOrder))
                        eComConfirmOrder.EComOrder.DeliverProcessStatusId = deliverShipped;
                    //db.SaveChanges();
                }
                db.SaveChanges();

                Helper.EmailMethod.sendEmailForEComConfirmOrderToAdmin(orderFind);

                dataAndStatus = new DataAndStatus(1, null);
            }
            result = Request.CreateResponse(HttpStatusCode.OK, dataAndStatus);
            return result;
        }

        [AllowCrossSite]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse<string[]>(HttpStatusCode.OK, new string[] { "value1", "value2" });
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}