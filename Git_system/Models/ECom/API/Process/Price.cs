using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom.API.Process
{
    public class Price
    {
        private Database_MainEntities1 db { get; set; }

        public Price()
        {
            this.db = new Database_MainEntities1();
        }

        public Price(Database_MainEntities1 dataEntity)
        {
            this.db = dataEntity;
        }

        /// <summary>
        /// Calculate price of all order with discount
        /// </summary>
        /// <param name="eComOrderItems">List of OrderItem</param>
        /// <param name="membershipType">Code number of membership</param>
        /// <returns>price of all order with discount</returns>
        public double CheckPriceForMembershipTypeAndPromotionPrice(List<EComOrderItem> eComOrderItems, int membershipType)
        {
            double price = 0;
            double priceMembershipType = CheckPriceForMembershipType(eComOrderItems, membershipType);
            double priceMembershipTypeAndPromotion = CheckPriceForMembershipTypeAndPromotion(eComOrderItems, membershipType);
            price = priceMembershipType - priceMembershipTypeAndPromotion;
            return price;
        }

        /// <summary>
        /// Calculate price of all Order
        /// </summary>
        /// <param name="eComOrderItems">List of OrderItem</param>
        /// <param name="membershipType">Code number of membership</param>
        /// <returns>price of all order</returns>
        public double CheckPriceForMembershipType(List<EComOrderItem> eComOrderItems, int membershipType)
        {
            return CheckPriceFormOrderItemsAndMembershipType(eComOrderItems, membershipType);
        }

        /// <summary>
        /// Calculate price of order and membership
        /// </summary>
        /// <param name="eComOrderItems">List of OrderItem</param>
        /// <param name="membershipType">Code number of membership</param>
        /// <returns>price of order</returns>
        public double CheckPriceFormOrderItemsAndMembershipType(List<EComOrderItem> eComOrderItems, int membershipType)
        {
            double price = 0;
            foreach (EComOrderItem eComOrderItem in eComOrderItems)
            {
                price += (db.EComProducts.Find(eComOrderItem.EComProductId).toPriceForMembership(membershipType) * eComOrderItem.Quantity);
            }
            return price;
        }

        /// <summary>
        /// Calculate price of all discount
        /// </summary>
        /// <param name="eComOrderItems">List of OrderItem</param>
        /// <param name="membershipType">Code number of membership</param>
        /// <returns>price of all discount</returns>
        public double CheckPriceForMembershipTypeAndPromotion(List<EComOrderItem> eComOrderItems, int membershipType)
        {
            double price = 0;
            List<Promotion.PromotionDetail> promotionDetails = new List<Promotion.PromotionDetail>();
            promotionDetails = Promotion.promotionDetailAll(eComOrderItems, membershipType);
            price = promotionDetails.Sum(p => p.Price);
            return price;
        }
    }
}