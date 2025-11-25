using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Controllers
{
    public class HeThongNhaHangController : Controller
    {
        // Giả lập dữ liệu 3 địa chỉ nhà hàng mẫu
        private List<NhaHang> DuLieuNhaHangMau()
        {
            return new List<NhaHang>
            {
                new NhaHang {
                    TenNhaHang = "KFC Nguyễn Huệ",
                    DiaChi = "71 Nguyễn Huệ, Quận 1, TPHCM",
                    Lat = 10.7766,
                    Lng = 106.7032,
                    SoDienThoai = "028 3821 8778",
                    GioMoCua = "10:00 - 22:00",
                    HinhAnh = "https://tse2.mm.bing.net/th/id/OIP.ikOLIToN0i-v3Ojxq35YWQHaEh?rs=1&pid=ImgDetMain&o=7&rm=3" // Ảnh mẫu
                },
                new NhaHang {
                    TenNhaHang = "KFC Aeon Mall BD",
                    DiaChi = "Đại lộ Bình Dương, Thuận An, Bình Dương",
                    Lat = 10.9634,
                    Lng = 106.6857,
                    SoDienThoai = "1900 6886",
                    GioMoCua = "09:00 - 22:00",
                    HinhAnh = "https://aeonmall-hadong.com.vn/wp-content/uploads/2023/01/dsc00983-750x468.jpg"
                },
                new NhaHang {
                    TenNhaHang = "KFC Biên Hòa",
                    DiaChi = "Lô A, Đường Đồng Khởi, Biên Hòa, Đồng Nai",
                    Lat = 10.9577,
                    Lng = 106.8227,
                    SoDienThoai = "0251 389 4567",
                    GioMoCua = "08:00 - 23:00",
                    HinhAnh = "https://tse3.mm.bing.net/th/id/OIP.2OEdgaxTMdkPajb7J7yFlwHaFj?rs=1&pid=ImgDetMain&o=7&rm=3"
                }
            };
        }


        // GET: /HeThongNhaHang/Index
        public ActionResult Index()
        {
            var model = new HeThongNhaHang
            {
                DanhSachNhaHang = DuLieuNhaHangMau()
            };
            return View(model);
        }

        // [TÙY CHỌN] Action để xử lý tìm kiếm
        [HttpPost]
        public ActionResult TimKiem(HeThongNhaHang model)
        {
            // Logic tìm kiếm thực tế sẽ sử dụng Geocoding API để chuyển Địa chỉ thành Lat/Lng
            // Sau đó lọc DanhSachNhaHang gần tọa độ đó.

            // Ví dụ đơn giản: Giữ nguyên danh sách mẫu
            model.DanhSachNhaHang = DuLieuNhaHangMau();

            // Nếu có kết quả tìm kiếm, có thể đặt ViewBag.CenterLat/Lng để bản đồ zoom vào đó

            return View("Index", model);
        }
    }
}