using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class NhaHang
    {

        public string TenNhaHang { get; set; }
        public string DiaChi { get; set; }
        public double Lat { get; set; } // Vĩ độ
        public double Lng { get; set; } // Kinh độ
        public string HinhAnh { get; set; }      // Đường dẫn hình ảnh
        public string SoDienThoai { get; set; }  // Số điện thoại
        public string GioMoCua { get; set; }     // Giờ mở cửa
    }
    public class HeThongNhaHang
    {
        // Danh sách các nhà hàng sẽ hiển thị trên bản đồ và danh sách
        public List<NhaHang> DanhSachNhaHang { get; set; }

        // Có thể thêm thuộc tính cho ô tìm kiếm nếu cần
        public string DiaChiTimKiem { get; set; }
    }
}