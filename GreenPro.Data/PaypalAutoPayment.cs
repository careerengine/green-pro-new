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
    
    public partial class PaypalAutoPayment
    {
        public int Id { get; set; }
        public string TrasactionID { get; set; }
        public string ReferenceID { get; set; }
        public string PaymentStatus { get; set; }
        public string PendingReason { get; set; }
        public string PaymentDate { get; set; }
        public string GrossAmount { get; set; }
        public Nullable<int> UserPackageID { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string BillingAggrementID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual UserPackage UserPackage { get; set; }
    }
}
