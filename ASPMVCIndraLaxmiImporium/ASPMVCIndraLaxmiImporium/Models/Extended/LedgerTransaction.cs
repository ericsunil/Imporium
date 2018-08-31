using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(LedgerTransactionMetadata))]
    public partial class LedgerTransaction
    {
    }

    public class LedgerTransactionMetadata
    {
        [DisplayName("Ledger")]
        public int LedgerID { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> Amount { get; set; }
        [DisplayName("Pay Leder")]
        public string PayLedger { get; set; }
    }
}