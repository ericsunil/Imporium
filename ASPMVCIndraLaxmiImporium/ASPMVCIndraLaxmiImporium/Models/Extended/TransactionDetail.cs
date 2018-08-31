using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(TransactionDetailMetadata))]
    public partial class TransactionDetail
    {
    }

    public class TransactionDetailMetadata
    {
        [DisplayName("Transaction Main")]
        public Nullable<int> TransactionMainID { get; set; }

        [DisplayName("Ledger Number")]
        public string LedgerNumber { get; set; }

        [DisplayName("Customer")]
        public int CustomerID { get; set; }
    }
}