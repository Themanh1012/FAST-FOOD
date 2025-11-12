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
    public class CartsController : Controller
    {
        private KFCContext db = new KFCContext();

        // Lấy giỏ hàng từ Session
        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Thêm món ăn vào giỏ
        public ActionResult AddToCart(int id)
        {
            var mon = db.MonAns.Find(id);
            if (mon == null)
                return HttpNotFound();

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.MonAnId == id);
            if (item != null)
            {
                item.SoLuong++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    MonAnId = mon.MonAnId,
                    TenMon = mon.TenMon,
                    Gia = mon.Gia,
                    HinhAnh = mon.HinhAnh,
                    SoLuong = 1
                });
            }

            return RedirectToAction("Index");
        }

        // Hiển thị giỏ hàng
        public ActionResult Index()
        {
            var cart = GetCart();
            ViewBag.Total = cart.Sum(i => i.ThanhTien);
            return View(cart);
        }

        // Xóa món khỏi giỏ
        public ActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.MonAnId == id);
            if (item != null) cart.Remove(item);
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng món
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.MonAnId == id);
            if (item != null && quantity > 0)
                item.SoLuong = quantity;
            return RedirectToAction("Index");
        }

        // Trang Checkout - nhập thông tin khách hàng
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Home");
            }
                
            ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "TenHinhThuc");
            ViewBag.Total = cart.Sum(i => i.ThanhTien);
            return View(cart);
        }

        // Nhận dữ liệu từ form Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(string TenKhachHang, string DiaChi, string SoDienThoai, int MaHTTT)
        {
            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Index", "Home");

            var donHang = new DonHang
            {
                TenKhachHang = TenKhachHang,
                DiaChi = DiaChi,
                SoDienThoai = SoDienThoai,
                NgayDat = DateTime.Now,
                MaHTTT = MaHTTT,
                TrangThai = "Chờ xác nhận",
                TongTien = cart.Sum(i => i.ThanhTien)
            };

            db.DonHangs.Add(donHang);
            db.SaveChanges();

            // Lưu chi tiết đơn hàng
            foreach (var item in cart)
            {
                var ct = new ChiTietDonHang
                {
                    DonHangId = donHang.MaDonHang,
                    MonAnId = item.MonAnId,
                    SoLuong = item.SoLuong,
                    ThanhTien = item.ThanhTien
                };
                db.ChiTietDonHangs.Add(ct);
            }

            db.SaveChanges();

            // Xóa giỏ sau khi đặt hàng
            Session["Cart"] = null;
            TempData["Message"] = "Đặt hàng thành công!";
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        public ActionResult History()
        {
            // Lấy tất cả đơn hàng 
            var donHangs = db.DonHangs
                .Include(d => d.HinhThucThanhToan)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(donHangs);
        }
    }
}