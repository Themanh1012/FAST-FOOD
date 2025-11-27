using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FAST_FOOD.Models
{
    
    public class DatTiec
    {
        public int Id { get; set; }

        public int MaTK { get; set; }
        public virtual account Account { get; set; }
        // THÔNG TIN LIÊN HỆ
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        [Display(Name = "Họ và tên người đặt *")]
        public string HoTenNguoiDat { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Display(Name = "Số điện thoại *")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        // THÔNG TIN BUỔI TIỆC
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ tổ chức.")]
        [Display(Name = "Địa điểm tổ chức *")]
        public string DiaChiToChuc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành phố.")]
        [Display(Name = "Tỉnh/Thành phố *")]
        public string TinhThanhPho { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Quận/Huyện.")]
        public string QuanHuyen { get; set; }

        // Giả sử Phường/Xã không bắt buộc nhập cho đơn giản
        public string PhuongXa { get; set; }

        // Ngày, Tháng, Năm sẽ được ghép lại trong Controller
        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian tổ chức *")]
        public DateTime ThoiGianToChuc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn số lượng người tham dự.")]
        [Display(Name = "Số lượng người tham dự *")]
        public string SoLuongNguoiThamDu { get; set; }
        // THÔNG TIN QUẢN LÝ ĐƠN HÀNG (Quan trọng cho Database)
        // Thêm trường này để ghi lại thời điểm đơn được tạo
        [Display(Name = "Ngày đặt")]
        public DateTime NgayDat { get; set; } = DateTime.Now;
        // Thêm trường này để theo dõi trạng thái xử lý đơn hàng
        [StringLength(50)]
        [Display(Name = "Trạng thái đơn hàng")]
        public string TrangThai { get; set; } = "Mới";
        // Điều khoản
        [MustBeTrue(ErrorMessage = "Vui lòng đồng ý với các điều khoản và chính sách.")]
        [Display(Name = "Tôi đã đọc và đồng ý...")]
        public bool DongYDieuKhoan { get; set; }
    }
    // Custom Validation Attribute để kiểm tra Checkbox
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }
    }
}