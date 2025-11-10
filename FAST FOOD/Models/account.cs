using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FAST_FOOD.Models
{
    public class account
    {
        [Key]
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