using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllProduct());
        }

        IEnumerable<Product> GetAllProduct()
        {
            using (DBModel db = new DBModel())
            {
                return db.Products.ToList<Product>();
            }
        }

        //[HttpGet] iterator will be by default
        public ActionResult AddorEdit(int id = 0)
        {
            Product emp = new Product();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.Products.Where(x => x.ProductID == id).FirstOrDefault<Product>();
                }
            }
            return View(emp);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddorEdit(Product emp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    if (emp.ProductID == 0)
                    {
                        db.Products.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProduct()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    Product emp = db.Products.Where(x => x.ProductID == id).FirstOrDefault<Product>();
                    db.Products.Remove(emp);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProduct()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail(int id = 0)
        {
         
            Product objcity = new DBModel().Products.Find(id);

            return View(objcity);
        }

        public ActionResult ProductNameDetail(int id = 0)
        {
            return Content(new DBModel().Products.Find(id).ProductName);
        }
    }
}