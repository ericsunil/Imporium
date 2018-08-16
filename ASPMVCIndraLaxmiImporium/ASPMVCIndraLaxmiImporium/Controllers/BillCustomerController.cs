using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class BillCustomerController : Controller
    {
        // GET: BillCustomer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAll()
        {
            return View(GetAllBillCustomer());
        }

        IEnumerable<BillCustomer> GetAllBillCustomer()
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
        public ActionResult AddorEdit(BillCustomer emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.BillCustomerID == 0)
                    {
                        db.BillCustomers.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("ViewAll");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllBillCustomer()), message = "Save Successfully" }, JsonRequestBehavior.AllowGet);
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
                    BillCustomer emp = db.BillCustomers.Where(x => x.BillCustomerID == id).FirstOrDefault<BillCustomer>();
                    db.BillCustomers.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllBillCustomer()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult GetCustomer(String id = "")
        {
            String[] type = { "Debtor", "Creditor", "Transport" };
            String CustomerType = id;
            List<Customer> objcity = new List<Customer>();
            objcity = new DBModel().Customers.Where(m => m.Type == CustomerType).ToList();
            SelectList obgcity = new SelectList(objcity, "CustomerID", "CustomerName", 0);
            // return Content(objcity.Count+""+ classid);

            return Json(obgcity, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProduct()
        {
            List<Product> objcity = new List<Product>();
            objcity = new DBModel().Products.ToList();
            SelectList obgcity = new SelectList(objcity, "ProductID", "ProductName", 0);
            // return Content(objcity.Count+""+ classid);

            return Json(obgcity, JsonRequestBehavior.AllowGet);
        }

    }
}