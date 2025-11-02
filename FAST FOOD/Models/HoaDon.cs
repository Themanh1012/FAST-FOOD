using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        //Khoa ngoai toi Donhang
        [Required]
        public int MaDonHang { get; set; }

        [ForeignKey("MaDonHang")]
        public virtual DonHang DonHang { get; set; }

        //Khoa ngoai toi Nhanvien
        [Required]
        public int MaNV { get; set; }
        [ForeignKey(nameof(MaNV))]
        public Nhanvien Nhanvien { get; set; }

        //Khoa ngoai toi HinhThucThanhToan

       
        [Required]
        public int MaHTTT { get; set; }

        [ForeignKey("MaHTTT")]
        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }    

        public DateTime NgayThanhToan { get; set; }  = DateTime.Now;
        [Required]
        public decimal TongTien { get; set; }   
    }
}