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
      
            public ActionResult Index(int? id)
             {


            //var monAns = db.MonAns
            //                .Where(x => x.DanhMucId == id)
            //                .ToList();
            //var selected = db.Danhmucs.Find(id);

            //// danh sách tất cả danh mục để làm MENU NGANG
            //var categories = db.Danhmucs.ToList();
            //ViewBag.SelectedCategory = selected;
            //ViewBag.Categories = categories;
            var selected = db.Danhmucs.Find(id);
            if (selected == null) return HttpNotFound();

            var categories = db.Danhmucs.ToList();
            var monans = db.MonAns.Where(m => m.DanhMucId == id).ToList();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = selected;

            return View(monans);
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

        public ActionResult Category(int id)
        {
            var cat = db.Danhmucs.Find(id);
            if (cat == null) return HttpNotFound();

            var mon = db.MonAns.Where(m => m.DanhMucId == id).ToList();
            var all = db.Danhmucs.ToList();

            var vm = new DanhMucViewModel
            {
                DanhMuc = cat,
                MonAns = mon,
                Danhmucs = all
            };

            return View(vm);
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