using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
    }
    public class ProductMetadata
    {
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
    }
}