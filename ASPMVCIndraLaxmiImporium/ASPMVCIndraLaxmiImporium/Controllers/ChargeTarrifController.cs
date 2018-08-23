using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class ChargeTarrifController : Controller
    {
        // GET: ChargeTarrif
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAll()
        {
            return View(GetAllChargeTarrif());
        }

        IEnumerable<TransactionMain> GetAllChargeTarrif()
        {
            using (DBModel db = new DBModel())
            {
                return db.TransactionMains.ToList<TransactionMain>();
            }
        }

        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            TransactionMain emp = new TransactionMain();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.TransactionMains.Where(x => x.TransactionMainID == id).FirstOrDefault<TransactionMain>();
                }
            }
            return View(emp);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(TransactionMain emp, int CustomerID, double Amount )
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.TransactionMainID == 0)
                    {
                        db.TransactionMains.Add(emp);
                        db.SaveChanges();
                        TransactionDetailController.BackendPost(new TransactionDetail() { TransactionDetailID = 0, TransactionMainID = emp.TransactionMainID, LedgerNumber ="1", Description = emp.Description, CustomerID = CustomerID, Credit = 0, Debit=Amount });
                        LedgerTransactionController.BackendPostLedgerTransaction(new LedgerTransaction() { LedgerTransactionID = 0, LedgerID =Convert.ToInt32( LedgerController.GetLedgerNumber(CustomerID)), Debit= 0, Credit= Convert.ToDecimal(Amount), });
                        
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Data Added Successfully" }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllChargeTarrif()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    TransactionMain emp = db.TransactionMains.Where(x => x.TransactionMainID == id).FirstOrDefault<TransactionMain>();
                    db.TransactionMains.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllChargeTarrif()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}