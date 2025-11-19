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
    public class ChiTietDonHangsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: ChiTietDonHangs
        public ActionResult Index(int? donHangId)
        {
           if(donHangId ==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var chiTietDonHangs = db.ChiTietDonHangs
                 .Include(c => c.MonAn)
                 .Include(c => c.DonHang)
                 .Where(c => c.DonHangId == donHangId)
                 .ToList();
            ViewBag.DonHang = db.DonHangs.Find(donHangId);
            return View(chiTietDonHangs);
        }

        // GET: ChiTietDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Create
        public ActionResult Create(int donHangId)
        {

            var model = new ChiTietDonHang
            {
                DonHangId = donHangId
            };
            ViewBag.MonAnId = new SelectList(db.MonAns, "MonAnId", "TenMon");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietDonHang chiTiet)
        {
            if (ModelState.IsValid)
            {

                if (chiTiet.SoLuong <= 0)
                {
                    ModelState.AddModelError("SoLuong", "Số lượng phải lớn hơn 0");
                }

                var mon = db.MonAns.Find(chiTiet.MonAnId);
                if (mon != null)
                {
                    chiTiet.ThanhTien = mon.Gia * chiTiet.SoLuong;
                }

                db.ChiTietDonHangs.Add(chiTiet);
                db.SaveChanges();

                // ✅ cập nhật tổng tiền sau khi thêm
                CapNhatTongTien(chiTiet.DonHangId);

                return RedirectToAction("Index", new { donHangId = chiTiet.DonHangId });
            }
            ViewBag.MonAnId= new SelectList(db.MonAns, "MonAnId", "TenMon", chiTiet.MonAnId);   
            return View(chiTiet);
        }


        // GET: ChiTietDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonHangId = new SelectList(db.DonHangs, "DonHangId", "TenKhachHang", chiTietDonHang.DonHangId);
            ViewBag.MonAnId = new SelectList(db.MonAns, "MonAnId", "TenMon", chiTietDonHang.MonAnId);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChiTietId,DonHangId,MonAnId,SoLuong,ThanhTien")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonHangId = new SelectList(db.DonHangs, "DonHangId", "TenKhachHang", chiTietDonHang.DonHangId);
            ViewBag.MonAnId = new SelectList(db.MonAns, "MonAnId", "TenMon", chiTietDonHang.MonAnId);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chiTietDonHang = db.ChiTietDonHangs.Include(c => c.MonAn).Include(c => c.DonHang)
                .FirstOrDefault(c => c.ChiTietId == id);
            if (chiTietDonHang == null) return HttpNotFound();
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            //db.ChiTietDonHangs.Remove(chiTietDonHang);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            var chiTietDonHang = db.ChiTietDonHangs.Find(id);
            int donHangId = chiTietDonHang.DonHangId;
            db.ChiTietDonHangs.Remove(chiTietDonHang);
            db.SaveChanges();

            //cap nhat tong don
            var don = db.DonHangs.Find(donHangId);
            don.TongTien = db.ChiTietDonHangs
                .Where(c => c.DonHangId == don.MaDonHang)
                .Sum(c => c.ThanhTien);
            db.SaveChanges();
            return RedirectToAction("Index", new { donHangId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void CapNhatTongTien(int donHangId)
        {
            var donHang = db.DonHangs.FirstOrDefault(d => d.MaDonHang == donHangId);
            if (donHang != null)
            {
                var tongTien = db.ChiTietDonHangs
                                 .Where(ct => ct.DonHangId == donHangId)
                                 .Sum(ct => (decimal?)ct.ThanhTien) ?? 0;
                donHang.TongTien = tongTien;
                db.SaveChanges();
            }
        }
    }
}
