using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class ChiTietDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChiTietId {  get; set; }

        public int DonHangId { get; set; }
        [ForeignKey("DonHangId")]
        public virtual DonHang DonHang { get; set; }

        public int MonAnId { get; set; }
        [ForeignKey("MonAnId")]
        public virtual MonAn MonAn { get; set; }

        public int SoLuong { get; set; }
        public decimal ThanhTien {  get; set; }
        public object MaDonHang { get; internal set; }
    }
}