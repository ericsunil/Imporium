using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll(int id=0)
        {

            return View(GetAllBill(id));
        }

        IEnumerable<Bill> GetAllBill( int id)
        {
            using (DBModel db = new DBModel())
            {
                return db.Bills.Where(x => x.BillNumber == id).ToList<Bill>();
            }
        }

        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            Bill emp = new Bill();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.Bills.Where(x => x.BillID == id).FirstOrDefault<Bill>();
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
                    if (emp.BillID == 0)
                    {
                        db.Bills.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Data Added Successfully" }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllBill()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message
    }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Bill emp = db.Bills.Where(x => x.BillID == id).FirstOrDefault<Bill>();
                    db.Bills.Remove(emp);
                    db.SaveChanges();
                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllBill(emp.BillNumber)), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}