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

    public partial class EComOrderItem : ICloneable
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int EComProductId { get; set; }
        public int EComOrderId { get; set; }
    
        public virtual EComProduct EComProduct { get; set; }
        public virtual EComOrder EComOrder { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}