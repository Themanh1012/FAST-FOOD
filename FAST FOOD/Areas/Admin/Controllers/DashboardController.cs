using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ViewBag.TotalRevenue = db.DonHangs
                .Sum(d => (decimal?)d.TongTien) ?? 0;

            // Doanh thu hôm nay
            var today = DateTime.Today;

            ViewBag.TodayRevenue = db.DonHangs
                .Where(d =>
                    DbFunctions.TruncateTime(d.NgayDat) == today
                )
                .Sum(d => (decimal?)d.TongTien) ?? 0;

            // Tổng số đơn
            ViewBag.TotalOrders = db.DonHangs.Count();

            // Tổng số khách
            ViewBag.UserCount = db.accounts.Count();

            var users = db.accounts.Select(u => new UserOrderViewModel
            {
                UserId = u.MaTK,
                HoTen = u.HoTen,
                Email = u.Email,

                TotalOrders = db.DonHangs
                   .Count(d => d.TenKhachHang.Trim() == u.HoTen.Trim()),

                TotalMoney = db.DonHangs
                   .Where(d => d.TenKhachHang.Trim() == u.HoTen.Trim())
                   .Sum(d => (decimal?)d.TongTien) ?? 0,

                LastOrder = db.DonHangs
                   .Where(d => d.TenKhachHang.Trim() == u.HoTen.Trim())
                   .OrderByDescending(d => d.NgayDat)
                   .Select(d => d.NgayDat)
                   .FirstOrDefault()
            })
           .OrderByDescending(x => x.LastOrder)
           .Take(10) // 10 khách hàng gần nhất
           .ToList();

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
                    .Where(d => d.TenKhachHang.Trim() == user.HoTen.Trim())
                    .OrderByDescending(d => d.NgayDat)
                    .ToList()
            };

            return View(model);
        }
    }

    }
