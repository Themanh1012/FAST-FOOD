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
        public int MaDonHang { get; set; }
        [ForeignKey(nameof(MaDonHang))]
        public DonHang DonHang { get; set; }

        //Khoa ngoai toi Nhanvien
        public int MaNV { get; set; }
        [ForeignKey(nameof(MaNV))]
        public Nhanvien Nhanvien { get; set; }

        //Khoa ngoai toi HinhThucThanhToan

        [ForeignKey("HinhThucThanhToan")]
        public int MaHTTT { get; set; }
        [ForeignKey(nameof(MaHTTT))]
        public HinhThucThanhToan HinhThucThanhToan { get; set; }    

        public DateTime NgayThanhToan { get; set; }  = DateTime.Now;
        public decimal TongTien { get; set; }   
    }
}