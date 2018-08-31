using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(TransactionMainMetadata))]
    public partial class TransactionMain
    {
    }
    public class TransactionMainMetadata
    {
        [DisplayName("Bill Number")]
        public Nullable<int> BillNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0: MM/dd/yyyy")]
        public Nullable<System.DateTime> Date { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
    }
}