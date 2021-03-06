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
    
    public partial class Product
    {
        public Product()
        {
            this.Group = 1;
            this.Active = false;
            this.ProductSKUActive = true;
            this.isCreditCard = true;
            this.isShortTerm = false;
            this.isLongTerm = false;
            this.VatTypeId = 2;
            this.Courses = new HashSet<Course>();
        }
    
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Name")]
        public string TitleTH { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Name")]
        public string TitleEN { get; set; }
        public string DetailTH { get; set; }
        public string DetailEN { get; set; }
        public string LocationTH { get; set; }
        public string LocationEN { get; set; }
        public Nullable<short> Limit { get; set; }
        public bool SupportTH { get; set; }
        public bool SupportEN { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double Price { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceInter { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceCorporate { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceIndividual { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceCorporateInter { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double PriceIndividualInter { get; set; }
        public short Group { get; set; }
        public bool Active { get; set; }
        public bool ProductSKUActive { get; set; }
        public bool isCreditCard { get; set; }
        public bool isShortTerm { get; set; }
        public bool isLongTerm { get; set; }
        public int VatTypeId { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }
        public virtual VatType VatType { get; set; }
    }
}
