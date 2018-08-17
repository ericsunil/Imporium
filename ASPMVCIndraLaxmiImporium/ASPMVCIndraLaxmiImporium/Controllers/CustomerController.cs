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
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.CustomerID == 0)
                    {
                        db.Customers.Add(emp);
                        db.SaveChanges();
                       
                        LedgerController.BackendPost(new Ledger() { LedgerID=0, LedgerNumber=0, Type=emp.Type,CustomerID=emp.CustomerID });
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCustomer()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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