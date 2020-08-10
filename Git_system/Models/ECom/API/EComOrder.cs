using System;
using System.Collections.Generic;

namespace Git_system.Models.ECom.API
{
    public class EComPaymentProcessStatusAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EComDeliverProcessStatusAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EComOrderAPI
    {
        public int Id { get; set; }

        public System.DateTime Datetime { get; set; }

        public int MembershipId { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public int PaymentType { get; set; }

        public int PaymentProcessStatusId { get; set; }
        public EComPaymentProcessStatusAPI PaymentProcessStatus { get; set; }
        public int DeliverProcessStatusId { get; set; }
        public EComDeliverProcessStatusAPI DeliverProcessStatus { get; set; }
        public string DeliverNumber { get; set; }
        public string DeliverSendName { get; set; }
        public Nullable<System.DateTime> DeliverSendDate { get; set; }

        public string SendFullname { get; set; }

        public string SendAddress { get; set; }

        public string SendProvince { get; set; }

        public string SendCountry { get; set; }

        public string SendPostcode { get; set; }

        public string SendPhonenumber { get; set; }

        public string SendEmail { get; set; }

        public string ReceiptFullname { get; set; }

        public string ReceiptAddress { get; set; }

        public string ReceiptProvince { get; set; }

        public string ReceiptCountry { get; set; }

        public string ReceiptPostcode { get; set; }

        public string ReceiptPhonenumber { get; set; }

        public string ReceiptEmail { get; set; }

        public bool isEletronic { get; set; }

        public List<EComOrderItemAPI> OrderItem { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static EComDeliverProcessStatusAPI ToAPI(this DeliverProcessStatus content, string language = "th")
        {
            EComDeliverProcessStatusAPI rContent = new EComDeliverProcessStatusAPI();
            if (content != null)
            {
                rContent.Id = content.Id;
                rContent.Name = language == "th" ? content.NameTH : content.NameEN;
            }
            return rContent;
        }

        public static EComPaymentProcessStatusAPI ToAPI(this PaymentProcessStatus content, string language = "th")
        {
            EComPaymentProcessStatusAPI rContent = new EComPaymentProcessStatusAPI();
            if (content != null)
            {
                rContent.Id = content.Id;
                rContent.Name = language == "th" ? content.NameTH : content.NameEN;
            }
            return rContent;
        }

        public static List<Git_system.Models.ECom.API.EComOrderAPI> ToAPI(this List<Git_system.Models.EComOrder> contents, string language = "th")
        {
            //List<Git_system.Models.ECom.API.EComOrderAPI> rContents = new List<Git_system.Models.ECom.API.EComOrderAPI>();
            //foreach (Git_system.Models.EComOrder content in contents)
            //{
            //    rContents.Add(content.ToAPI(language: language));
            //}
            List<Git_system.Models.ECom.API.EComOrderAPI> rContents = new List<Git_system.Models.ECom.API.EComOrderAPI>();
            if (contents.Count > 0)
                rContents = contents.ConvertAll(c => c.ToAPI(language));
            return rContents;
        }

        public static Git_system.Models.ECom.API.EComOrderAPI ToAPI(this Git_system.Models.EComOrder content, string language = "th")
        {
            Git_system.Models.ECom.API.EComOrderAPI rContent = new Git_system.Models.ECom.API.EComOrderAPI();
            rContent.Id = content.Id;
            rContent.Datetime = content.Datetime;
            rContent.MembershipId = content.MembershipId;
            rContent.Price = content.Price;
            rContent.Currency = content.Currency;
            rContent.PaymentType = content.PaymentType;
            rContent.PaymentProcessStatusId = content.PaymentProcessStatusId;
            rContent.PaymentProcessStatus = content.PaymentProcessStatu.ToAPI(language);
            rContent.DeliverProcessStatusId = content.DeliverProcessStatusId;
            rContent.DeliverProcessStatus = content.DeliverProcessStatu.ToAPI(language);
            rContent.DeliverNumber = content.DeliverNumber;
            rContent.DeliverSendName = content.DeliverSendName;
            rContent.DeliverSendDate = content.DeliverSendDate;
            rContent.SendFullname = content.SendFullname;
            rContent.SendAddress = content.SendAddress;
            rContent.SendProvince = content.SendProvince;
            rContent.SendCountry = content.SendCountry;
            rContent.SendPostcode = content.SendPostcode;
            rContent.SendPhonenumber = content.SendPhonenumber;
            rContent.SendEmail = content.SendEmail;
            rContent.ReceiptFullname = content.ReceiptFullname;
            rContent.ReceiptAddress = content.ReceiptAddress;
            rContent.ReceiptProvince = content.ReceiptProvince;
            rContent.ReceiptCountry = content.ReceiptCountry;
            rContent.ReceiptPostcode = content.ReceiptPostcode;
            rContent.ReceiptPhonenumber = content.ReceiptPhonenumber;
            rContent.ReceiptEmail = content.ReceiptEmail;
            rContent.OrderItem = content.EComOrderItems.ToAPI();
            //TODO: change to read from database.
            rContent.isEletronic = PayHelper.checkEletronicOrder(content);
            return rContent;
        }
    }
}