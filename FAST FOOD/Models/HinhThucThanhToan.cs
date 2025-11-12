using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class HinhThucThanhToan
    {
        [Key]
        public int MaHTTT { get; set; }

        [Required,StringLength(100)]
        [Display(Name = "Tên Hình Thức Thanh Toán")]
         
        public string TenHinhThuc { get; set; }

        // Quan hệ 1-n với DonHang
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public HinhThucThanhToan()
        {
            DonHangs = new HashSet<DonHang>();
        }
    }
}