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
           
                DBModel db = new DBModel();
                Bill emp = db.Bills.Where(b => b.IsCommit == false).FirstOrDefault<Bill>();
            if (emp !=null)
            {
                db.Bills.Remove(emp);
                db.SaveChanges();
            }
           
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
                emp.Ispaid = false;
                using (DBModel db = new DBModel())
                {
                    if (emp.BillCustomerID == 0)
                    {
                        db.BillCustomers.Add(emp);
                        db.SaveChanges();
                        DBModel isCommet = new DBModel();
                      List< Bill> a= isCommet.Bills.Where(b=>b.BillNumber == emp.BillNumber).ToList<Bill>();
                       foreach(Bill bill in a)
                        {
                            using (DBModel commit = new DBModel())
                            {
                                bill.IsCommit = true;
                                commit.Entry(bill).State = EntityState.Modified;
                                commit.SaveChanges();
                              }
                        }

                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
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
                    Bill emp1 = db.Bills.Where(x => x.BillNumber == emp.BillNumber).FirstOrDefault<Bill>();
                    db.BillCustomers.Remove(emp);
                    
                    db.Bills.Remove(emp1);
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