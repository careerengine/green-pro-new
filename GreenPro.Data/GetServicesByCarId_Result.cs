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
    
    public partial class GetServicesByCarId_Result
    {
        public int ServiceID { get; set; }
        public string Service_Name { get; set; }
        public string Service_Description { get; set; }
        public decimal Service_Price { get; set; }
        public bool IsAddOn { get; set; }
        public System.DateTime CreateDt { get; set; }
        public string CreatedBy { get; set; }
    }
}