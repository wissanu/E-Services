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
    
    public partial class EComProduct
    {
        public EComProduct()
        {
            this.ActiveStatus = false;
            this.PinStatus = false;
            this.PinWeight = 0D;
            this.ElectronicFileStatus = false;
            this.DeliverStatus = true;
            this.Quantity = 0;
            this.InStock = true;
            this.Vat = 0m;
            this.PriceNormal = 0D;
            this.PriceNormalInter = 0D;
            this.VatTypeId = 2;
            this.EComCategoryMaps = new HashSet<EComCategoryMap>();
            this.EComOrderItems = new HashSet<EComOrderItem>();
            this.EComPromotions = new HashSet<EComPromotion>();
            this.EComLogViews = new HashSet<EComLogView>();
            this.EComDeliverPromotionMaps = new HashSet<EComDeliverPromotionMap>();
        }
    
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public string NameTH { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public string NameEN { get; set; }

        public string DetailTH { get; set; }
        public string DetailEN { get; set; }
        public string ImageFiles { get; set; }
        public string OtherFiles { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceInter { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceIndividual { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceIndividualInter { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceCorporate { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceCorporateInter { get; set; }

        public bool ActiveStatus { get; set; }
        public bool PinStatus { get; set; }
        public Nullable<double> PinWeight { get; set; }
        public string ElectronicFiles { get; set; }
        public bool ElectronicFileStatus { get; set; }
        public bool DeliverStatus { get; set; }
        public int Quantity { get; set; }
        public bool InStock { get; set; }
        public decimal Vat { get; set; }
        public string DescriptionTH { get; set; }
        public string DescriptionEN { get; set; }
        public Nullable<double> PriceNormal { get; set; }
        public Nullable<double> PriceNormalInter { get; set; }
        public int VatTypeId { get; set; }
        public string DemoElectronicFiles { get; set; }
    
        public virtual ICollection<EComCategoryMap> EComCategoryMaps { get; set; }
        public virtual ICollection<EComOrderItem> EComOrderItems { get; set; }
        public virtual ICollection<EComPromotion> EComPromotions { get; set; }
        public virtual ICollection<EComLogView> EComLogViews { get; set; }
        public virtual VatType VatType { get; set; }
        public virtual EComProductDeliver EComProductDeliver { get; set; }
        public virtual ICollection<EComDeliverPromotionMap> EComDeliverPromotionMaps { get; set; }
    }
}
