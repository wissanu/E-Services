using System.Collections.Generic;

namespace Git_system.Models.ECom.API
{
    public class EComDeliverTypeAPI
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }
    }

    public static partial class ExtensionMethod
    {
        /// <summary>
        /// Converter List of EComDeliverType entity to List of EComDeliverTypeAPI
        /// </summary>
        /// <param name="content">List of EComDeliverType will be convert</param>
        /// <param name="languageTypeID">Number of language: 1 th , 2 en</param>
        /// <param name="membershipType">Number of MembershipType</param>
        /// <returns>List of EComDeliverTypeAPI</returns>
        public static List<EComDeliverTypeAPI> ToAPI(this List<EComDeliverType> contents, int languageTypeID, int membershipType)
        {
            List<EComDeliverTypeAPI> rContents = new List<EComDeliverTypeAPI>();
            foreach (EComDeliverType content in contents)
            {
                rContents.Add(content.ToAPI(membershipType: membershipType, languageTypeID: languageTypeID));
            }
            return rContents;
        }

        /// <summary>
        /// Converter EComDeliverType entity to EComDeliverTypeAPI
        /// </summary>
        /// <param name="content">EComDeliverType will be convert</param>
        /// <param name="languageTypeID">Number of language: 1 th , 2 en</param>
        /// <param name="membershipType">Number of MembershipType</param>
        /// <returns>EcomDeliverTypeAPI</returns>
        public static EComDeliverTypeAPI ToAPI(this EComDeliverType content, int languageTypeID, int membershipType)
        {
            EComDeliverTypeAPI rContent = new EComDeliverTypeAPI();
            rContent.Id = content.Id;
            rContent.Name = languageTypeID == 1 ? content.NameTH : content.NameEN;
            rContent.Detail = languageTypeID == 1 ? content.DetailTH : content.DetailEN;
            rContent.Price = (membershipType == 1 || membershipType == 3 || membershipType == 5) ? content.PriceTH : content.PriceUS;
            rContent.Currency = Process.Membership.checkCurrencyOfMembership(membershipType: membershipType);
            return rContent;
        }
    }
}