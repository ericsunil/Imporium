using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class LedgerTransactionController : Controller
    {
        // GET: LedgerTransaction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllLedgetTransaction());
        }

        IEnumerable<LedgerTransaction> GetAllLedgetTransaction()
        {
            using (DBModel db = new DBModel())
            {
                return db.LedgerTransactions.ToList<LedgerTransaction>();
            }
        }

        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            LedgerTransaction emp = new LedgerTransaction();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.LedgerTransactions.Where(x => x.LedgerTransactionID == id).FirstOrDefault<LedgerTransaction>();
                }
            }
            return View(emp);
        }


        public static void BackendPostLedgerTransaction(LedgerTransaction emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.LedgerTransactionID == 0)
                    {
                        db.LedgerTransactions.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                
            }
            catch (Exception ex)
            {

                
            }
        }

        public static decimal BackendPostBalange(int LedgerID)
        {
            return (decimal)new DBModel().LedgerTransactions.Where(x => x.LedgerTransactionID == new DBModel().LedgerTransactions.Where(z => z.LedgerID == LedgerID).Max(y => y.LedgerTransactionID)).FirstOrDefault<LedgerTransaction>().Balance;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(LedgerTransaction emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.LedgerTransactionID == 0)
                    {
                        db.LedgerTransactions.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllLedgetTransaction()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    LedgerTransaction emp = db.LedgerTransactions.Where(x => x.LedgerTransactionID == id).FirstOrDefault<LedgerTransaction>();
                    db.LedgerTransactions.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllLedgetTransaction()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}