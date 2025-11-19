using FAST_FOOD.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
namespace FAST_FOOD.Controllers
{
    public class AccountController : Controller
    {
        private readonly KFCContext db = new KFCContext();

        // ------------------- ĐĂNG KÝ -------------------
        public ActionResult DangKy() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(account model)
        {
            try
            {
                // 1️⃣ Kiểm tra dữ liệu hợp lệ
                if (!ModelState.IsValid)
                {
                    var errors = string.Join("<br/>",
                        ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage));
                    ViewBag.Error = "Vui lòng nhập đầy đủ thông tin hợp lệ:<br/>" + errors;
                    return View(model);
                }

                // 2️⃣ Kiểm tra trùng tên đăng nhập
                if (db.accounts.Any(x => x.TenDangNhap == model.TenDangNhap))
                {
                    ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                    return View(model);
                }

                // 3️⃣ Kiểm tra trùng email
                if (!string.IsNullOrWhiteSpace(model.Email) &&
                    db.accounts.Any(x => x.Email == model.Email))
                {
                    ViewBag.Error = "Email này đã được sử dụng.";
                    return View(model);
                }

                // 4️⃣ Đặt vai trò mặc định
                model.VaiTro = "User";

                // 5️⃣ Mã hóa mật khẩu
                model.MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau);

                // 6️⃣ Lưu vào DB
                db.Configuration.ValidateOnSaveEnabled = false;
                db.accounts.Add(model);
                db.SaveChanges();

                TempData["Success"] = "🎉 Đăng ký thành công! Bạn có thể đăng nhập ngay.";
                return RedirectToAction("DangNhap");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("❌ Exception: " + ex.Message);
                ViewBag.Error = "Lỗi hệ thống: " + ex.Message;
                return View(model);
            }
        }

        // ------------------- ĐĂNG NHẬP -------------------
        public ActionResult DangNhap() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string TenDangNhap, string MatKhau)
        {
            var user = db.accounts.FirstOrDefault(x => x.TenDangNhap == TenDangNhap);
            if (user == null || !BCrypt.Net.BCrypt.Verify(MatKhau, user.MatKhau))
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }

            Session["User"] = user;
            Session["Role"] = user.VaiTro;

            Session["HoTen"] = user.HoTen;

            TempData["Success"] = $"Xin chào {user.HoTen ?? user.TenDangNhap}!";
            return RedirectToAction("Index", "Home");
        }

        // ------------------- ĐĂNG XUẤT -------------------
        public ActionResult DangXuat()
        {
            Session.Clear();
            TempData["Info"] = "Bạn đã đăng xuất.";
            return RedirectToAction("DangNhap", "Account");
        }

        // ------------------- QUÊN MẬT KHẨU -------------------
        public ActionResult QuenMatKhau() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuenMatKhau(string email)
        {
            var user = db.accounts.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                ViewBag.Error = "Email không tồn tại trong hệ thống.";
                return View();
            }

            string token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.TokenExpireTime = DateTime.Now.AddMinutes(15);
            db.SaveChanges();

            string resetLink = Url.Action("DatLaiMatKhau", "Account",
                new { token = token }, protocol: Request.Url.Scheme);

            GuiMail(email, "Đặt lại mật khẩu FAST FOOD",
                $"Xin chào {user.HoTen ?? user.TenDangNhap},<br><br>" +
                $"Bạn vừa yêu cầu đặt lại mật khẩu.<br>" +
                $"Nhấn vào liên kết sau để đặt lại mật khẩu:<br>" +
                $"<a href='{resetLink}'>Đặt lại mật khẩu</a><br><br>" +
                $"Liên kết này sẽ hết hạn sau 15 phút.");

            ViewBag.Success = "Đã gửi liên kết đặt lại mật khẩu qua email.";
            return View();
        }

        // ------------------- ĐẶT LẠI MẬT KHẨU -------------------
        public ActionResult DatLaiMatKhau(string token)
        {
            var user = db.accounts.FirstOrDefault(x =>
                x.ResetToken == token && x.TokenExpireTime > DateTime.Now);
            if (user == null)
            {
                TempData["Error"] = "Liên kết không hợp lệ hoặc đã hết hạn.";
                return RedirectToAction("DangNhap");
            }

            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLaiMatKhau(string token, string MatKhau, string XacNhanMatKhau)
        {
            var user = db.accounts.FirstOrDefault(x =>
                x.ResetToken == token && x.TokenExpireTime > DateTime.Now);
            if (user == null)
            {
                TempData["Error"] = "Liên kết không hợp lệ hoặc đã hết hạn.";
                return RedirectToAction("DangNhap");
            }

            if (MatKhau != XacNhanMatKhau)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                ViewBag.Token = token;
                return View();
            }

            user.MatKhau = BCrypt.Net.BCrypt.HashPassword(MatKhau);
            user.ResetToken = null;
            user.TokenExpireTime = null;
            db.SaveChanges();

            TempData["Success"] = "Đặt lại mật khẩu thành công! Vui lòng đăng nhập.";
            return RedirectToAction("DangNhap");
        }

        // ------------------- GỬI EMAIL -------------------
        private void GuiMail(string toEmail, string subject, string body)
        {
            var fromEmail = "yourgmail@gmail.com";
            var fromPassword = "app_password_16_chars";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, fromPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        // ------------------- TRANG CÁ NHÂN -------------------
        public ActionResult Profile()
        {
            if (Session["User"] == null)
                return RedirectToAction("DangNhap");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatTaiKhoan(account model)
        {
            var acc = db.accounts.Find(model.MaTK);
            if (acc != null)
            {
                acc.HoTen = model.HoTen;
                acc.Email = model.Email;
                db.SaveChanges();
                Session["User"] = acc;
                TempData["Success"] = "Cập nhật thông tin thành công!";
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaTaiKhoan(int MaTK)
        {
            var acc = db.accounts.Find(MaTK);
            if (acc != null)
            {
                db.accounts.Remove(acc);
                db.SaveChanges();
                Session.Clear();
                TempData["Info"] = "Tài khoản của bạn đã được xóa.";
            }
            return RedirectToAction("DangNhap");
        }
    }
}
