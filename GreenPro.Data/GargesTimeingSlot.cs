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
    
    public partial class GargesTimeingSlot
    {
        public GargesTimeingSlot()
        {
            this.UserPackages = new HashSet<UserPackage>();
        }
    
        public int Id { get; set; }
        public string SlotTimeing { get; set; }
        public bool IsActive { get; set; }
        public int GarageId { get; set; }
    
        public virtual ICollection<UserPackage> UserPackages { get; set; }
        public virtual Garage Garage { get; set; }
    }
}
