//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASPMVCIndraLaxmiImporium.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionDetail
    {
        public int TransactionDetailID { get; set; }
        public Nullable<int> TransactionMainID { get; set; }
        public string LedgerNumber { get; set; }
        public string Description { get; set; }
        public Nullable<double> Debit { get; set; }
        public Nullable<double> Credit { get; set; }
        public int CustomerID { get; set; }
    
        public virtual TransactionMain TransactionMain { get; set; }
    }
}
