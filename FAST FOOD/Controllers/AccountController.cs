using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Controllers
{
    public class AccountController : Controller
    {
        public readonly KFCContext db = new KFCContext();
        // GET: Account
        //dang ki
        public ActionResult DangKy() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(account model)
        {
            if (ModelState.IsValid)
            {
                if (db.accounts.Any(x => x.TenDangNhap == model.TenDangNhap))
                {
                    ViewBag.Error = "Tên đăng nhập đã tồn tại";
                    return View(model);
                }
                model.MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau);  //ma hoa
                model.VaiTro = "User"; //mac dinh la user
                db.accounts.Add(model);
                db.SaveChanges();


                TempData["Success"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap");
            }
            return View(model);
        }
        //dang nhap
        public ActionResult DangNhap() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DangNhap (string TenDangNhap , string MatKhau)
        {
            var user= db.accounts.FirstOrDefault(x => x.TenDangNhap == TenDangNhap);
            if(user == null || !BCrypt.Net.BCrypt.Verify(MatKhau, user.MatKhau))
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }

            Session["User"] = user;
            Session["Role"] = user.VaiTro;
            Session["HoTen"] = user.HoTen; 
            if(user.VaiTro == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //dang xuat
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("DangNhap", "account");


        }

    }
}