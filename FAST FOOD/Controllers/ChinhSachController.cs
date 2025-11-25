using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Controllers
{
    public class ChinhSachController : Controller
    {
        // 1. Chính sách hoạt động
        public ActionResult HoatDong()
        {
            return View();
        }

        // 2. Chính sách và quy định
        public ActionResult QuyDinh()
        {
            return View();
        }

        // 3. Chính sách bảo mật thông tin
        public ActionResult BaoMat()
        {
            return View();
        }
    }
}