//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenPro.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPackagesAddon
    {
        public int Id { get; set; }
        public int UserPackageID { get; set; }
        public int ServiceID { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<System.DateTime> NextServiceDate { get; set; }
    
        public virtual Service Service { get; set; }
        public virtual UserPackage UserPackage { get; set; }
    }
}
