using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllCustomer());
        }

        IEnumerable<Customer> GetAllCustomer()
        {
            using (DBModel db = new DBModel())
            {
                return db.Customers.ToList<Customer>();
            }
        }

        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            Customer emp = new Customer();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault<Customer>();
                }
            }
            return View(emp);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(Customer emp)
        {
            DBModel dbledgercheck = new DBModel();
            if(dbledgercheck.Ledgers.ToList().Count == 0)
            {
                Ledger ledgerCash = new Ledger() { LedgerID = 0, LedgerName = "Cash", Type ="System", CustomerID = emp.CustomerID };
                Ledger ledgerCR = new Ledger() { LedgerID = 0, LedgerName = "Cash Receivable", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledgerCP = new Ledger() { LedgerID = 0, LedgerName = "Cash Payable", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledgerCommission = new Ledger() { LedgerID = 0, LedgerName = "Commission", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger1 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger2 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger3 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger4 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger5 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger6 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger7 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger8 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger9 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                Ledger ledger10 = new Ledger() { LedgerID = 0, LedgerName = "Default", Type = "System", CustomerID = emp.CustomerID };
                dbledgercheck.Ledgers.Add(ledgerCash);
                dbledgercheck.Ledgers.Add(ledgerCR);
                dbledgercheck.Ledgers.Add(ledgerCP);
                dbledgercheck.Ledgers.Add(ledgerCommission);
                dbledgercheck.Ledgers.Add(ledger1);
                dbledgercheck.Ledgers.Add(ledger2);
                dbledgercheck.Ledgers.Add(ledger3);
                dbledgercheck.Ledgers.Add(ledger4);
                dbledgercheck.Ledgers.Add(ledger5);
                dbledgercheck.Ledgers.Add(ledger6);
                dbledgercheck.Ledgers.Add(ledger7);
                dbledgercheck.Ledgers.Add(ledger8);
                dbledgercheck.Ledgers.Add(ledger9);
                dbledgercheck.Ledgers.Add(ledger10);
                dbledgercheck.SaveChanges();

            }
            
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.CustomerID == 0)
                    {
                        string payCode = LedgerController.GetLedgerNumberByName("Cash Receivable").ToString();
                        decimal Debit = 0;
                        decimal Credit = Math.Abs(Convert.ToDecimal(emp.PreviousBalance));
                      

                        if (Convert.ToDecimal(emp.PreviousBalance) < 0)
                        {
                            payCode = LedgerController.GetLedgerNumberByName("Cash Payable").ToString();
                            Debit = Math.Abs(Convert.ToDecimal(emp.PreviousBalance));
                            Credit = 0;

                        }
                        db.Customers.Add(emp);
                        
                        emp.PreviousBalance = Math.Abs(Convert.ToDecimal(emp.PreviousBalance)).ToString();
                        
                        db.SaveChanges();
                        Ledger ledger = new Ledger() { LedgerID = 0, LedgerName = "Person", Type = emp.Type, CustomerID = emp.CustomerID };
                        DBModel dbLedger = new DBModel();
                        dbLedger.Ledgers.Add(ledger);
                        dbLedger.SaveChanges();



                        LedgerTransaction ledgettransaction = new LedgerTransaction()
                        {
                            LedgerTransactionID = 0,
                            LedgerID = ledger.LedgerID,
                            Debit = Debit,
                            Credit = Credit,
                            Balance = Math.Abs(Convert.ToDecimal(emp.PreviousBalance)),
                            Amount = 0,
                            PayLedger = payCode
                        };
                        TransactionMain tmain = new TransactionMain()
                        {
                            TransactionMainID = 0,
                            BillNumber = 0,
                            Description = "Opening Balance",
                            Date = DateTime.Now,
                            UserName = "Admin"

                        };
                        

                        DBModel db1 = new DBModel();
                       
                       
                    
                        db1.TransactionMains.Add(tmain);
                        db1.SaveChanges();

                        DBModel db2 = new DBModel();
                        db2.LedgerTransactions.Add(ledgettransaction);
                        TransactionDetail tdetail = new TransactionDetail()
                        {
                            TransactionDetailID=0,
                            TransactionMainID=tmain.TransactionMainID,
                            LedgerNumber =ledger.LedgerID.ToString(),
                            Description ="Opening",
                            Debit = (double) Debit,
                            Credit = (double)Credit,
                            CustomerID = emp.CustomerID
                        };
                        db2.TransactionDetails.Add(tdetail);
                        db2.SaveChanges();

                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCustomer()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Customer emp = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault<Customer>();
                    db.Customers.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCustomer()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}