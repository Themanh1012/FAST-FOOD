using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class DonHang
    {

        [Key]
        public int MaDonHang {  get; set; }

        [Required]
        [Display(Name = "Ngày Đặt")]
        public DateTime NgayDat { get; set; }= DateTime.Now;

        [Required, StringLength(100)]
        [Display(Name = "Tên Khách Hàng")]
        public string TenKhachHang {  get; set; }

        [StringLength(200)]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi {  get; set; }
        [StringLength(15)]
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai {  get; set; }

        [Display(Name = "Tổng Tiền")]
        [DataType(DataType.Currency)]
        public decimal TongTien {  get; set; }

        [Display(Name = "Trạng Thái")]
        public string TrangThai {  get; set; }

        [Display(Name = "Hình Thức Thanh Toán")]
        public int? MaHTTT { get; set; }

        [ForeignKey("MaHTTT")]
        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }

      
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}