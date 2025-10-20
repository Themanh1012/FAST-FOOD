using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FAST_FOOD.Models;

namespace FAST_FOOD.Controllers
{
    public class MonAnsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: MonAns
        public ActionResult Index()
        {
            var monAns = db.MonAns.Include(m => m.DanhMuc);
            return View(monAns.ToList());
        }

        // GET: MonAns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // GET: MonAns/Create
        public ActionResult Create()
        {
            var danhMucList = db.Danhmucs.ToList();
            ViewBag.DanhMucId = new SelectList(db.Danhmucs, "DanhMucId", "TenDanhMuc");
            return View();
        }

        // POST: MonAns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonAnId,TenMon,Gia,HinhAnh,DanhMucId")] MonAn monAn, HttpPostedFileBase HinhAnhFile)
        {
            if (ModelState.IsValid)
            {
                //NHỚ KỸ 
                if (HinhAnhFile != null && HinhAnhFile.ContentLength > 0)
                {
                    //lay ten file
                    var fileName = System.IO.Path.GetFileName(HinhAnhFile.FileName);

                    //luu thu muc
                    var path = Server.MapPath("~/Images/" + fileName);

                    //luu file vao image
                    HinhAnhFile.SaveAs(path);

                    //gan ten file trong model
                    monAn.HinhAnh = fileName;
                }
                else
                {
                    monAn.HinhAnh = monAn.HinhAnh ?? "noimage.png"; // hoặc string.Empty
                }
                try
                {
                    db.MonAns.Add(monAn);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Ghi log hoặc in ra lỗi chi tiết để xem nguyên nhân thực sự
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException?.Message);

                    ViewBag.Error = ex.InnerException?.Message ?? ex.Message;
                    ViewBag.DanhMucId = new SelectList(db.Danhmucs, "DanhMucId", "TenDanhMuc", monAn.DanhMucId);
                    return View(monAn);
                }
            }

            ViewBag.DanhMucId = new SelectList(db.Danhmucs, "DanhMucId", "TenDanhMuc", monAn.DanhMucId);
            return View(monAn);
        }

        // GET: MonAns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanhMucId = new SelectList(db.Danhmucs, "DanhMucId", "TenDanhMuc", monAn.DanhMucId);
            return View(monAn);
        }

        // POST: MonAns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MonAnId,TenMon,Gia,HinhAnh,DanhMucId")] MonAn monAn )
        {
            if (ModelState.IsValid)
            {
               

                db.Entry(monAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DanhMucId = new SelectList(db.Danhmucs, "DanhMucId", "TenDanhMuc", monAn.DanhMucId);
            return View(monAn);
        }

        // GET: MonAns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // POST: MonAns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonAn monAn = db.MonAns.Find(id);
            db.MonAns.Remove(monAn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
