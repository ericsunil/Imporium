using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class TarrifChargeController : Controller
    {
        // GET: TarrifCharge
        public ActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(int BillNumber, int CustomerID, int PaidByID, string Description, DateTime Date, double Amount)
        {
            TransactionMain main = new TransactionMain() { TransactionMainID = 0, BillNumber = BillNumber, Description = Description, Date = Date, UserName = "Admin" };
            DBModel db = new DBModel();
            db.TransactionMains.Add(main);
            db.SaveChanges();
            DBModel db2 = new DBModel();
            if (PaidByID == 0)
            {
                db2.TransactionDetails.Add(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = main.TransactionMainID, LedgerNumber = "0", Description = Description, Debit = Amount, Credit = 0, CustomerID = CustomerID });
               

            }
            else {
                db2.TransactionDetails.Add(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = main.TransactionMainID, LedgerNumber = LedgerController.GetLedgerNumber(PaidByID), Description = Description, Debit = Amount, Credit = 0, CustomerID = CustomerID });
            }


                 db2.SaveChanges();
            DBModel db3 = new DBModel();
            db3.TransactionDetails.Add(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = main.TransactionMainID, LedgerNumber =LedgerController.GetLedgerNumber(CustomerID), Description = Description, Debit = 0, Credit = Amount, CustomerID = CustomerID });
            db3.SaveChanges();

            

            return RedirectToAction("Index");
        }

     [HttpGet]
        public ActionResult AddorEdit()
        {
            return View();
        }
    }
}