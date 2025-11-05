using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FAST_FOOD.Models;
using System.Web.Mvc;
using System.Net;

namespace FAST_FOOD.Areas.Customer.Controllers
{
    public class MonAnsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: MonAns (hiển thị danh sách tất cả món ăn)
        public ActionResult Index()
        {
            var monAns = db.MonAns.Include(m => m.DanhMuc).ToList();
            return View(monAns);
        }

        // GET: MonAns/Details/5 (hiển thị chi tiết món)
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var monAn = db.MonAns
                          .Include(m => m.DanhMuc)
                          .FirstOrDefault(m => m.MonAnId == id);

            if (monAn == null)
                return HttpNotFound();

            return View(monAn);
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