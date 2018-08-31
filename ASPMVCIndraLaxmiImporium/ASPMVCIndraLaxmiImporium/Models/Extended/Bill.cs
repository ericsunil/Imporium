using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(BillMetadata))]
    public partial class Bill
    {
    }
    public class BillMetadata
    {
        [DisplayName("Bill Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bill Number is Required")]
        public int BillNumber { get; set; }

        [DisplayName("S.N.")]
        public Nullable<int> SN { get; set; }

        [DisplayName("Debtor")]
        public Nullable<int> DebtorID { get; set; }

        [DisplayName("Transport")]
        public Nullable<int> TransportID { get; set; }

        [DisplayName("Credtor")]
        public Nullable<int> CredtorID { get; set; }

        [DisplayName("Product Name")]
        public Nullable<int> ProductID { get; set; }

        [DisplayName("Total Pair")]
        public Nullable<int> TotalPair { get; set; }

        [DisplayName("Total Amount")]
        public Nullable<decimal> TotalAmount { get; set; }


    }

}