using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom.API.Process
{
    public class Promotion
    {
        #region Core Promotion

        private const int MembershipGuest = 1;
        private const int MembershipGuestInter = 2;
        private const int MembershipIndividual = 3;
        private const int MembershipIndividualInter = 4;
        private const int MembershipCorporate = 5;
        private const int MembershipCorporateInter = 6;

        /// <summary>
        /// Check Promotion is memberhisp
        /// </summary>
        /// <param name="eComPromotion">Promotion</param>
        /// <param name="membershipType">Membership Type Id</param>
        /// <returns>True False of Membership Type Id and Promotion</returns>
        private static bool checkPromotionForMembership(EComPromotion eComPromotion, int membershipType)
        {
            if (membershipType == MembershipGuest && eComPromotion.isGuest == true) return true;
            if (membershipType == MembershipGuestInter && eComPromotion.isGuestInter == true) return true;
            if (membershipType == MembershipIndividual && eComPromotion.isIndividual == true) return true;
            if (membershipType == MembershipIndividualInter && eComPromotion.isIndividualInter == true) return true;
            if (membershipType == MembershipCorporate && eComPromotion.isCorporate == true) return true;
            if (membershipType == MembershipCorporateInter && eComPromotion.isCorporateInter == true) return true;
            return false;
        }

        /// <summary>
        /// Check List of Promotion
        /// </summary>
        /// <param name="eComPromotions">List of Promotion</param>
        /// <param name="membershipType">Membership Type id</param>
        /// <returns>List of Promotion is correct membership type id</returns>
        private static List<EComPromotion> checkPromotionForMembership(List<EComPromotion> eComPromotions, int membershipType)
        {
            List<EComPromotion> promotionsForMembership = new List<EComPromotion>();
            foreach (EComPromotion eComPromotion in eComPromotions)
            {
                if (checkPromotionForMembership(eComPromotion, membershipType))
                    promotionsForMembership.Add(eComPromotion);
            }
            return promotionsForMembership;
        }

        /// <summary>
        /// find category of product and insert into order
        /// </summary>
        /// <param name="eComorderItem">item in order</param>
        /// <returns>item in order have category</returns>
        private static EComOrderItem mapCategory(EComOrderItem eComorderItem)
        {
            Database_MainEntities1 db = new Database_MainEntities1();

            if (eComorderItem.EComProduct == null)
                eComorderItem.EComProduct = db.EComProducts.Find(eComorderItem.EComProductId);
            return eComorderItem;
        }

        /// <summary>
        /// List orderitem insert with category of product
        /// </summary>
        /// <param name="eComorderItems">List of orderitem(item in order)</param>
        /// <returns>List of orderitem(item in order) is have category</returns>
        private static List<EComOrderItem> mapCategory(List<EComOrderItem> eComorderItems)
        {
            foreach (EComOrderItem eComorderItem in eComorderItems)
            {
                eComorderItem.EComProduct = mapCategory(eComorderItem).EComProduct;
            }
            return eComorderItems;
        }

        public class PromotionDetail : Git_system.Models.Form.ProductSKUOut { }

        /// <summary>
        /// merge between List of PromotionDetail and List of PromotionDetail
        /// </summary>
        /// <param name="contents">List of PromotionDetail</param>
        /// <param name="addContents">merge to List of PromotionDetail</param>
        /// <returns>List of PromotionDetail is merged</returns>
        private static List<PromotionDetail> promotionDetailsAdd(List<PromotionDetail> contents, List<PromotionDetail> addContents)
        {
            List<PromotionDetail> rcontents = contents;
            foreach (PromotionDetail addContent in addContents)
            {
                rcontents.Add(addContent);
            }
            return rcontents;
        }

        /// <summary>
        /// Check promotion of memberhip ,calculate discount and export to PromotionDetail
        /// </summary>
        /// <param name="eComOrderItems">List of OrderItem</param>
        /// <param name="membershipType">membership type id</param>
        /// <returns>PromotionDetail was calculated</returns>
        public static List<PromotionDetail> promotionDetailAll(List<EComOrderItem> eComOrderItems, int membershipType)
        {
            Database_MainEntities1 db = new Database_MainEntities1();

            List<EComPromotion> eComPromotions = db.EComPromotions.Where(p => p.ActiveStatus.Equals(true)).ToList();
            eComOrderItems = mapCategory(eComOrderItems);
            List<EComPromotion> eComPromotionsForMembership = checkPromotionForMembership(eComPromotions, membershipType);

            Rule.DiscountRule discountRule = new Rule.DiscountRule();
            List<PromotionDetail> promotionDetails = discountRule.CalculateDiscountPromotionDetails(eComPromotionsForMembership, eComOrderItems, membershipType);
            return promotionDetails;
        }

        #endregion Core Promotion

        /// <summary>
        /// All rule of promotion
        /// </summary>
        public class Rule
        {
            /// <summary>
            /// Convert EComPromotion to PromotionDetail
            /// </summary>
            /// <param name="eComPromotion">EComPromotion to be converted</param>
            /// <param name="price">The price of all items</param>
            /// <returns>PromotionDetail with price</returns>
            private static PromotionDetail convertPromotionDetailForm(EComPromotion eComPromotion, double price)
            {
                PromotionDetail promotionDetail = new PromotionDetail();
                promotionDetail.Id = eComPromotion.Id;
                promotionDetail.NameTH = eComPromotion.NameTH;
                promotionDetail.NameEN = eComPromotion.NameEN;
                promotionDetail.Price = price * (eComPromotion.Percent / 100);
                return promotionDetail;
            }

            /// <summary>
            /// Calculate the price of OrderItem Promotion and then Convert to PromotionDetail
            /// </summary>
            /// <param name="eComPromotion">EComPromotion to be converted</param>
            /// <param name="eComOrderItems">EComOrderItems to be calculated price</param>
            /// <param name="membershipType">Code number of membership type</param>
            /// <returns>PromotionDetail with price</returns>
            private static PromotionDetail calculatePromotion(EComPromotion eComPromotion, List<EComOrderItem> eComOrderItems, int membershipType)
            {
                PromotionDetail promotionDetail = new PromotionDetail();
                Price Price = new Price(new Database_MainEntities1());
                double price = Price.CheckPriceFormOrderItemsAndMembershipType(eComOrderItems, membershipType);
                promotionDetail = convertPromotionDetailForm(eComPromotion, price);

                return promotionDetail;
            }

            /// <summary>
            /// Interface of rule
            /// </summary>
            private interface IDiscountRule
            {
                List<PromotionDetail> CalculatePromotionDetails(List<EComPromotion> eComPromotions, List<EComOrderItem> eComOrderItems, int membershipType);
            }

            #region Rule

            /// <summary>
            ///  Calculate of discount membership
            /// </summary>
            public class MembershipTypeRule : IDiscountRule
            {
                /// <summary>
                /// Calculate Max of discount membership
                /// </summary>
                /// <param name="eComPromotions">List of Promotion</param>
                /// <param name="eComOrderItems">List of OrderItem</param>
                /// <param name="membershipType">Code number of membership</param>
                /// <returns>List of PromotionDetail with highest discount</returns>
                public List<PromotionDetail> CalculatePromotionDetails(List<EComPromotion> eComPromotions, List<EComOrderItem> eComOrderItems, int membershipType)
                {
                    PromotionDetail promotionDetail = new PromotionDetail();
                    promotionDetail.Price = 0;
                    foreach (EComPromotion eComPromotion in eComPromotions.Where(p => p.Type == 1))// Only select membership discount.
                    {
                        PromotionDetail promotionDetailFormcalculatePromotion = calculatePromotion(eComPromotion, eComOrderItems, membershipType);
                        if (promotionDetailFormcalculatePromotion.Price > promotionDetail.Price)
                            promotionDetail = promotionDetailFormcalculatePromotion;
                    }
                    List<PromotionDetail> promotionDetails = new List<PromotionDetail>();
                    if (promotionDetail.Price != 0)
                        promotionDetails.Add(promotionDetail);
                    return promotionDetails;
                }
            }

            #endregion Rule

            public class DiscountRule
            {
                /// <summary>
                /// List of rules
                /// </summary>
                private List<IDiscountRule> _rules = new List<IDiscountRule>();

                /// <summary>
                /// All rule
                /// </summary>
                public DiscountRule()
                {
                    _rules.Add(new MembershipTypeRule());
                }

                /// <summary>
                /// Calculate discount of all promotions and return List of PromotionDetail
                /// </summary>
                /// <param name="eComPromotions">all promotion</param>
                /// <param name="eComOrderItems">all orderitem</param>
                /// <param name="membershipType">Code number of membership</param>
                /// <returns>List of all PromotionDetail</returns>
                public List<PromotionDetail> CalculateDiscountPromotionDetails(List<EComPromotion> eComPromotions, List<EComOrderItem> eComOrderItems, int membershipType)
                {
                    List<PromotionDetail> promotionDetails = new List<PromotionDetail>();

                    foreach (IDiscountRule rule in _rules)
                    {
                        promotionDetails = promotionDetailsAdd(promotionDetails, rule.CalculatePromotionDetails(eComPromotions, eComOrderItems, membershipType));
                    }

                    return promotionDetails;
                }
            }
        }
    }
}