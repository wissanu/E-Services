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
    using System.ComponentModel.DataAnnotations;
    
    public partial class EComProductDeliver
    {
        public EComProductDeliver()
        {
            this.Price = 0D;
            this.PriceInter = 0D;
            this.DeliverTypeId = 2;
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceInter { get; set; }

        public int DeliverTypeId { get; set; }
    
        public virtual DeliverType DeliverType { get; set; }
        public virtual EComProduct EComProduct { get; set; }
    }
}
