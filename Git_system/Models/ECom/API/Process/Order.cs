using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom.API.Process
{
    public class Order
    {
        private Database_MainEntities1 db { get; set; }

        public Order()
        {
            this.db = new Database_MainEntities1();
        }

        public Order(Database_MainEntities1 dataEntity)
        {
            this.db = dataEntity;
        }

        #region Main Order method

        /// <summary>
        /// ทำการเพิ่มรายการสินค้าที่สั่งซื้อ และคืนค้าการทำงาน
        /// </summary>
        /// <param name="eComOrder">รับค่ารายการสั่งซื้อ</param>
        /// <param name="eComOrderItems">รับค่ารายการสินค้าที่สั่งซื้อ</param>
        /// <returns>dataAndStatus โดยมี data เป็น EComOrder</returns>
        public DataAndStatus AddOrder(EComOrder eComOrder, List<EComOrderItem> eComOrderItems)
        {
            //Database_MainEntities1 db = new Database_MainEntities1();
            DataAndStatus dataAndStatus = new DataAndStatus();
            try
            {
                eComOrder.Membership = null;
                this.db.EComOrders.Add(eComOrder);

                /// เพิ่ม orderitem
                try
                {
                    foreach (EComOrderItem eComOrderItem in eComOrderItems)
                    {
                        eComOrderItem.EComOrderId = eComOrder.Id;
                        this.db.Entry(eComOrderItem.EComProduct).State = System.Data.Entity.EntityState.Unchanged;
                        this.db.Entry(eComOrderItem.EComProduct.EComProductDeliver).State = System.Data.Entity.EntityState.Unchanged;
                        this.db.EComOrderItems.Add(eComOrderItem);
                    }
                    this.db.SaveChanges();
                    dataAndStatus = new DataAndStatus(1, eComOrder);
                }
                catch
                {
                    List<EComOrderItem> eComOrderItemsDelete = this.db.EComOrderItems.Where(e => e.EComOrderId == eComOrder.Id).ToList();
                    if (eComOrderItemsDelete.Count > 0)
                    {
                        foreach (EComOrderItem eComOrderItemDelete in eComOrderItemsDelete)
                        {
                            this.db.EComOrderItems.Remove(eComOrderItemDelete);
                            this.db.SaveChanges();
                        }
                    }
                    this.db.EComOrders.Remove(eComOrder);
                    this.db.SaveChanges();
                    dataAndStatus = new DataAndStatus(103, null);
                }
            }
            catch
            {
                dataAndStatus = new DataAndStatus(102, null);
            }

            return dataAndStatus;
        }

        /// <summary>
        /// ทำการตรวจสอบรหัสสินค้า กับ รหัสสินค้าที่มีอยู่ และ รหัสสินค้าที่มีสถานะพร้อม
        /// </summary>
        /// <param name="eComOrderItems">สินค้าที่ทำการสั่งซื้อ</param>
        /// <returns>ค่าเมื่อตรวจสอบแล้วว่าสินค้ามีอยู่หรือไม่ True สินค้าที่ทำการสั่งซื้อถูกต้อง False สินค้าไม่ถูกต้อง</returns>
        public bool CheckOrderItems(List<EComOrderItem> eComOrderItems)
        {
            //int[] productItems = db.EComProducts.Where(p => p.ActiveStatus == true).Select(p => p.Id).ToArray();
            int[] productItems = this.db.EComProducts.Select(p => p.Id).ToArray();
            int[] eComOrderProductItems = eComOrderItems.Select(o => o.EComProductId).ToArray();
            int[] sameProductId = eComOrderProductItems.Intersect(productItems).ToArray();
            return (Enumerable.SequenceEqual((sameProductId), (eComOrderProductItems)));
        }

        #endregion Main Order method

        #region Order detail

        public OrderDetail checkOrderDetail(List<EComOrderItem> eComOrderItems, int membershipType)
        {
            var orderDetail = new OrderDetail();
            foreach (var orderItem in eComOrderItems)
            {
                orderItem.EComProduct = this.db.EComProducts.Find(orderItem.EComProductId);
            }
            DeliverPromotion deliverPromotion = new DeliverPromotion();
            deliverPromotion.calculateDeliverPromotion(eComOrderItems, new Models.Membership((short)membershipType));

            var deliverPrice = eComOrderItems.toDeliverPrice((short)membershipType);
            orderDetail.DeliverPrice = deliverPrice;
            orderDetail.DeliverCurrency = Membership.checkCurrencyOfMembership((short)membershipType);

            var membership = new Models.Membership();
            membership.MembershipRegisterTypeId = (short)membershipType;
            orderDetail.ProductPrice = eComOrderItems.Sum(o => o.EComProduct.toPriceForMembership(membership) * o.Quantity);
            orderDetail.ProductVat = eComOrderItems.Sum(o => o.EComProduct.toVatPriceForMembership(membership) * o.Quantity);
            orderDetail.ProductTotalPrice = eComOrderItems.Sum(o => o.EComProduct.toTotalPriceForMembership(membership) * o.Quantity);
            orderDetail.PromotionDetail = Promotion.promotionDetailAll(eComOrderItems, membershipType);
            return orderDetail;
        }

        #endregion Order detail
    }
}