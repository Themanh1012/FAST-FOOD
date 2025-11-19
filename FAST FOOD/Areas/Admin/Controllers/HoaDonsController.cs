using FAST_FOOD.Areas.Admin.Filters;
using FAST_FOOD.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HoaDonsController : Controller
    {
        private readonly KFCContext db = new KFCContext();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {

           

            var hoaDons = db.HoaDons
                            .Include(h => h.DonHang)
                            .Include(h => h.HinhThucThanhToan)
                            .ToList();
            return View(hoaDons);

        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hoaDon = db.HoaDons
                .Include(h => h.DonHang)
                .Include(h => h.HinhThucThanhToan)
                .FirstOrDefault(h => h.MaHoaDon == id);

            if (hoaDon == null)
                return HttpNotFound();

            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "TenKhachHang");
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHinhThuc");

            


            return View();
        }

        // POST: Admin/HoaDons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                hoaDon.NgayThanhToan = DateTime.Now; // tự động set ngày thanh toán
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "TenKhachHang", hoaDon.MaDonHang);
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHinhThuc", hoaDon.MaHTTT);

            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
                return HttpNotFound();

            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "TenKhachHang", hoaDon.MaDonHang);
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHinhThuc", hoaDon.MaHTTT);

            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "TenKhachHang", hoaDon.MaDonHang);
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHinhThuc", hoaDon.MaHTTT);

            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hoaDon = db.HoaDons
                .Include(h => h.DonHang)
                .Include(h => h.HinhThucThanhToan)
                .FirstOrDefault(h => h.MaHoaDon == id);

            if (hoaDon == null)
                return HttpNotFound();

            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
                return HttpNotFound();

            db.HoaDons.Remove(hoaDon);
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
