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
                // Kiểm tra đăng nhập
                if (Session["User"] == null)
                {
                    TempData["Info"] = "Bạn cần đăng nhập trước khi thêm vào giỏ hàng!";
                    return RedirectToAction("DangNhap", "Account", new { area = "" });
                }

                var mon = db.MonAns.Find(id);
                if (mon == null)
                    return HttpNotFound();

                var cart = GetCart();
                var item = cart.FirstOrDefault(c => c.MonAnId == id);

                if (item != null)
                    item.SoLuong++;
                else
                    cart.Add(new CartItem
                    {
                        MonAnId = mon.MonAnId,
                        TenMon = mon.TenMon,
                        Gia = mon.Gia,
                        HinhAnh = mon.HinhAnh,
                        SoLuong = 1
                    });

                TempData["Success"] = "Đã thêm món vào giỏ hàng!"; // ⭐ THÔNG BÁO

                return RedirectToAction("Index", "Home", new { area = "" });
            }
        // Hiển thị giỏ hàng
        public ActionResult Index()
        {
            // Nếu chưa đăng nhập → chuyển sang Login
            if (Session["User"] == null)
            {
                TempData["Info"] = "Bạn cần đăng nhập để xem giỏ hàng!";
                return RedirectToAction("DangNhap", "Account", new { area = "" });
            }

            var cart = GetCart();
            ViewBag.Total = cart.Any() ? cart.Sum(i => i.ThanhTien) : 0;

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
                    return RedirectToAction("Index", "Home");

                // GIẢI PHÁP CHUẨN: đúng tên trường trong DB
                ViewBag.MaHTTT = new SelectList(db.HinhThucThanhToans.ToList(),
                                                "MaHTTT",
                                                "TenHinhThuc");

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

                var user = (account)Session["User"];   // ⭐ LẤY THÔNG TIN USER ĐĂNG NHẬP

                var donHang = new DonHang
                {
                    TenKhachHang = user.HoTen,        // ⭐ DÙNG TÊN USER — KHÔNG DÙNG TÊN FORM
                    DiaChi = DiaChi,
                    SoDienThoai = SoDienThoai,
                    NgayDat = DateTime.Now,
                    MaHTTT = MaHTTT,
                    TrangThai = "Chờ xác nhận",
                    TongTien = cart.Sum(i => i.ThanhTien)
                };

                db.DonHangs.Add(donHang);
                db.SaveChanges();

                foreach (var item in cart)
                {
                    db.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        DonHangId = donHang.MaDonHang,
                        MonAnId = item.MonAnId,
                        SoLuong = item.SoLuong,
                        ThanhTien = item.ThanhTien
                    });
                }

                db.SaveChanges();

                Session["Cart"] = null;
                TempData["Message"] = "Đặt hàng thành công!";
                return RedirectToAction("Success");
            }

            public ActionResult Success()
            {
                ViewBag.Message = TempData["Message"];
                return View();
            }
            [HttpPost]
            public JsonResult AddToCartAjax(int id)
            {
                if (Session["User"] == null)
                    return Json(new { status = "notlogin" });

                var mon = db.MonAns.Find(id);
                if (mon == null)
                    return Json(new { status = "error" });

                var cart = GetCart();
                var item = cart.FirstOrDefault(x => x.MonAnId == id);

                if (item != null)
                    item.SoLuong++;
                else
                    cart.Add(new CartItem
                    {
                        MonAnId = mon.MonAnId,
                        TenMon = mon.TenMon,
                        Gia = mon.Gia,
                        HinhAnh = mon.HinhAnh,
                        SoLuong = 1
                    });

                // cập nhật số lượng tổng
                int totalCount = cart.Sum(x => x.SoLuong);

                return Json(new { status = "success", count = totalCount });
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