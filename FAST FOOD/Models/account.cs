using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        public string VaiTro { get; set; } = "User";

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

        public int MaTK {get; set; }

        [Required , StringLength(100)]
        [Display(Name ="Tên Đăng Nhập")]
        public string TenDangNhap { get; set; }

        [Required , StringLength(255)]
        [Display(Name = "Mật Khẩu")]
        public string MatKhau { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Vai Trò")]
        public string VaiTro { get; set; } = "User";

        [StringLength(100)]
        [Display(Name = "Họ Tên")]
        public string HoTen { get; set; }

        [StringLength(200)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

