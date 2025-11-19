using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly KFCContext db = new KFCContext();

        public ActionResult Index()
        {
            var users = db.accounts.Select(u => new UserOrderViewModel
            {
                UserId = u.MaTK,
                HoTen = u.HoTen,
                Email = u.Email,

                TotalOrders = db.DonHangs.Count(d => d.TenKhachHang == u.HoTen),

                TotalMoney = db.DonHangs
                    .Where(d => d.TenKhachHang == u.HoTen)
                    .Sum(d => (decimal?)d.TongTien) ?? 0,

                LastOrder = db.DonHangs
                    .Where(d => d.TenKhachHang == u.HoTen)
                    .OrderByDescending(d => d.NgayDat)
                    .Select(d => d.NgayDat)
                    .FirstOrDefault()
            }).ToList();

            ViewBag.UserOrders = users;

            return View();
        }

        public ActionResult UserDetail(int id)
        {
            var user = db.accounts.Find(id);
            if (user == null)
                return HttpNotFound();

            var model = new UserOrderDetailViewModel
            {
                HoTen = user.HoTen,
                Email = user.Email,
                DonHangs = db.DonHangs
                    .Where(d => d.TenKhachHang == user.HoTen)
                    .OrderByDescending(d => d.NgayDat)
                    .ToList()
            };

            return View(model);
        }
    }
}