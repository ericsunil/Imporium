using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class TransactionDetailController : Controller
    {
        // GET: TransactionDetail
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ViewAll()
        {
            return View(GetAllTransactionMail());
        }

        IEnumerable<TransactionDetail> GetAllTransactionMail()
        {
            using (DBModel db = new DBModel())
            {
                return db.TransactionDetails.ToList<TransactionDetail>();
            }
        }



        public ActionResult AddorEdit(int id = 0)
        {
            TransactionDetail emp= new TransactionDetail();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.TransactionDetails.Where(x => x.TransactionDetailID == id).FirstOrDefault<TransactionDetail>();
                }
            }
            return View(emp);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(TransactionDetail emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.TransactionDetailID == 0)
                    {
                        db.TransactionDetails.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllTransactionMail()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    TransactionDetail emp = db.TransactionDetails.Where(x => x.TransactionDetailID == id).FirstOrDefault<TransactionDetail>();
                    db.TransactionDetails.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllTransactionMail()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}