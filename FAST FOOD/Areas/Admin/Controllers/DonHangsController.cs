using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FAST_FOOD.Models;

namespace FAST_FOOD.Areas.Admin.Controllers
{
    public class DonHangsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: DonHangs
        public ActionResult Index()
        {
            var donHangs = db.DonHangs
                             .Include(d => d.HinhThucThanhToan)
                             .ToList();
            return View(donHangs);
        }


        // GET: DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "MaDonHang");
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHTTT");

            var hoaDon = new HoaDon
            {
                NgayThanhToan = DateTime.Now
            };

            return View(hoaDon);
        }


        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenKhachHang,DiaChi,SoDienThoai,TongTien,TrangThai,MaHTTT")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                donHang.NgayDat = DateTime.Now;
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHTTT", donHang.MaHTTT);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null) return HttpNotFound();

            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "MaDonHang", hoaDon.MaDonHang);
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHTTT", hoaDon.MaHTTT);
            return View(hoaDon);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Cập nhật đơn hàng thành công!";
                return RedirectToAction("Index");
            }

            // Gán lại danh sách chọn khi model không hợp lệ
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaDonHang", "MaDonHang", donHang.MaHTTT);
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
            db.SaveChanges();
            TempData["Success"] = "Xóa đơn hàng thành công!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        
        }
        // Ví dụ trong DonHangsController
        public ActionResult XacNhanThanhToan(int id)
        {
            var donHang = db.DonHangs.Find(id);
            if (donHang == null)
                return HttpNotFound();

            // Tạo hóa đơn mới
            var hoaDon = new HoaDon
            {
                MaDonHang = donHang.MaDonHang,
                MaHTTT = donHang.MaHTTT,  // nếu DonHang có thuộc tính này
                TongTien = donHang.TongTien,
                NgayThanhToan = DateTime.Now
            };

            db.HoaDons.Add(hoaDon);
            db.SaveChanges();

            return RedirectToAction("Index", "HoaDons");
        }

    }
}
