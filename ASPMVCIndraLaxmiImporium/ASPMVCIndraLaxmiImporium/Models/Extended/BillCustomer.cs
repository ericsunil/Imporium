using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(BillCustomerMetadata))]
    public partial class BillCustomer
    {
    }

    public class BillCustomerMetadata
    {
        [DisplayName("Debtor Name")]
        public Nullable<int> CustomerCode { get; set; }

        [DisplayName("Bill Number")]
        public Nullable<int> BillNumber { get; set; }

        [DisplayName("C.B.M.")]
        public string CBM { get; set; }
        [DisplayName("Transport Name")]
        public string TransportCode { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0: MM/dd/yyyy")]
        public Nullable<System.DateTime> Date { get; set; }

        [DisplayName("Total Amount")]
        public string Total { get; set; }
    }
}