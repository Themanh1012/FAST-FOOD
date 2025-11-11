using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FAST_FOOD.Models;

namespace FAST_FOOD.Areas.Customer.Controllers
{
    public class LichSusController : Controller
    {
        private readonly KFCContext db = new KFCContext();

        //  Hiển thị danh sách đơn hàng
        public ActionResult Index()
        {
            var donHangs = db.DonHangs
                .Include(d => d.HinhThucThanhToan)
                .Include(d => d.ChiTietDonHangs.Select(ct => ct.MonAn))
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(donHangs);
        }

        //  Xem chi tiết đơn hàng
        public ActionResult Details(int id)
        {
            var donHang = db.DonHangs
                .Include(d => d.ChiTietDonHangs.Select(ct => ct.MonAn))
                .Include(d => d.HinhThucThanhToan)
                .FirstOrDefault(d => d.MaDonHang == id);

            if (donHang == null)
                return HttpNotFound();

            return View(donHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}