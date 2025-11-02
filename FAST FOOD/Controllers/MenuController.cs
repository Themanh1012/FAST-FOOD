using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private KFCContext db = new KFCContext();
        public ActionResult Index()
        {
            var danhmucs = db.Danhmucs.ToList();
            return View(danhmucs);
        }
        public ActionResult DanhMuc(int id)
        {
            var danhMuc = db.Danhmucs.Include("MonAns").FirstOrDefault(dm => dm.DanhMucId == id);
            if(danhMuc==null)
                return HttpNotFound();
            return View(danhMuc);
        }
    }
}