using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }
    public class CustomerMetadata
    {
        [DisplayName("Customer Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Name is Required")]
        public string CustomerName { get; set; }

        [DisplayName("Previous Balance")]
        public string PreviousBalance { get; set; }
    }

}