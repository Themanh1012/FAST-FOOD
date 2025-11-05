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
    public class DanhmucsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: Danhmuc
        public ActionResult Index()
        {
            // Lấy danh sách danh mục để hiển thị ở trang chính (menu)
            var danhMucs = db.Danhmucs.ToList();
            return View(danhMucs);
        }

        // GET: Danhmuc/Details/5
        public ActionResult Details(int id)
        {
            var danhMuc = db.Danhmucs
                            .Include(d => d.MonAns) // nạp luôn danh sách món ăn thuộc danh mục
                            .FirstOrDefault(d => d.DanhMucId == id);

            if (danhMuc == null)
                return HttpNotFound();

            // Gửi danh sách món ăn qua ViewBag để view dùng
            ViewBag.MonAnTrongDanhMuc = danhMuc.MonAns.ToList();

            return View(danhMuc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}