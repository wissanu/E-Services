//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Git_system.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EComDeliverPromotionMap
    {
        public int Id { get; set; }
        public int EComDeliverPromotionId { get; set; }
        public int EComProductId { get; set; }
    
        public virtual EComDeliverPromotion EComDeliverPromotion { get; set; }
        public virtual EComProduct EComProduct { get; set; }
    }
}
