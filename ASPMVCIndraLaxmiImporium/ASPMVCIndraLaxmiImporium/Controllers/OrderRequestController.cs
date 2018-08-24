using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class OrderRequestController : Controller
    {
        // GET: OrderRequest
        public ActionResult Index()
        {
            return View(GetAllOrderRequest());
        }

        IEnumerable<BillCustomer> GetAllOrderRequest()
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
    }
}