using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class LedgerController : Controller
    {
        // GET: Ledger
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllLedger());
        }

        IEnumerable<Ledger> GetAllLedger()
        {
            using (DBModel db = new DBModel())
            {
                return db.Ledgers.ToList<Ledger>();
            }
        }


        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            Ledger emp = new Ledger();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.Ledgers.Where(x => x.LedgerID == id).FirstOrDefault<Ledger>();
                }
            }
            return View(emp);
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public static void BackendPost(Ledger emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.LedgerID == 0)
                    {
                        db.Ledgers.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllLedger()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                //return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(Ledger emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.LedgerID == 0)
                    {
                        db.Ledgers.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllLedger()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    Ledger emp = db.Ledgers.Where(x => x.LedgerID == id).FirstOrDefault<Ledger>();
                    db.Ledgers.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllLedger()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}