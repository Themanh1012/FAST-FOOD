using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class account
    {
        [Key]

        public int MaTK { get; set; }

        [Required, StringLength(100, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 ký tự trở lên.")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }
            
        [Required, StringLength(255, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên.")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [NotMapped]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string XacNhanMatKhau { get; set; }

        [Required]
        [Display(Name = "Vai trò")]
        public string VaiTro { get; set; } = "Khách hàng ";

        [StringLength(100)]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Mã khôi phục")]
        public string ResetToken { get; set; }
        [Display(Name = "Thời gian hết hạn mã khôi phục")]
        public DateTime? TokenExpireTime { get; set; }
    }
}

      
  

