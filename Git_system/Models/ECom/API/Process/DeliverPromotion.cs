using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom.API.Process
{
    public class DeliverPromotion
    {
        private const int MembershipGuest = 1;
        private const int MembershipGuestInter = 2;
        private const int MembershipIndividual = 3;
        private const int MembershipIndividualInter = 4;
        private const int MembershipCorporate = 5;
        private const int MembershipCorporateInter = 6;

        private Database_MainEntities1 db { get; set; }

        public DeliverPromotion()
        {
            this.db = new Database_MainEntities1();
        }

        public DeliverPromotion(Database_MainEntities1 dataEntity)
        {
            this.db = dataEntity;
        }

        private EComOrderItem setDiscountDeliverPrice(EComOrderItem orderItem, double discount)
        {
            if (orderItem.EComProduct.EComProductDeliver == null)
                orderItem.EComProduct.EComProductDeliver = new EComProductDeliver();
            else
            {
                orderItem.EComProduct.EComProductDeliver.Price = orderItem.EComProduct.EComProductDeliver.Price * (1 - discount / 100);
                orderItem.EComProduct.EComProductDeliver.PriceInter = orderItem.EComProduct.EComProductDeliver.PriceInter * (1 - discount / 100);
            }
            return orderItem;
        }

        public List<EComOrderItem> calculateDeliverPromotion(List<EComOrderItem> eComOrderItems, Models.Membership membership)
        {
            List<EComOrderItem> fullOrderItems = eComOrderItems;
            List<EComDeliverPromotion> allDeliverPromotions = this.db.EComDeliverPromotions.Where(e => e.ActiveStatus == true).ToList().Where(e => checkPromotionDeliverForMembership(e, membership.MembershipRegisterTypeId)).ToList();

            double totalPrice = Convert.ToDouble(eComOrderItems.Sum(o => o.EComProduct.toTotalPriceForMembership(membership) * o.Quantity).ToString("0.000000"));
            foreach (EComDeliverPromotion deliverPromotion in allDeliverPromotions.OrderBy(p => p.Type))
            {
                if (deliverPromotion.Type == 1)
                {
                    double operatorPrice = Convert.ToDouble(deliverPromotion.Operator.GetValueOrDefault(0).ToString("0.000000"));

                    if (!membership.isLocal())
                        operatorPrice = Convert.ToDouble(deliverPromotion.Operator2.GetValueOrDefault(0).ToString("0.000000"));

                    if (operatorPrice <= totalPrice)
                        eComOrderItems.ForEach(e => setDiscountDeliverPrice(e, deliverPromotion.Percent));
                    //eComOrderItems.Clear();
                }
            }
            return fullOrderItems;
        }

        /// <summary>
        /// Check Promotion is memberhisp
        /// </summary>
        /// <param name="eComDeliverPromotion">Promotion</param>
        /// <param name="membershipType">Membership Type Id</param>
        /// <returns>True False of Membership Type Id and Promotion</returns>
        private static bool checkPromotionDeliverForMembership(EComDeliverPromotion eComDeliverPromotion, int membershipType)
        {
            if (membershipType == MembershipGuest && eComDeliverPromotion.isGuest == true) return true;
            if (membershipType == MembershipGuestInter && eComDeliverPromotion.isGuestInter == true) return true;
            if (membershipType == MembershipIndividual && eComDeliverPromotion.isIndividual == true) return true;
            if (membershipType == MembershipIndividualInter && eComDeliverPromotion.isIndividualInter == true) return true;
            if (membershipType == MembershipCorporate && eComDeliverPromotion.isCorporate == true) return true;
            if (membershipType == MembershipCorporateInter && eComDeliverPromotion.isCorparateInter == true) return true;
            return false;
        }
    }
}