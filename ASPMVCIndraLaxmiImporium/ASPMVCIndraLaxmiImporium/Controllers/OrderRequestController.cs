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
                return db.BillCustomers.ToList<BillCustomer>();
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
        public ActionResult SaveCustomerDetail(BillCustomer cust)
        {
            TransactionMain main = new TransactionMain() { TransactionMainID = 0, BillNumber = cust.BillNumber, Description = "Goods dispatch to " + cust.CustomerCode, Date = cust.Date, UserName = "Admin" };

            DBModel db = new DBModel();
            db.TransactionMains.Add(main);
            db.SaveChanges();

            //for TransactionDetail
            DBModel db2 = new DBModel();

             db2.TransactionDetails.Add(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = main.TransactionMainID, LedgerNumber = LedgerController.GetLedgerNumber((int)cust.CustomerCode), Description ="Goods dispatch by "+ cust.BillNumber , Debit = 0, Credit = Convert.ToDouble(cust.Total), CustomerID = (int)cust.CustomerCode });
            db2.SaveChanges();

            DBModel db3 = new DBModel();
            db3.Entry(cust).State = EntityState.Modified;
            db3.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult GetCreditAmount( Bill cdt)
        {

            return RedirectToAction("Index");
        }


    }
}