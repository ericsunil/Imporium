
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ASPMVCIndraLaxmiImporium.Models
{
    public class Context : DbContext
    {

        public Context() : base("BillCustomer")
        {

        }

        public IDbSet<BillCustomer> CustomerSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new BillCustomerConfiguration());
         
        }
    }
}