using FAST_FOOD.Areas.Admin.Filters;
using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin.Controllers
{
    [AdminAuthorize]
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var donHang = db.DonHangs
      .Include(d => d.HinhThucThanhToan)
      .Include("ChiTietDonHangs.MonAn")
      .FirstOrDefault(d => d.MaDonHang == id);


            if (donHang == null)
                return HttpNotFound();

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
            System.Diagnostics.Debug.WriteLine("==> Connection: " + db.Database.Connection.ConnectionString);
            System.Diagnostics.Debug.WriteLine("==> Danhmuc count: " + db.Danhmucs.Count());

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null) return HttpNotFound();

            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHTTT", donHang.MaHTTT);
            return View(donHang);

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
                var dh = db.DonHangs.Find(donHang.MaDonHang);
                if (dh == null) return HttpNotFound();

                dh.TrangThai = donHang.TrangThai;
                dh.TenKhachHang = donHang.TenKhachHang;
                dh.TongTien = donHang.TongTien;
                dh.MaHTTT = donHang.MaHTTT;

                // ⭐ GIỮ NGUYÊN NGÀY ĐẶT
                // Không sửa NgayDat — tránh mất ngày tạo

                db.SaveChanges();
            }

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
        public ActionResult XacNhan(int id)
        {
            var dh = db.DonHangs.Find(id);
            if (dh == null) return HttpNotFound();

            dh.TrangThai = "Đã xác nhận";
            db.SaveChanges();

            TempData["Success"] = "Đơn #" + id + " đã được xác nhận";
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

        public ActionResult TuChoi(int id)
        {
            var donHang = db.DonHangs.Find(id);

            if (donHang == null)
                return HttpNotFound();

            if (donHang.TrangThai == "Chờ xác nhận")
            {
                donHang.TrangThai = "Đã Hủy"; // hoặc "Từ chối"
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
