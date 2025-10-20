using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class DonHang
    {

        [Key]
        public int DonHangId {  get; set; }

        public DateTime NgayDat { get; set; }

        [Required, StringLength(100)]
        public string TenKhachHang {  get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}