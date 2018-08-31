using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(LedgerMetadata))]
    public partial class Ledger
    {
    }
    public class LedgerMetadata
    {
        [DisplayName("Customer")]
        public Nullable<int> CustomerID { get; set; }

        [DisplayName("Customer Type")]
        public string Type { get; set; }

        [DisplayName("Ledger Name")]
        public string LedgerName { get; set; }
    }
}