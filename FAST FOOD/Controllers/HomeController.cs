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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}