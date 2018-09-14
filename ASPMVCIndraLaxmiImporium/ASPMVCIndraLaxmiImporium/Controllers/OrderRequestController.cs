using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class OrderRequestController : Controller
    {
        // GET: OrderRequest
        public ActionResult Index()
        {
            return View(GetAllOrderRequest());
        }

        IEnumerable<BillCustomer> GetAllOrderRequest()
        {
            using (DBModel db = new DBModel())
            {
                return db.BillCustomers.Where(b=>b.Ispaid != true).ToList<BillCustomer>();
            }
        }

        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            BillCustomer emp = new BillCustomer();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.BillCustomers.Where(x => x.BillCustomerID == id).FirstOrDefault<BillCustomer>();
                }
            }
            return View(emp);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(Bill emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if(emp.BillID != 0)
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("AddorEdit");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllBillCustomer()), message = "Save Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveCustomerDetail(BillCustomer cust, int TotalAmount, int CustomerName)
        {
            TransactionMain main = new TransactionMain() { TransactionMainID = 0, BillNumber = cust.BillNumber, Description = "Goods dispatch to " + cust.CustomerCode, Date = cust.Date, UserName = "Admin" };

            DBModel db = new DBModel();
            db.TransactionMains.Add(main);
            db.SaveChanges();

            //for TransactionDetail
            DBModel db2 = new DBModel();

            db2.TransactionDetails.Add(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = main.TransactionMainID, LedgerNumber =CustomerName.ToString(), Description = "Goods dispatch by " + cust.BillNumber, Debit = 0, Credit = Convert.ToDouble(cust.Total), CustomerID = 0 });
            db2.SaveChanges();

            DBModel db3 = new DBModel();

            db3.Entry(cust).State = EntityState.Modified;
            db3.SaveChanges();

            DBModel db5 = new DBModel();
            BillCustomer b = db5.BillCustomers.SingleOrDefault(be => be.BillNumber == cust.BillNumber);
            b.Ispaid = true;
            db5.Entry(b).State = EntityState.Modified;
            db5.SaveChanges();

            DBModel db4 = new DBModel();

            db4.TransactionDetails.Add(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = main.TransactionMainID, LedgerNumber = CustomerName.ToString(), Description = "Paid for Goods " + cust.BillNumber, Debit = TotalAmount, Credit = 0, CustomerID = 0 });
            db4.SaveChanges();
            // return Content(CustomerName.ToString());

            return RedirectToAction("Index");
        }

        public ActionResult GetCreditAmount( int id)
        {
            using (DBModel db = new DBModel())
            {
                List<TransactionDetail> crd = db.TransactionDetails.Where(x => x.LedgerNumber == id.ToString()).ToList<TransactionDetail>();
                int credit=  Convert.ToInt32(crd.Sum(d => d.Credit)); 
                int debit = Convert.ToInt32(crd.Sum(d=>d.Debit));
               
                return Json(credit - debit, JsonRequestBehavior.AllowGet);
            }           
        }

        public ActionResult GetCustomer(String id = "")
        {
            //String[] type = { "Debtor", "Creditor", "Transport" };
            //String CustomerType = id;

            //DBModel db = new DBModel();
            //var balance = (from aa in db.Customers
            //               join c in db.Ledgers on aa.CustomerID equals c.CustomerID into lg
            //               from x in lg.DefaultIfEmpty()
            //               select new
            //               {
            //                   aa.CustomerName,
            //                   aa.Type,
            //                   x.LedgerID,
            //                   x.LedgerName
            //               }).ToList();

        


            List<CustomerLedger> objcity = new List<CustomerLedger>();

            SelectList obgcity = new SelectList(new DBModel().ViewCustomerLedgers.Where(x=>x.Type== id).ToList<ViewCustomerLedger>(), "LedgerNumber", "CustomerName", 0);


            return Json(obgcity, JsonRequestBehavior.AllowGet);
            //return Content(a);

        }
    }
}