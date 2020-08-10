using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom
{
    public static partial class Extension
    {
        private const int VatType_include = 1;
        private const int VatType_notInclude = 2;
        private const int VatType_noTex = 3;

        /// <summary>
        /// ส่งราคาคาตามประเภทสมาชิก
        /// </summary>
        /// <param name="eComProduct">สินค้าที่ต้องการทราบราคาตามประเภทสมาชิก</param>
        /// <param name="membershipType">ประเภทสมาชิก 1 - 6</param>
        /// <returns>ราคาของสินค้าตามประเภทสมาชิก</returns>
        [Obsolete("PriceForMembershipType is deprecated, please use toPriceForMembership instead.", false)]
        public static double PriceForMembershipType(this EComProduct eComProduct, int membershipType)
        {
            return getPriceForMembership(eComProduct, membershipType);
        }

        private static double getPriceForMembership(this EComProduct eComProduct, int membershipType)
        {
            double priceForMembership = 0;
            switch (membershipType)
            {
                case 1:
                    priceForMembership = eComProduct.Price;
                    break;

                case 2:
                    priceForMembership = eComProduct.PriceInter;
                    break;

                case 3:
                    priceForMembership = eComProduct.PriceIndividual;
                    break;

                case 4:
                    priceForMembership = eComProduct.PriceIndividualInter;
                    break;

                case 5:
                    priceForMembership = eComProduct.PriceCorporate;
                    break;

                case 6:
                    priceForMembership = eComProduct.PriceCorporateInter;
                    break;
            }
            return priceForMembership;
        }

        /// <summary>
        /// ส่งราคาคาตามประเภทสมาชิก
        /// </summary>
        /// <param name="eComProduct">สินค้าที่ต้องการทราบราคาตามประเภทสมาชิก</param>
        /// <param name="membershipType">ประเภทสมาชิก</param>
        /// <returns>ราคาของสินค้าตามประเภทสมาชิก</returns>
        private static double getPriceForMembership(this EComProduct eComProduct, Membership membership)
        {
            return getPriceForMembership(eComProduct, membership.MembershipRegisterTypeId);
        }

        /// <summary>
        /// Calculator product prcie
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="membership">membership</param>
        /// <returns>price of membership</returns>
        public static double toPriceForMembership(this EComProduct product, int membershipType)
        {
            var membership = new Membership();
            membership.MembershipRegisterTypeId = (short)membershipType;
            return product.toPriceForMembership(membership);
        }

        /// <summary>
        /// Calculator product prcie
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="membership">membership</param>
        /// <returns>price of membership</returns>
        public static double toPriceForMembership(this EComProduct product, Membership membership)
        {
            double price = 0;
            double setPrice = product.getPriceForMembership(membership);
            price = setPrice;
            if (product.VatTypeId == VatType_include)
            {
                double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
                price = setPrice / (1 + vat_setting / 100);
            }
            else if (product.VatTypeId == VatType_notInclude)
                price = setPrice;

            // Function calculator deliver and sub product price.
            var priceWithDeliver = new Func<EComProduct, Double, Double>(
                (ep, pr) => (ep.EComProductDeliver ?? new EComProductDeliver()).DeliverTypeId == 1 ? pr - ep.getDeliverPrice(membership.MembershipRegisterTypeId) : pr
            );

            return priceWithDeliver(product, price);
        }

        public static double toTotalPriceForMembership(this EComProduct product, Membership membership)
        {
            double price = 0;

            //double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
            var priceForMembership = product.toPriceForMembership(membership);
            var vatForMembership = product.toVatPriceForMembership(membership);
            price = priceForMembership + vatForMembership;

            return price;
        }

        public static double toVatPriceForMembership(this EComProduct product, Membership membership)
        {
            double price = 0;
            if (product.VatTypeId != VatType_noTex)
            {
                double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
                price = product.toPriceForMembership(membership) * (vat_setting / 100);
            }

            return price;
        }

        private static double getDeliverPrice(this EComProduct eComProduct, Int16 membershipType)
        {
            return (eComProduct.EComProductDeliver ?? new EComProductDeliver()).getDeliverPrice(membershipType);
        }

        private static double getDeliverPrice(this EComProductDeliver eComProductDeliver, Int16 membershipType)
        {
            var checkMembershipLocal = new Func<int, bool>(mt => Array.Exists(new int[] { 1, 3, 5 }, e => e == mt));

            return checkMembershipLocal((int)membershipType) ?
                (eComProductDeliver ?? new EComProductDeliver()).Price :
                (eComProductDeliver ?? new EComProductDeliver()).PriceInter;
        }

        public static double toDeliverPrice(this EComProduct eComProduct, Int16 membershipType)
        {
            return (eComProduct.EComProductDeliver ?? new EComProductDeliver()).DeliverTypeId == 3 ? 0 : eComProduct.EComProductDeliver.getDeliverPrice(membershipType);
        }

        public static double toDeliverPrice(this EComProduct eComProduct, Membership membership)
        {
            return eComProduct.toDeliverPrice(membership.MembershipRegisterTypeId);
        }

        /// <summary>
        /// Give price for group by membershipType.
        /// </summary>
        /// <param name="membershipType">number of membershipType</param>
        /// <returns>nullable of price</returns>
        private static Double? getPriceFromGroup(Int16 membershipType)
        {
            Func<int, bool> checkMembershipLocal = mt => Array.Exists(new int[] { 1, 3, 5 }, e => e == mt);

            Form.PriceFromGroup priceFromGroup = new Form.PriceFromGroup();
            if (checkMembershipLocal(membershipType))
                return priceFromGroup.THD;
            else
                return priceFromGroup.USD;
        }

        public static double toDeliverPrice(this List<EComOrderItem> eComOrderItems, Int16 membershipType)
        {
            const int priceByCategory = 4;
            const int priceByGroup = 5;
            Func<int, bool> checkCalPriceInProduct = deliverTypeId => Array.TrueForAll(new int[] { priceByCategory, priceByGroup }, e => e != deliverTypeId);

            var deliverPrice = eComOrderItems.Where(o => checkCalPriceInProduct(o.EComProduct.EComProductDeliver.DeliverTypeId)).Sum(o => (o.EComProduct.toDeliverPrice(membershipType) * o.Quantity));

            // Cal price by category if category active and category not show in menu ,It mean product not have categroy active.
            deliverPrice += eComOrderItems.Where(o => o.EComProduct.EComProductDeliver.DeliverTypeId == 4 && o.EComProduct.EComCategoryMaps.Where(c => (c.EComCategory.VisibleStatus && c.EComCategory.ActiveStatus)).Count() == 0).Sum(o => (o.EComProduct.toDeliverPrice(membershipType) * o.Quantity));

            // Check product must have category active and show in menu.
            var orderItemForDeliverPriceGroup = eComOrderItems.Where(o => o.EComProduct.EComProductDeliver.DeliverTypeId == 4 && o.EComProduct.EComCategoryMaps.Where(c => (c.EComCategory.VisibleStatus && c.EComCategory.ActiveStatus)).Count() > 0).ToList();

            // Cal price by category if category active and show in menu.
            deliverPrice += getDeliverPriceForOrderItem(orderItemForDeliverPriceGroup, membershipType);

            //getPriceFromGroup().h

            // Cal price by group.
            deliverPrice += eComOrderItems.Where(o => o.EComProduct.EComProductDeliver.DeliverTypeId == 5).Count() > 0 ? getPriceFromGroup(membershipType).GetValueOrDefault(0) : 0;

            return deliverPrice;
        }

        private static List<EComCategory> getCategoryForOrderItem(this List<EComOrderItem> eComOrderItems)
        {
            var listOfCategory = new List<EComCategory>();
            foreach (var oi in eComOrderItems)
            {
                foreach (var c in oi.EComProduct.EComCategoryMaps)
                {
                    if (c.EComCategory.VisibleStatus)
                    {
                        if (listOfCategory.Where(s => s.Id != c.EComCategoryId).Count() == 0)
                            listOfCategory.Add(c.EComCategory);
                        break;
                    }
                }
            }

            return listOfCategory;
        }

        private static double getDeliverPriceForOrderItem(this List<EComOrderItem> eComOrderItems, Int16 membershipType)
        {
            var membership = new Membership(membershipType);
            return getCategoryForOrderItem(eComOrderItems).Sum(c => membership.isLocal() ? c.Price : c.PriceInter);
        }

        public static double PriceNormalForMembershipType(this EComProduct eComProduct, Int16 membershipType)
        {
            var checkMembershipLocal = new Func<int, bool>(mt => Array.Exists(new Int16[] { 1, 3, 5 }, e => e == mt));

            var price = !checkMembershipLocal((Int16)membershipType) ? (double)eComProduct.PriceNormalInter : (double)eComProduct.PriceNormal;
            if (eComProduct.VatTypeId == VatType_notInclude)
            {
                double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
                price = price * (1 + (vat_setting / 100));
            }
            return price;
        }

        public static double PriceNormalForMembershipType(this EComProduct eComProduct, Membership membership)
        {
            return eComProduct.PriceNormalForMembershipType(membership.MembershipRegisterTypeId);
        }
    }
}