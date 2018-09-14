using ASPMVCIndraLaxmiImporium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVCIndraLaxmiImporium.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Ledger()
        {
            return View();
        }
        public ActionResult ReportCustomerLedger(int id)
        {
            //return View(new DBModel().R );
            return View();
        }
    }
}