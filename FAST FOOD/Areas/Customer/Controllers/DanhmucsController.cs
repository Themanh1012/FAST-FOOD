using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FAST_FOOD.Models;
using System.Data.Entity;
namespace FAST_FOOD.Areas.Customer.Controllers
{
    public class DanhmucsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: Customer/Danhmucs
        public ActionResult Index()
        {
            var danhMucs = db.Danhmucs.ToList();
            return View(danhMucs);
        }

        // GET: Customer/Danhmucs/Details/5
        public ActionResult Details(int id)
        {
            var danhMuc = db.Danhmucs
                            .Include("MonAns")
                            .FirstOrDefault(d => d.DanhMucId == id);
            if (danhMuc == null)
                return HttpNotFound();

            return View(danhMuc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}