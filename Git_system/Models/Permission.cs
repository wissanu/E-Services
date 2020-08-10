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
    
    public partial class Permission
    {
        public Permission()
        {
            this.isAdmin = false;
            this.isUser = false;
            this.isProduct = false;
            this.isPayment = false;
            this.isLog = false;
            this.isESlide = false;
            this.isEProduct = false;
            this.isEPayView = false;
            this.isEPayConfirm = false;
            this.isEDeliver = false;
            this.isEPromotion = false;
            this.isEStatistic = false;
            this.Users = new HashSet<UserAdmin>();
        }
    
        public int permissionId { get; set; }
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public string Name { get; set; }
        public string Detail { get; set; }
        public bool isAdmin { get; set; }
        public bool isUser { get; set; }
        public bool isProduct { get; set; }
        public bool isPayment { get; set; }
        public bool isLog { get; set; }
        public bool isESlide { get; set; }
        public bool isEProduct { get; set; }
        public bool isEStock { get; set; }
        public bool isEPayView { get; set; }
        public bool isEPayConfirm { get; set; }
        public bool isEDeliver { get; set; }
        public bool isEPromotion { get; set; }
        public bool isEStatistic { get; set; }
    
        public virtual ICollection<UserAdmin> Users { get; set; }
    }
}
