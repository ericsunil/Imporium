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
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.BillCustomers = new HashSet<BillCustomer>();
        }
   /*sdfdf*/        public int BillID { get; set; }
        public int BillNumber { get; set; }
        public Nullable<int> SN { get; set; }
        public Nullable<int> DebtorID { get; set; }
        public Nullable<int> TransportID { get; set; }
        public Nullable<int> CredtorID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Orderded { get; set; }
        public Nullable<int> Pair { get; set; }
        public Nullable<int> TotalPair { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<bool> IsCommit { get; set; }
        public Nullable<bool> Ispaid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillCustomer> BillCustomers { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Customer Customer1 { get; set; }
        public virtual Customer Customer2 { get; set; }
        public virtual Product Product { get; set; }
    }
}
