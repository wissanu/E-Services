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
    
    public partial class Geography
    {
        public Geography()
        {
            this.Provinces = new HashSet<Province>();
        }
    
        public int GEO_ID { get; set; }
        public string GEO_NAME { get; set; }
    
        public virtual ICollection<Province> Provinces { get; set; }
    }
}