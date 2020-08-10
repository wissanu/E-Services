using Git_system.Helper;
using Git_system.Models;
using Git_system.Models.ECom.API;
using Git_system.Models.ECom.API.Process;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class Cart
    {
        public int DeliverType { get; set; }

        public int PaymentType { get; set; }

        public List<CartItem> Items { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }

        public CartAddress SenderAddress { get; set; }

        public CartAddress ReceiptAddress { get; set; }
    }

    public class CartAddress
    {
        public string Fullname { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public string Phonenumber { get; set; }

        public string Email { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }

        //public string Name { get; set; }

        //public string Detail { get; set; }

        //public List<string> ImageFiles { get; set; }

        public int Quantity { get; set; }

        public double PricePerUnit { get; set; }

        public string Currency { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static EComOrder toEComOrder(this Cart content)
        {
            EComOrder rcontent = new EComOrder();

            //rcontent.Id = content.Id ;
            //rcontent.DateTime Datetime = content.DateTime Datetime ;
            //rcontent.MembershipId = content.MembershipId ;
            rcontent.Price = content.Price;
            //rcontent.Currency = content.Currency;
            rcontent.PaymentType = content.PaymentType;
            //rcontent.PaymentProcessStatusId = content.PaymentProcessStatusId ;
            //rcontent.DeliverProcessStatusId = content.DeliverProcessStatusId ;
            //rcontent.DeliverNumber = content.DeliverNumber ;
            rcontent.EComDeliverTypeId = content.DeliverType;
            rcontent.SendFullname = content.SenderAddress.Fullname;
            rcontent.SendAddress = content.SenderAddress.Address;
            rcontent.SendProvince = content.SenderAddress.Province;
            rcontent.SendCountry = content.SenderAddress.Country;
            rcontent.SendPostcode = content.SenderAddress.Postcode;
            rcontent.SendPhonenumber = content.SenderAddress.Phonenumber;
            rcontent.SendEmail = content.SenderAddress.Email;
            rcontent.ReceiptFullname = content.ReceiptAddress.Fullname;
            rcontent.ReceiptAddress = content.ReceiptAddress.Address;
            rcontent.ReceiptProvince = content.ReceiptAddress.Province;
            rcontent.ReceiptCountry = content.ReceiptAddress.Country;
            rcontent.ReceiptPostcode = content.ReceiptAddress.Postcode;
            rcontent.ReceiptPhonenumber = content.ReceiptAddress.Phonenumber;
            rcontent.ReceiptEmail = content.ReceiptAddress.Email;

            rcontent.EComOrderItems = content.Items.toEComOrderItem();
            rcontent.Currency = content.Items.Count() != 0 ? content.Items.FirstOrDefault().Currency : string.Empty;
            return rcontent;
        }

        public static List<EComOrderItem> toEComOrderItem(this List<CartItem> content)
        {
            List<EComOrderItem> rcontent = new List<EComOrderItem>();
            foreach (CartItem cartItem in content)
            {
                rcontent.Add(cartItem.toEComOrderItem());
            }
            return rcontent;
        }

        public static EComOrderItem toEComOrderItem(this CartItem content)
        {
            EComOrderItem rcontent = new EComOrderItem();
            rcontent.EComProductId = content.Id;
            rcontent.Quantity = content.Quantity;
            return rcontent;
        }
    }

    public class OrderController : MainController
    {
        /// <summary>
        /// ทำการแสดง Order ของแต่ละบุคคล
        /// </summary>
        /// <param name="membershipId">หมายเลขสมาชิก</param>
        /// <param name="payment">สถานะการชำระ</param>
        /// <param name="deliver">สถานะการส่งสินค้า</param>
        /// <returns>คือค่าเป็น json ของ dateAndStatus โดยมี data เป็น EComOrdersAPI</returns>
        private DataAndStatus ReadOrderForMembership(int membershipId, int payment = 0, int deliver = 0)
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            List<EComOrder> eComOrders = db.EComOrders.Where(o => o.MembershipId == membershipId).OrderByDescending(o => o.Datetime).ToList();
            if (deliver != 0)
                eComOrders = eComOrders.Where(o => o.DeliverProcessStatusId == deliver).ToList();
            if (payment != 0)
                eComOrders = eComOrders.Where(o => o.PaymentProcessStatusId == payment).ToList();
            dataAndStatus = new DataAndStatus(1, eComOrders.ToAPI(_LanguageTypeId == 1 ? "th" : "en"));

            return dataAndStatus;
        }

        private DataAndStatus GetPayment(int paymentId)
        {
            return new DataAndStatus(1, (db.EComOrders.Find(paymentId) ?? new EComOrder()).ToAPI(_LanguageTypeId == 1 ? "th" : "en"));
        }

        // query
        // GET api/Order
        [AllowCrossSite]
        public HttpResponseMessage Get(int Payment = 0, int Deliver = 0)
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            if (_MembershipId == 0)
            {
                dataAndStatus = new DataAndStatus(201, null);
            }
            else
            {
                dataAndStatus = ReadOrderForMembership(_MembershipId, Payment, Deliver);
            }

            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        // query
        // GET api/Order/5
        [AllowCrossSite]
        public HttpResponseMessage Get(int id)
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            if (_MembershipId == 0)
            {
                dataAndStatus = new DataAndStatus(201, null);
            }
            else
            {
                dataAndStatus = GetPayment(id);
            }

            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        private DataAndStatus saveOrder(Cart cart)
        {
            var Order = new Order(db);

            EComOrder order = cart.toEComOrder();
            List<EComOrderItem> orderItems = order.EComOrderItems.ToList();
            DataAndStatus dataAndStatus = new DataAndStatus();

            if (orderItems.Count() == 0)
                return dataAndStatus = new DataAndStatus(104, null);

            if (_MembershipId == 0)
                return dataAndStatus = new DataAndStatus(201, null);

            order.Datetime = System.DateTime.Now;
            order.MembershipId = _MembershipId;
            bool checkOrder = Order.CheckOrderItems(orderItems);
            if (checkOrder)
            {
                var orderDetail = Order.checkOrderDetail(orderItems, _MembershipTypeId);
                order.Price = orderDetail.summaryOrderTotalPrice();
                var jOrderDetail = Newtonsoft.Json.JsonConvert.SerializeObject(orderDetail);
                order.JOrderDetail = jOrderDetail;
                DataAndStatus addOrderDataAndStatus = Order.AddOrder(order, orderItems);

                var membership = _MembershipId != 0 ? db.Memberships.Find(_MembershipId) : new Models.Membership();

                // Send email when payment type is bank
                if (order.PaymentType == 1)
                {
                    EmailMethod.sendEmailForEComPayment(membership, _LanguageTypeId == 1 ? "TH" : "EN", order, orderDetail);
                    EmailMethod.sendEmailForEComPaymentToAdmin(order, orderDetail);
                }

                if (addOrderDataAndStatus.status == 1)
                    dataAndStatus = new DataAndStatus(1, order.ToAPI());
                else
                    dataAndStatus = addOrderDataAndStatus;
            }
            else
            {
                dataAndStatus = new DataAndStatus(101, null);
            }
            return dataAndStatus;
        }

        private DataAndStatus checkOrder(Cart cart)
        {
            var Order = new Order();

            EComOrder order = cart.toEComOrder();
            List<EComOrderItem> orderItems = order.EComOrderItems.ToList();
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, null);

            if (orderItems.Count() == 0)
                return dataAndStatus = new DataAndStatus(104, null);

            if (_MembershipId == 0)
                return dataAndStatus = new DataAndStatus(201, null);

            order.Datetime = System.DateTime.Now;
            order.MembershipId = _MembershipId;
            bool checkOrder = Order.CheckOrderItems(orderItems);
            if (checkOrder)
            {
                var orderDetail = Order.checkOrderDetail(orderItems, _MembershipTypeId);
                dataAndStatus = new DataAndStatus(1, orderDetail);
            }

            return dataAndStatus;
        }

        // save or check order
        // POST api/Order
        //public HttpResponseMessage Post(EComOrder Order, List<EComOrderItem> OrderItems)
        [AllowCrossSite]
        public HttpResponseMessage Post(object value, bool check = false)
        {
            Cart cart = new Cart();
            cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(value.ToString());

            DataAndStatus dataAndStatus = new DataAndStatus();
            if (check)
                dataAndStatus = checkOrder(cart);
            else
                dataAndStatus = saveOrder(cart);

            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, dataAndStatus);
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}