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
    
    public partial class GarrageWeekday
    {
        public int Id { get; set; }
        public int GarrageId { get; set; }
        public int WeekdayId { get; set; }
    
        public virtual Garage Garage { get; set; }
        public virtual Weekday Weekday { get; set; }
    }
}
