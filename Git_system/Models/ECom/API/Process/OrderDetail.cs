using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom.API.Process
{
    public class OrderDetail
    {
        public double DeliverPrice { get; set; }
        public string DeliverCurrency { get; set; }
        public double ProductPrice { get; set; }
        public double ProductVat { get; set; }
        public double ProductTotalPrice { get; set; }
        public List<Promotion.PromotionDetail> PromotionDetail { get; set; }

        public double getPromotionPrice()
        {
            return this.PromotionDetail.Count() != 0 ? this.PromotionDetail.Sum(p => p.Price) : 0;
        }

        public double summaryOrderTotalPrice()
        {
            return this.ProductTotalPrice - this.getPromotionPrice() + this.DeliverPrice;
        }
    }
}