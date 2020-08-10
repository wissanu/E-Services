using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Git_system.Models.ECom.API
{
    public static partial class ExtensionMethod
    {
        /// <summary>
        /// Convert to Product is one language.
        /// </summary>
        /// <param name="content">EcomProduct will be convert</param>
        /// <param name="membershipType">Type of membership.</param>
        /// <param name="languageTypeID">Type of language (1 : th , 2 : en)</param>
        /// <param name="currency">Currency name</param>
        /// <returns></returns>
        [Obsolete("Less information")]
        private static EComProductAPIOneLanguage ToAPIOneLanguage(this Git_system.Models.EComProduct content, int membershipType, int languageTypeID, string currency)
        {
            EComProductAPIOneLanguage rContent = new EComProductAPIOneLanguage();

            rContent.Id = content.Id;

            rContent.Name = languageTypeID == 1 ? content.NameTH : content.NameEN;
            rContent.Detail = languageTypeID == 1 ? content.DetailTH : content.DetailEN;
            rContent.Description = languageTypeID == 1 ? content.DescriptionTH : content.DescriptionEN;
            rContent.Currency = currency;

            rContent.ImageFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(content.ImageFiles ?? "[]");

            rContent.OtherFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(content.OtherFiles ?? "[]");

            rContent.PriceNormal = content.PriceNormalForMembershipType(new Membership((short)membershipType));

            rContent.Price = content.toTotalPriceForMembership(new Membership((short)membershipType));

            rContent.PriceDeliver = content.toDeliverPrice(new Membership((short)membershipType));

            //rContent.ActiveStatus = content.ActiveStatus;

            //rContent.PinStatus = content.PinStatus;

            //rContent.PinWeight = content.PinWeight;

            rContent.ElectronicFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(content.ElectronicFiles ?? "[]");

            rContent.DemoElectronicFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(content.DemoElectronicFiles ?? "[]");

            rContent.ElectronicFileStatus = content.ElectronicFileStatus;

            rContent.DeliverStatus = content.DeliverStatus;

            rContent.Category = content.EComCategoryMaps.Select(c => c.EComCategory).Where(c => c.ActiveStatus.Equals(true) && c.VisibleStatus.Equals(true) && c.EComCategoryMaps.Select(p => p.EComProduct).Where(p => p.ActiveStatus.Equals(true)).Count() > 0).ToList().ToAPI(languageTypeID: languageTypeID);

            EComProductStock stock = new EComProductStock();
            stock.InStock = content.InStock;
            stock.Quantity = content.Quantity;
            rContent.Stock = stock;

            return rContent;
        }

        private static string getProductHistoryPayDetail(this Git_system.Models.EComProduct content)
        {
            if (content.EComOrderItems == null)
                content.EComOrderItems = new Models.Database_MainEntities1().EComOrderItems.ToList();

            if (content.EComOrderItems == null)
                return "[]";

            content.EComOrderItems.ToList().ConvertAll(o => o.EComOrder).Where(o => o.MembershipId == 1).OrderByDescending(o => o.Id);

            return "";
        }

        /// <summary>
        /// Convert to Product is one language.
        /// </summary>
        /// <param name="content">EcomProduct will be convert</param>
        /// <param name="membershipType">Type of membership.</param>
        /// <param name="languageTypeID">Type of language (1 : th , 2 : en)</param>
        /// <returns></returns>
        [Obsolete("Less information")]
        public static EComProductAPIOneLanguage ToAPIOneLanguage(this Git_system.Models.EComProduct content, int membershipType, int languageTypeID)
        {
            return content.ToAPIOneLanguage(membershipType: membershipType, languageTypeID: languageTypeID, currency: Process.Membership.checkCurrencyOfMembership(membershipType: membershipType));
        }

        /// <summary>
        /// Convert to Product is one language.
        /// </summary>
        /// <param name="content">List of EcomProduct will be convert</param>
        /// <param name="membershipType">Type of membership.</param>
        /// <param name="languageTypeID">Type of language (1 : th , 2 : en)</param>
        /// <returns></returns>
        [Obsolete("Less information")]
        public static List<EComProductAPIOneLanguage> ToAPIOneLanguage(this List<Git_system.Models.EComProduct> contents, int membershipType, int languageTypeID)
        {
            List<EComProductAPIOneLanguage> rContents = new List<EComProductAPIOneLanguage>();
            string currency = Process.Membership.checkCurrencyOfMembership(membershipType: membershipType);
            foreach (Git_system.Models.EComProduct content in contents)
            {
                rContents.Add(content.ToAPIOneLanguage(membershipType: membershipType, languageTypeID: languageTypeID, currency: currency));
            }
            return rContents;
        }

        public static List<EComProductAPIOneLanguageWithPayHistory> ToAPIOneLanguageWithHistory(this List<Git_system.Models.EComProduct> contents, Membership membership, int languageTypeID)
        {
            int membershipType = 0;
            if (membership.MembershipRegisterTypeId != 0)
                membershipType = membership.MembershipRegisterTypeId;

            return contents.ConvertAll(c => c.ToAPIOneLanguageWithHistory(membership: membership, languageTypeID: languageTypeID, currency: Process.Membership.checkCurrencyOfMembership(membershipType: membershipType)));
        }

        public static EComProductAPIOneLanguageWithPayHistory ToAPIOneLanguageWithHistory(this Git_system.Models.EComProduct content, Membership membership, int languageTypeID)
        {
            int membershipType = 0;
            if (membership.MembershipRegisterTypeId != 0)
                membershipType = membership.MembershipRegisterTypeId;

            return content.ToAPIOneLanguageWithHistory(membership: membership, languageTypeID: languageTypeID, currency: Process.Membership.checkCurrencyOfMembership(membershipType: membershipType));
        }

        private static EComProductAPIOneLanguageWithPayHistory ToAPIOneLanguageWithHistory(this Git_system.Models.EComProduct content, Membership membership, int languageTypeID, string currency)
        {
            EComProductAPIOneLanguage rcontent = new EComProductAPIOneLanguageWithPayHistory();
            rcontent = content.ToAPIOneLanguage(membershipType: membership.MembershipRegisterTypeId, languageTypeID: languageTypeID, currency: currency);
            EComProductAPIOneLanguageWithPayHistory rcontentx = new EComProductAPIOneLanguageWithPayHistory(rcontent);

            if (membership.Id != 0)
            {
                if (content.EComOrderItems.Count() == 0)
                {
                    var orderItem = new Models.Database_MainEntities1().EComOrderItems.Where(o => o.EComProductId == content.Id).ToList();
                    if (orderItem.Count() != 0)
                        content.EComOrderItems = orderItem;
                }

                if (content.EComOrderItems.Count() != 0)
                {
                    var orderItem = content.EComOrderItems.ToList().ConvertAll(o => o.EComOrder).Where(o => o.MembershipId == membership.Id && o.PaymentProcessStatusId == 3).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (orderItem != null)
                        rcontentx.LastPay = orderItem.Datetime;
                    else
                        rcontentx.ElectronicFiles = new List<FileLink>();
                }
                else
                    rcontentx.ElectronicFiles = new List<FileLink>();
            }
            else
                rcontentx.ElectronicFiles = new List<FileLink>();

            return rcontentx;
        }
    }

    public class EComProductAPIOneLanguage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        public string Description { get; set; }

        public List<FileLink> ImageFiles { get; set; }

        public List<FileLink> OtherFiles { get; set; }

        public double PriceNormal { get; set; }

        public double Price { get; set; }

        public double PriceDeliver { get; set; }

        public string Currency { get; set; }

        //public bool ActiveStatus { get; set; }

        //public bool PinStatus { get; set; }

        //public Nullable<double> PinWeight { get; set; }

        public List<FileLink> ElectronicFiles { get; set; }

        public List<FileLink> DemoElectronicFiles { get; set; }

        public bool ElectronicFileStatus { get; set; }

        public bool DeliverStatus { get; set; }

        public List<EComCategoryAPI> Category { get; set; }

        public EComProductStock Stock { get; set; }
    }

    public class EComProductStock
    {
        public bool InStock { get; set; }

        public int Quantity { get; set; }
    }

    public class EComProductAPIOneLanguageWithPayHistory : EComProductAPIOneLanguage
    {
        public Nullable<System.DateTime> LastPay { get; set; }

        public EComProductAPIOneLanguageWithPayHistory()
        {
        }

        public EComProductAPIOneLanguageWithPayHistory(EComProductAPIOneLanguage parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public class PayHistory
    {
        public Nullable<System.DateTime> LastPay { get; set; }
    }
}