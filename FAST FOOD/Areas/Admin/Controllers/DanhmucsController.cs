using FAST_FOOD.Areas.Admin.Filters;
using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin.Controllers
{
    //[AdminAuthorize]
    public class DanhmucsController : Controller
    {
        private KFCContext db = new KFCContext();

        // GET: Danhmucs
        public ActionResult Index()
        {
            return View(db.Danhmucs.ToList());
        }

        // GET: Danhmucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Danhmuc danhmuc = db.Danhmucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // GET: Danhmucs/Create
        // GET: Danhmucs/Create
        public ActionResult Create()
        {
            return View(new FAST_FOOD.Models.Danhmuc());
        }

        // POST: Admin/Danhmucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DanhMucId,TenDanhMuc")] Danhmuc danhmuc, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra và tạo thư mục nếu chưa có
                string folderPath = Server.MapPath("~/Images/Products/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Nếu có upload ảnh thì lưu file
                if (UploadImage != null && UploadImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(UploadImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/Products/"), fileName);

                    UploadImage.SaveAs(filePath);

                    danhmuc.HinhAnh = "~/Images/Products/" + fileName;
                    // ✔ CHỈ LƯU TÊN FILE
                }

                // Lưu xuống DB
                db.Danhmucs.Add(danhmuc);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(danhmuc);
        }
        // GET: Danhmucs/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            System.Diagnostics.Debug.WriteLine("===== DEBUG INFO =====");
            System.Diagnostics.Debug.WriteLine("Connection: " + db.Database.Connection.ConnectionString);
            System.Diagnostics.Debug.WriteLine("Danhmuc count: " + db.Danhmucs.Count());

            var item = db.Danhmucs.Find(id);
            if (item == null)
            {
                System.Diagnostics.Debug.WriteLine("Không tìm thấy ID " + id);
                TempData["Error"] = $"Không tìm thấy danh mục (ID={id}).";
                return RedirectToAction("Index");
            }

            System.Diagnostics.Debug.WriteLine("Tìm thấy danh mục: " + item.TenDanhMuc);
            return View(item);
        }
        // POST: Danhmucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DanhMucId,TenDanhMuc")] Danhmuc danhmuc, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                var danhmucGoc = db.Danhmucs.Find(danhmuc.DanhMucId);
                if (danhmucGoc == null)
                    return HttpNotFound();

                danhmucGoc.TenDanhMuc = danhmuc.TenDanhMuc;

                if (UploadImage != null && UploadImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(UploadImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/Products/"), fileName);
                    UploadImage.SaveAs(path);

                    danhmucGoc.HinhAnh = "/Images/Products/" + fileName;

                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhmuc);
        }

        // GET: Danhmucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Danhmuc danhmuc = db.Danhmucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // POST: Danhmucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var danhmuc = db.Danhmucs.Find(id);
            if (danhmuc == null)
                return HttpNotFound();

            bool hasProducts = db.MonAns.Any(p => p.DanhMucId == id);
            if (hasProducts)
            {
                ModelState.AddModelError("", "Không thể xóa danh mục vì đang có sản phẩm thuộc danh mục này!");
                return View(danhmuc);
            }

            db.Danhmucs.Remove(danhmuc);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    }
