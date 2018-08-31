using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class TransactionMainController : Controller
    {
        // GET: TransactionMain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllTransactionMain());
        }

        IEnumerable<TransactionMain> GetAllTransactionMain()
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
        public ActionResult AddorEdit(TransactionMain emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.TransactionMainID == 0)
                    {
                        db.TransactionMains.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
                //return Json(new { success = true, message = emp.TransactionMainID }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllTransactionMain()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllTransactionMain()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}