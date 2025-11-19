using FAST_FOOD.Areas.Admin.Filters;
using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HinhThucThanhToansController : Controller
    {
        private readonly KFCContext db = new KFCContext();

        // GET: Admin/HinhThucThanhToans
        public ActionResult Index()
        {
            var httt = db.HinhThucThanhToans.ToList();
            return View(httt);
        }

        // GET: Admin/HinhThucThanhToans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var httt = db.HinhThucThanhToans
                         .Include(h => h.DonHangs)
                         .FirstOrDefault(h => h.MaHTTT == id);

            if (httt == null)
                return HttpNotFound();

            return View(httt);
        }

        // GET: Admin/HinhThucThanhToans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HinhThucThanhToans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HinhThucThanhToan hinhThucThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.HinhThucThanhToans.Add(hinhThucThanhToan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hinhThucThanhToan);
        }

        // GET: Admin/HinhThucThanhToans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hinhThucThanhToan = db.HinhThucThanhToans.Find(id);
            if (hinhThucThanhToan == null)
                return HttpNotFound();

            return View(hinhThucThanhToan);
        }

        // POST: Admin/HinhThucThanhToans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HinhThucThanhToan hinhThucThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhThucThanhToan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hinhThucThanhToan);
        }

        // GET: Admin/HinhThucThanhToans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hinhThucThanhToan = db.HinhThucThanhToans.Find(id);
            if (hinhThucThanhToan == null)
                return HttpNotFound();

            return View(hinhThucThanhToan);
        }

        // POST: Admin/HinhThucThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hinhThucThanhToan = db.HinhThucThanhToans.Find(id);
            if (hinhThucThanhToan == null)
                return HttpNotFound();

            db.HinhThucThanhToans.Remove(hinhThucThanhToan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
