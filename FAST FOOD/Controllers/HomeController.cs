using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Controllers
{
    public class HomeController : Controller
    {
        private KFCContext db = new KFCContext();
        public ActionResult Index()
        {
            // Lấy tất cả danh mục từ database
            var danhMucs = db.Danhmucs.ToList();
           

            ViewBag.Suggest = db.MonAns.Take(10).ToList();
            // Trả danh sách danh mục qua View
            return View(danhMucs);
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuiLienHe(string HoTen, string Email, string NoiDung)
        {
            // Xử lý gửi email hoặc lưu vào database ở đây
            ViewBag.Message = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất.";
            return View("Contact");
        }
    }
}