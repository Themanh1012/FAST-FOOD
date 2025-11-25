using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Customer.Controllers
{
    public class DatTiecController : Controller
    {
        private Dictionary<string, List<string>> DiaDiemData = new Dictionary<string, List<string>>
{
    // Key: "Thành phố Hồ Chí Minh" -> "THANHPHOHOCHIMINH"
    {"Thành phố Hồ Chí Minh", new List<string> { "Quận 1", "Quận 3", "Quận 4", "Quận 7", "Quận Bình Thạnh", "Thành phố Thủ Đức" }},
    
    // Key: "Hà Nội" -> "HANOI"
    {"Hà Nội", new List<string> { "Quận Ba Đình", "Quận Hoàn Kiếm", "Quận Hai Bà Trưng", "Quận Đống Đa", "Quận Cầu Giấy" }},
    
    // Key: "Đà Nẵng" -> "DANANG"
    {"Đà Nẵng", new List<string> { "Quận Hải Châu", "Quận Thanh Khê", "Quận Sơn Trà", "Quận Ngũ Hành Sơn" }}
    
    // Bạn có thể thêm các tỉnh khác nếu muốn, ví dụ:
    // {"BINHDUONG", new List<string> { "Thành phố Thủ Dầu Một", "Thành phố Thuận An", "Thành phố Dĩ An" }}
};
        // Giả lập dữ liệu Phường/Xã
        private Dictionary<string, List<string>> PhuongXaData = new Dictionary<string, List<string>>
{
    // Quận 1 (TPHCM)
    {"Quận 1", new List<string> { "Phường Bến Nghé", "Phường Phạm Ngũ Lão", "Phường Tân Định" }},
    // Quận 7 (TPHCM)
    {"Quận 7", new List<string> { "Phường Tân Phong", "Phường Tân Phú", "Phường Phú Mỹ" }},
    // Quận Hoàn Kiếm (Hà Nội)
    {"Quận Hoàn Kiếm", new List<string> { "Phường Tràng Tiền", "Phường Hàng Đào", "Phường Cửa Nam" }},
    // Quận Thanh Khê (Đà Nẵng)
    {"Quận Thanh Khê", new List<string> { "Phường Vĩnh Trung", "Phường Thạc Gián" }}
    // Thêm các quận/huyện khác nếu cần...
};

        private List<SelectListItem> GetAllTinhThanhPho()
        {
            // Dùng mảng chuỗi đơn giản để liệt kê 63 tỉnh/thành
            string[] tinhThanh = new string[]
            {
        "Thành phố Hồ Chí Minh","Hà Nội","An Giang", "Bà Rịa - Vũng Tàu", "Bạc Liêu", "Bắc Kạn", "Bắc Giang", "Bắc Ninh",
        "Bến Tre", "Bình Dương", "Bình Định", "Bình Phước", "Bình Thuận", "Cà Mau",
        "Cao Bằng", "Cần Thơ", "Đà Nẵng", "Đắk Lắk", "Đắk Nông", "Điện Biên", "Đồng Nai",
        "Đồng Tháp", "Gia Lai", "Hà Giang", "Hà Nam", "Hà Tĩnh", "Hải Dương",
        "Hải Phòng", "Hậu Giang", "Hòa Bình", "Hưng Yên", "Khánh Hòa",
        "Kiên Giang", "Kon Tum", "Lai Châu", "Lâm Đồng", "Lạng Sơn", "Lào Cai", "Long An",
        "Nam Định", "Nghệ An", "Ninh Bình", "Ninh Thuận", "Phú Thọ", "Phú Yên", "Quảng Bình",
        "Quảng Nam", "Quảng Ngãi", "Quảng Ninh", "Quảng Trị", "Sóc Trăng", "Sơn La", "Tây Ninh",
        "Thái Bình", "Thái Nguyên", "Thanh Hóa", "Thừa Thiên Huế", "Tiền Giang", "Trà Vinh",
        "Tuyên Quang", "Vĩnh Long", "Vĩnh Phúc", "Yên Bái"
            };

            // Sử dụng LINQ để sắp xếp, chọn và tạo danh sách SelectListItem một cách ngắn gọn
            var listItems = tinhThanh
           .OrderBy(t => t) // Sắp xếp theo tên
           .Select(ten => new SelectListItem
           {
               // Value sẽ là tên viết hoa không dấu, không khoảng trắng (dùng làm mã)
               Value = ten,
               Text = ten
           })
           .ToList();

            // Thêm mục mặc định
            listItems.Insert(0, new SelectListItem { Value = "", Text = "Tỉnh/Thành phố" });

            return listItems;
        }

        // GET: /DatTiec/DatTiec
        public ActionResult DatTiec()
        {
            var model = new DatTiec();
            ViewBag.TinhThanhPhoList = GetAllTinhThanhPho();
            ViewBag.QuanHuyenList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Quận/Huyện" } };

            return View(model);
        }

        // POST AJAX: /DatTiec/GetQuanHuyenByTinh
        [HttpPost]
        public JsonResult GetQuanHuyenByTinh(string maTinh)
        {
            List<SelectListItem> quanHuyenList = new List<SelectListItem>();
            quanHuyenList.Add(new SelectListItem { Value = "", Text = "Quận/Huyện" });

            if (!string.IsNullOrEmpty(maTinh) && DiaDiemData.ContainsKey(maTinh))
            {
                foreach (var qh in DiaDiemData[maTinh])
                {
                    quanHuyenList.Add(new SelectListItem { Value = qh, Text = qh });
                }
            }

            return Json(quanHuyenList);
        }
        // POST AJAX: Lấy danh sách Phường/Xã dựa trên Tên Quận
        [HttpPost]
        public JsonResult GetPhuongXaByQuan(string tenQuan)
        {
            List<SelectListItem> phuongXaList = new List<SelectListItem>();
            phuongXaList.Add(new SelectListItem { Value = "", Text = "Phường/Xã" });

            if (!string.IsNullOrEmpty(tenQuan) && PhuongXaData.ContainsKey(tenQuan))
            {
                foreach (var px in PhuongXaData[tenQuan])
                {
                    phuongXaList.Add(new SelectListItem { Value = px, Text = px });
                }
            }

            // Trả về dưới dạng JSON
            return Json(phuongXaList);
        }
        // POST: /DatTiec/XuLyDatTiec
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XuLyDatTiec(DatTiec model, string Ngay, string Thang, string Nam)
        {
            // ... (Giữ nguyên logic xử lý POST đã trình bày trước đó) ...
            try
            {
                int day = int.Parse(Ngay);
                int month = int.Parse(Thang);
                int year = int.Parse(Nam);
                model.ThoiGianToChuc = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                ModelState.AddModelError("ThoiGianToChuc", "Vui lòng chọn ngày, tháng, năm hợp lệ.");
            }

            if (ModelState.IsValid)
            {
              
                return RedirectToAction("DatTiecThanhCong");
            }

            // Nếu lỗi, phải gán lại ViewBag để Dropdown Tỉnh/Thành phố hiển thị lại đúng dữ liệu
            ViewBag.TinhThanhPhoList = GetAllTinhThanhPho();

            // Trả về View với Model có lỗi
            return View("DatTiec", model);
        }

        public ActionResult DatTiecThanhCong()
        {
            ViewBag.Title = "Đặt Tiệc Thành Công";
            return View();
        }
    }
}