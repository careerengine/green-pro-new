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
    
    public partial class WorkerGarage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkerGarage()
        {
            this.LeaderGarageDays = new HashSet<LeaderGarageDay>();
        }
    
        public int Id { get; set; }
        public string CrewLeaderId { get; set; }
        public int GarageID { get; set; }
        public bool IsLeader { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Garage Garage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaderGarageDay> LeaderGarageDays { get; set; }
    }
}
